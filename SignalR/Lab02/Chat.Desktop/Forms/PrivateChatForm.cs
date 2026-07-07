using Chat.Desktop.Services;
using System.Text.Json;

namespace Chat.Desktop
{
    public class PrivateChatForm : Form
    {
        private readonly ApiService _api;
        private readonly ChatHubService _hub;
        private readonly string _currentUserId;
        private readonly string _currentUserName;
        private readonly string _receiverId;
        private readonly string _receiverName;

        private static readonly Color Brand = Color.FromArgb(25, 135, 84);

        private RichTextBox rtbChat = null!;
        private TextBox txtMessage = null!;

        public PrivateChatForm(ApiService api, ChatHubService hub,
            string currentUserId, string currentUserName,
            string receiverId, string receiverName)
        {
            _api = api;
            _hub = hub;
            _currentUserId = currentUserId;
            _currentUserName = currentUserName;
            _receiverId = receiverId;
            _receiverName = receiverName;

            Text = $"Private Chat — {receiverName}";
            Size = new Size(620, 500);
            MinimumSize = new Size(460, 360);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 9.5f);
            BuildUI();
        }

        private void BuildUI()
        {
            var header = new Panel { Dock = DockStyle.Top, Height = 46, BackColor = Brand };
            header.Controls.Add(new Label
            {
                Text = $"💬  {_receiverName}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(14, 10)
            });

            rtbChat = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10)
            };

            var inputPanel = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.White, Padding = new Padding(8) };
            txtMessage = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11), BorderStyle = BorderStyle.FixedSingle };
            txtMessage.KeyPress += (_, e) => { if (e.KeyChar == '\r') { e.Handled = true; SendMessage(); } };

            var btnSend = new Button
            {
                Text = "Send",
                Dock = DockStyle.Right,
                Width = 90,
                BackColor = Brand,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Click += (_, _) => SendMessage();

            inputPanel.Controls.Add(txtMessage);
            inputPanel.Controls.Add(btnSend);

            Controls.Add(rtbChat);
            Controls.Add(inputPanel);
            Controls.Add(header);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _hub.PrivateMessageReceived += OnPrivateMessageReceived;
            await LoadHistoryAsync();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _hub.PrivateMessageReceived -= OnPrivateMessageReceived;
            base.OnFormClosed(e);
        }

        private async Task LoadHistoryAsync()
        {
            var messages = await _api.GetAsync<List<JsonElement>>($"api/messages/private/{_receiverId}");
            if (messages == null) return;

            foreach (var msg in messages)
            {
                var senderId = msg.GetProperty("senderId").GetString() ?? "";
                var sender = msg.GetProperty("senderName").GetString() ?? "";
                var content = msg.GetProperty("content").GetString() ?? "";
                var sentAt = msg.GetProperty("sentAt").GetDateTime();
                AppendMessage(sender, content, senderId == _currentUserId, sentAt);
            }
        }

        private void OnPrivateMessageReceived(PrivateMessageDto msg)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(() => OnPrivateMessageReceived(msg)); return; }

            bool isThisConversation =
                (msg.SenderId == _currentUserId && msg.ReceiverId == _receiverId) ||
                (msg.SenderId == _receiverId && msg.ReceiverId == _currentUserId);

            if (!isThisConversation) return;

            AppendMessage(msg.SenderName, msg.Content, msg.SenderId == _currentUserId, msg.SentAt);
        }

        private void AppendMessage(string sender, string content, bool isMine, DateTime sentAt)
        {
            rtbChat.SelectionStart = rtbChat.TextLength;

            rtbChat.SelectionColor = isMine ? Brand : Color.SeaGreen;
            rtbChat.SelectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
            rtbChat.AppendText(isMine ? "You" : sender);

            rtbChat.SelectionColor = Color.Gray;
            rtbChat.SelectionFont = new Font("Segoe UI", 8);
            rtbChat.AppendText($"   {sentAt.ToLocalTime():HH:mm}\n");

            rtbChat.SelectionColor = Color.Black;
            rtbChat.SelectionFont = new Font("Segoe UI", 10);
            rtbChat.AppendText($"{content}\n\n");

            rtbChat.ScrollToCaret();
        }

        private async void SendMessage()
        {
            var text = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;
            txtMessage.Clear();
            await _hub.SendPrivateMessageAsync(_receiverId, text);
        }
    }
}
