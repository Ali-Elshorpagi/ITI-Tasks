using Chat.Desktop.Services;
using System.Text.Json;

namespace Chat.Desktop
{
    public class AuditLogsForm : Form
    {
        private readonly ApiService _api;
        private readonly ChatHubService _hub;
        private DataGridView grid = null!;

        public AuditLogsForm(ApiService api, ChatHubService hub)
        {
            _api = api;
            _hub = hub;

            Text = "ChatApp — Audit Logs";
            Size = new Size(760, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 9.5f);
            BuildUI();
        }

        private void BuildUI()
        {
            var header = new Panel { Dock = DockStyle.Top, Height = 48, BackColor = Color.FromArgb(33, 37, 41) };
            header.Controls.Add(new Label
            {
                Text = "📋 Audit Logs  ● live",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(14, 11)
            });

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false
            };
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            grid.Columns.Add("Id", "#");
            grid.Columns.Add("User", "User");
            grid.Columns.Add("Action", "Action");
            grid.Columns.Add("Details", "Details");
            grid.Columns.Add("Timestamp", "Timestamp");
            grid.Columns["Id"]!.FillWeight = 25;
            grid.Columns["User"]!.FillWeight = 55;
            grid.Columns["Action"]!.FillWeight = 60;
            grid.Columns["Details"]!.FillWeight = 90;
            grid.Columns["Timestamp"]!.FillWeight = 70;

            Controls.Add(grid);
            Controls.Add(header);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _hub.AuditUpdated += OnAuditUpdated;
            await LoadLogsAsync();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _hub.AuditUpdated -= OnAuditUpdated;
            base.OnFormClosed(e);
        }

        private async Task LoadLogsAsync()
        {
            var logs = await _api.GetAsync<List<JsonElement>>("api/audit");
            grid.Rows.Clear();
            if (logs == null) return;

            foreach (var log in logs)
            {
                AddRow(
                    log.GetProperty("id").GetInt32(),
                    log.GetProperty("userName").GetString() ?? "",
                    log.GetProperty("action").GetString() ?? "",
                    log.GetProperty("details").GetString() ?? "",
                    log.GetProperty("timestamp").GetDateTime());
            }
        }

        private void OnAuditUpdated(AuditLogDto log)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(() => OnAuditUpdated(log)); return; }

            int i = grid.Rows.Add(log.Id, log.UserName, log.Action, log.Details,
                log.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
            var row = grid.Rows[i];
            grid.Rows.Remove(row);
            grid.Rows.Insert(0, row);
            ColorRow(row, log.Action);
        }

        private void AddRow(int id, string user, string action, string details, DateTime ts)
        {
            int i = grid.Rows.Add(id, user, action, details, ts.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
            ColorRow(grid.Rows[i], action);
        }

        private static void ColorRow(DataGridViewRow row, string action)
        {
            var cell = row.Cells["Action"];
            (Color back, Color fore) = action switch
            {
                "Login" => (Color.SeaGreen, Color.White),
                "Register" => (Color.RoyalBlue, Color.White),
                "Logout" => (Color.Gray, Color.White),
                "CreateRoom" => (Color.Teal, Color.White),
                "DeleteRoom" => (Color.Firebrick, Color.White),
                "JoinRoom" or "LeaveRoom" => (Color.Goldenrod, Color.Black),
                "PublicMessage" => (Color.RoyalBlue, Color.White),
                "PrivateMessage" => (Color.Teal, Color.White),
                "UserConnected" => (Color.SeaGreen, Color.White),
                "UserDisconnected" => (Color.Gray, Color.White),
                _ => (Color.DimGray, Color.White)
            };
            cell.Style.BackColor = back;
            cell.Style.ForeColor = fore;
            cell.Style.SelectionBackColor = back;
            cell.Style.SelectionForeColor = fore;
        }
    }
}
