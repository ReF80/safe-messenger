using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TelegramStyleMessenger
{
    public partial class JoinForm : Form
    {
        private bool isConnecting = false;

        public JoinForm()
        {
            InitializeComponent();
            ApplyModernStyle();
        }

        private void ApplyModernStyle()
        {
            // Темная тема
            this.BackColor = Color.FromArgb(32, 32, 32);
            panelMain.BackColor = Color.FromArgb(32, 32, 32);
            lblTitle.ForeColor = Color.White;
            lblName.ForeColor = Color.White;
            lblIP.ForeColor = Color.White;
            lblPort.ForeColor = Color.White;

            // Стиль текстовых полей
            foreach (Control control in panelMain.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Color.FromArgb(50, 50, 50);
                    textBox.ForeColor = Color.White;
                    textBox.BorderStyle = BorderStyle.FixedSingle;

                    textBox.Enter += (s, e) => {
                        textBox.BackColor = Color.FromArgb(70, 70, 70);
                    };
                    textBox.Leave += (s, e) => {
                        textBox.BackColor = Color.FromArgb(50, 50, 50);
                    };
                }
            }

            // Стиль кнопок
            btnConnect.BackColor = Color.FromArgb(0, 136, 204);
            btnConnect.ForeColor = Color.White;
            btnConnect.FlatAppearance.BorderSize = 0;
            btnConnect.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 116, 184);
            btnConnect.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 96, 164);

            btnBack.BackColor = Color.FromArgb(60, 60, 60);
            btnBack.ForeColor = Color.White;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (isConnecting) return;

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtIP.Text) ||
                string.IsNullOrWhiteSpace(txtPort.Text))
            {
                ShowModernMessage("Заполните все поля");
                return;
            }

            isConnecting = true;
            btnConnect.Enabled = false;
            btnBack.Enabled = false;
            btnConnect.Text = "Подключение...";

            try
            {
                string connectionInfo = $"{txtIP.Text}:{txtPort.Text}";

                // Создаем форму чата, но не показываем сразу
                var chatForm = new ChatForm(false, connectionInfo, txtName.Text);

                // Пытаемся подключиться асинхронно
                bool connectionSuccess = await Task.Run(() => ConnectToChat(chatForm));

                if (connectionSuccess)
                {
                    chatForm.Show();
                    this.Hide();
                }
                else
                {
                    chatForm.Dispose();
                    ShowModernMessage("Не удалось подключиться к серверу. Проверьте IP и порт.");
                }
            }
            catch (Exception ex)
            {
                ShowModernMessage($"Ошибка подключения: {ex.Message}");
            }
            finally
            {
                isConnecting = false;
                btnConnect.Enabled = true;
                btnBack.Enabled = true;
                btnConnect.Text = "Подключиться";
            }
        }

        private bool ConnectToChat(ChatForm chatForm)
        {
            try
            {
                // Даем форме время на инициализацию
                System.Threading.Thread.Sleep(100);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (isConnecting) return;

            var startForm = new StartForm();
            startForm.Show();
            this.Hide();
        }

        private void ShowModernMessage(string message)
        {
            using (var msgForm = new ModernMessageBox("Внимание", message))
            {
                msgForm.ShowDialog();
            }
        }

        private void JoinForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtIP.Focus();
                e.Handled = true;
            }
        }

        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPort.Focus();
                e.Handled = true;
            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnConnect_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}