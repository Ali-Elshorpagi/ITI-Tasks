using Chat.Desktop.Services;
using System.Text.Json;

namespace Chat.Desktop
{
    public class RegisterForm : Form
    {
        private readonly ApiService _api;
        public event Action<string, string, string>? LoginSucceeded;

        private TextBox txtUserName = null!;
        private TextBox txtDisplayName = null!;
        private TextBox txtEmail = null!;
        private TextBox txtPassword = null!;
        private Label lblStatus = null!;

        public RegisterForm(ApiService api)
        {
            _api = api;
            Text = "ChatApp — Register";
            Size = new Size(400, 340);
            StartPosition = FormStartPosition.CenterScreen;
            BuildUI();
        }

        private void BuildUI()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 7,
                ColumnCount = 2
            };

            panel.Controls.Add(new Label { Text = "Create Account", Font = new Font("Segoe UI", 13, FontStyle.Bold), AutoSize = true }, 0, 0);
            panel.SetColumnSpan(panel.Controls[0], 2);

            panel.Controls.Add(new Label { Text = "Username:", AutoSize = true }, 0, 1);
            txtUserName = new TextBox { Width = 200 };
            panel.Controls.Add(txtUserName, 1, 1);

            panel.Controls.Add(new Label { Text = "Display Name:", AutoSize = true }, 0, 2);
            txtDisplayName = new TextBox { Width = 200 };
            panel.Controls.Add(txtDisplayName, 1, 2);

            panel.Controls.Add(new Label { Text = "Email:", AutoSize = true }, 0, 3);
            txtEmail = new TextBox { Width = 200 };
            panel.Controls.Add(txtEmail, 1, 3);

            panel.Controls.Add(new Label { Text = "Password:", AutoSize = true }, 0, 4);
            txtPassword = new TextBox { Width = 200, PasswordChar = '•' };
            panel.Controls.Add(txtPassword, 1, 4);

            var btnRegister = new Button { Text = "Register", Width = 200, BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnRegister.Click += BtnRegister_Click;
            panel.Controls.Add(btnRegister, 0, 5);
            panel.SetColumnSpan(btnRegister, 2);

            lblStatus = new Label { AutoSize = true, ForeColor = Color.Red };
            panel.Controls.Add(lblStatus, 0, 6);
            panel.SetColumnSpan(lblStatus, 2);

            Controls.Add(panel);
        }

        private async void BtnRegister_Click(object? sender, EventArgs e)
        {
            lblStatus.Text = "Registering...";
            lblStatus.ForeColor = Color.Gray;

            var (success, content) = await _api.PostAsync("api/auth/register", new
            {
                userName = txtUserName.Text,
                displayName = txtDisplayName.Text,
                email = txtEmail.Text,
                password = txtPassword.Text
            });

            if (!success)
            {
                lblStatus.Text = "Registration failed. Check your inputs.";
                lblStatus.ForeColor = Color.Red;
                return;
            }

            var data = JsonSerializer.Deserialize<JsonElement>(content);
            _api.JwtToken = data.GetProperty("token").GetString() ?? "";
            var userId = data.GetProperty("userId").GetString() ?? "";
            var userName = data.GetProperty("userName").GetString() ?? "";
            var displayName = data.GetProperty("displayName").GetString() ?? "";

            LoginSucceeded?.Invoke(userId, userName, displayName);
            Close();
        }
    }
}
