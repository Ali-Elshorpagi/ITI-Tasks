using Chat.Desktop.Services;
using System.Text.Json;

namespace Chat.Desktop
{
    public partial class Form1 : Form
    {
        private readonly ApiService _api = new();

        public Form1()
        {
            InitializeComponent();
            Text = "ChatApp — Login";
            Size = new Size(380, 280);
            StartPosition = FormStartPosition.CenterScreen;
            BuildUI();
        }

        private TextBox txtEmailOrUsername = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Button btnRegister = null!;
        private Label lblStatus = null!;

        private void BuildUI()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 6,
                ColumnCount = 2
            };

            panel.Controls.Add(new Label { Text = "💬 ChatApp Login", Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true }, 0, 0);
            panel.SetColumnSpan(panel.Controls[0], 2);

            panel.Controls.Add(new Label { Text = "Email or Username:", AutoSize = true, Anchor = AnchorStyles.Right }, 0, 1);
            txtEmailOrUsername = new TextBox { Width = 200 };
            panel.Controls.Add(txtEmailOrUsername, 1, 1);

            panel.Controls.Add(new Label { Text = "Password:", AutoSize = true, Anchor = AnchorStyles.Right }, 0, 2);
            txtPassword = new TextBox { Width = 200, PasswordChar = '•' };
            panel.Controls.Add(txtPassword, 1, 2);

            btnLogin = new Button { Text = "Login", Width = 95, BackColor = Color.CornflowerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnLogin.Click += BtnLogin_Click;
            panel.Controls.Add(btnLogin, 0, 3);

            btnRegister = new Button { Text = "Register", Width = 95 };
            btnRegister.Click += BtnRegister_Click;
            panel.Controls.Add(btnRegister, 1, 3);

            lblStatus = new Label { AutoSize = true, ForeColor = Color.Red };
            panel.Controls.Add(lblStatus, 0, 4);
            panel.SetColumnSpan(lblStatus, 2);

            Controls.Add(panel);
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            lblStatus.Text = "Logging in...";
            lblStatus.ForeColor = Color.Gray;

            var (success, content) = await _api.PostAsync("api/auth/login", new
            {
                emailOrUsername = txtEmailOrUsername.Text,
                password = txtPassword.Text
            });

            if (!success)
            {
                lblStatus.Text = "Invalid credentials.";
                lblStatus.ForeColor = Color.Red;
                return;
            }

            var data = JsonSerializer.Deserialize<JsonElement>(content);
            _api.JwtToken = data.GetProperty("token").GetString() ?? "";
            var userId = data.GetProperty("userId").GetString() ?? "";
            var userName = data.GetProperty("userName").GetString() ?? "";
            var displayName = data.GetProperty("displayName").GetString() ?? "";

            var dashboard = new DashboardForm(_api, userId, userName, displayName);
            dashboard.Show();
            Hide();
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            var registerForm = new RegisterForm(_api);
            registerForm.LoginSucceeded += (userId, userName, displayName) =>
            {
                var dashboard = new DashboardForm(_api, userId, userName, displayName);
                dashboard.Show();
                Hide();
            };
            registerForm.Show();
        }
    }
}
