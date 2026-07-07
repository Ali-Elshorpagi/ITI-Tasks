using Chat.Desktop.Models;
using Chat.Desktop.Services;

namespace Chat.Desktop
{
    public class DashboardForm : Form
    {
        private readonly ApiService _api;
        private readonly ChatHubService _hub = new();
        private readonly string _userId;
        private readonly string _userName;
        private readonly string _displayName;

        private static readonly Color Brand = Color.FromArgb(13, 110, 253);

        private ListBox lstRooms = null!;
        private ListBox lstUsers = null!;
        private Label lblRoomInfo = null!;
        private Label lblStatus = null!;
        private Button btnEnter = null!, btnJoin = null!, btnLeave = null!, btnDelete = null!;

        public DashboardForm(ApiService api, string userId, string userName, string displayName)
        {
            _api = api;
            _userId = userId;
            _userName = userName;
            _displayName = displayName;

            Text = $"ChatApp — Dashboard ({displayName})";
            Size = new Size(860, 600);
            MinimumSize = new Size(760, 520);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 9.5f);
            BuildUI();
        }

        private void BuildUI()
        {
            var header = new Panel { Dock = DockStyle.Top, Height = 56, BackColor = Brand };

            var lblTitle = new Label
            {
                Text = "💬  ChatApp",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(14, 12)
            };

            var lblWelcome = new Label
            {
                Text = $"Hi, {_displayName}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(200, 18)
            };

            var btnLogout = new Button
            {
                Text = "⏻  Logout",
                Size = new Size(100, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Brand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;
            header.Layout += (_, _) =>
                btnLogout.Location = new Point(header.Width - btnLogout.Width - 12, 13);

            header.Controls.Add(lblTitle);
            header.Controls.Add(lblWelcome);
            header.Controls.Add(btnLogout);

            var footer = new Panel { Dock = DockStyle.Bottom, Height = 44, BackColor = Color.White };

            lblStatus = new Label
            {
                Text = "Connecting…",
                ForeColor = Color.DarkOrange,
                Font = new Font("Segoe UI", 9),
                AutoSize = true,
                Location = new Point(12, 13)
            };

            var footerButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Right,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(4, 6, 8, 4)
            };

            var btnRefresh = FooterBtn("↻  Refresh", Color.FromArgb(108, 117, 125));
            btnRefresh.Click += async (_, _) => await LoadDataAsync();

            var btnAudit = FooterBtn("📋  Audit Logs", Color.FromArgb(52, 58, 64));
            btnAudit.Click += (_, _) => new AuditLogsForm(_api, _hub).Show();

            footerButtons.Controls.Add(btnRefresh);
            footerButtons.Controls.Add(btnAudit);

            footer.Controls.Add(lblStatus);
            footer.Controls.Add(footerButtons);

            var body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10)
            };
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            body.Controls.Add(BuildRoomsPanel(), 0, 0);
            body.Controls.Add(BuildUsersPanel(), 1, 0);

            Controls.Add(body);
            Controls.Add(footer);
            Controls.Add(header);
        }

        private GroupBox BuildRoomsPanel()
        {
            var box = new GroupBox
            {
                Text = "🏠  Chat Rooms",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(6)
            };

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            lstRooms = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                IntegralHeight = false
            };
            lstRooms.SelectedIndexChanged += (_, _) => UpdateRoomButtons();
            lstRooms.DoubleClick += (_, _) => EnterSelectedRoom();

            lblRoomInfo = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.DimGray,
                Text = "Select a room to see details…"
            };

            var btnRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 2, 0, 0)
            };
            btnEnter = RoomBtn("Enter", Color.SeaGreen);
            btnEnter.Click += (_, _) => EnterSelectedRoom();
            btnJoin = RoomBtn("Join", Brand);
            btnJoin.Click += BtnJoin_Click;
            btnLeave = RoomBtn("Leave", Color.Goldenrod);
            btnLeave.Click += BtnLeave_Click;
            btnDelete = RoomBtn("Delete", Color.Firebrick);
            btnDelete.Click += BtnDelete_Click;
            var btnCreate = RoomBtn("+ Create", Color.MediumSlateBlue);
            btnCreate.Click += BtnCreateRoom_Click;
            btnRow.Controls.AddRange(new Control[] { btnEnter, btnJoin, btnLeave, btnDelete, btnCreate });

            layout.Controls.Add(lstRooms, 0, 0);
            layout.Controls.Add(lblRoomInfo, 0, 1);
            layout.Controls.Add(btnRow, 0, 2);
            box.Controls.Add(layout);
            return box;
        }

        private GroupBox BuildUsersPanel()
        {
            var box = new GroupBox
            {
                Text = "👥  Users",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Padding = new Padding(6)
            };

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            lstUsers = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                IntegralHeight = false
            };
            lstUsers.DoubleClick += (_, _) => MessageSelectedUser();

            var btnRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 2, 0, 0)
            };
            var btnMsg = RoomBtn("💬  Message", Brand);
            btnMsg.Width = 110;
            btnMsg.Click += (_, _) => MessageSelectedUser();
            btnRow.Controls.Add(btnMsg);

            layout.Controls.Add(lstUsers, 0, 0);
            layout.Controls.Add(btnRow, 0, 1);
            box.Controls.Add(layout);
            return box;
        }

        private static Button RoomBtn(string text, Color back) => new Button
        {
            Text = text,
            BackColor = back,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(80, 30),
            Margin = new Padding(2),
            Font = new Font("Segoe UI", 9, FontStyle.Bold)
        };

        private static Button FooterBtn(string text, Color back) => new Button
        {
            Text = text,
            BackColor = back,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(110, 30),
            Margin = new Padding(3, 0, 0, 0),
            Font = new Font("Segoe UI", 9)
        };

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await ConnectToHubAsync();
            await LoadDataAsync();
        }

        private async Task ConnectToHubAsync()
        {
            try
            {
                _hub.RoomsUpdated += () => SafeInvoke(async () => await LoadRoomsAsync());
                _hub.UserConnected += (_, _) => SafeInvoke(async () => await LoadUsersAsync());
                _hub.UserDisconnected += (_, _) => SafeInvoke(async () => await LoadUsersAsync());

                await _hub.StartAsync(_api.JwtToken);
                lblStatus.Text = "🟢  Connected — realtime updates on";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"⚠  Hub error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void SafeInvoke(Func<Task> action)
        {
            if (IsDisposed) return;
            try { BeginInvoke(action); } catch { }
        }

        private async Task LoadDataAsync()
        {
            await LoadRoomsAsync();
            await LoadUsersAsync();
        }

        private async Task LoadRoomsAsync()
        {
            var selectedId = (lstRooms.SelectedItem as RoomItem)?.Room.Id;
            var rooms = await _api.GetAsync<List<RoomDto>>("api/rooms");
            lstRooms.Items.Clear();
            if (rooms != null)
                foreach (var r in rooms)
                    lstRooms.Items.Add(new RoomItem(r));

            if (selectedId != null)
                for (int i = 0; i < lstRooms.Items.Count; i++)
                    if (((RoomItem)lstRooms.Items[i]).Room.Id == selectedId) { lstRooms.SelectedIndex = i; break; }

            UpdateRoomButtons();
        }

        private async Task LoadUsersAsync()
        {
            var users = await _api.GetAsync<List<UserDto>>("api/users");
            lstUsers.Items.Clear();
            if (users != null)
                foreach (var u in users)
                    if (u.Id != _userId)
                        lstUsers.Items.Add(new UserItem(u));
        }

        private void UpdateRoomButtons()
        {
            var item = lstRooms.SelectedItem as RoomItem;
            bool has = item != null;
            bool isMember = has && item!.Room.IsMember;
            bool isCreator = has && item!.Room.CreatedByUserName == _userName;

            btnEnter.Enabled = has;
            btnJoin.Enabled = has && !isMember;
            btnLeave.Enabled = isMember;
            btnDelete.Enabled = isCreator;

            lblRoomInfo.Text = has
                ? $"{item!.Room.Name}  —  {item.Room.Description}\nCreated by {item.Room.CreatedByUserName} · {item.Room.MemberCount} member(s)"
                : "Select a room to see details…";
        }

        private void EnterSelectedRoom()
        {
            if (lstRooms.SelectedItem is RoomItem r)
                new ChatRoomForm(_api, _hub, _userId, r.Room.Id, r.Room.Name).Show();
        }

        private void MessageSelectedUser()
        {
            if (lstUsers.SelectedItem is UserItem u)
                new PrivateChatForm(_api, _hub, _userId, _userName, u.User.Id, u.User.DisplayName).Show();
        }

        private async void BtnJoin_Click(object? s, EventArgs e)
        {
            if (lstRooms.SelectedItem is RoomItem r)
                await _api.PostAsync($"api/rooms/{r.Room.Id}/join", new { });
        }

        private async void BtnLeave_Click(object? s, EventArgs e)
        {
            if (lstRooms.SelectedItem is RoomItem r)
                await _api.PostAsync($"api/rooms/{r.Room.Id}/leave", new { });
        }

        private async void BtnDelete_Click(object? s, EventArgs e)
        {
            if (lstRooms.SelectedItem is not RoomItem r) return;
            if (MessageBox.Show($"Delete room \"{r.Room.Name}\"?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            await _api.DeleteAsync($"api/rooms/{r.Room.Id}");
        }

        private async void BtnCreateRoom_Click(object? s, EventArgs e)
        {
            var name = Microsoft.VisualBasic.Interaction.InputBox("Room name:", "Create Room");
            if (string.IsNullOrWhiteSpace(name)) return;
            var desc = Microsoft.VisualBasic.Interaction.InputBox("Description (optional):", "Create Room");
            await _api.PostAsync("api/rooms", new { name, description = desc });
        }

        private async void BtnLogout_Click(object? s, EventArgs e)
        {
            await _api.PostAsync("api/auth/logout", new { });
            await _hub.StopAsync();
            Application.Restart();
        }

        private class RoomItem
        {
            public RoomDto Room { get; }
            public RoomItem(RoomDto r) => Room = r;
            public override string ToString() =>
                (Room.IsMember ? "✓ " : "   ") + $"{Room.Name}  ({Room.MemberCount} member{(Room.MemberCount == 1 ? "" : "s")})";
        }

        private class UserItem
        {
            public UserDto User { get; }
            public UserItem(UserDto u) => User = u;
            public override string ToString() =>
                (User.IsOnline ? "🟢 " : "⚫ ") + $"{User.DisplayName}  (@{User.UserName})";
        }
    }
}
