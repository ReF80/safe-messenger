using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TelegramStyleMessenger
{
    public partial class JoinForm : Form
    {
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtIP.Text) ||
                string.IsNullOrWhiteSpace(txtPort.Text))
            {
                ShowModernMessage("Заполните все поля");
                return;
            }

            string connectionInfo = $"{txtIP.Text}:{txtPort.Text}";
            var chatForm = new ChatForm(false, connectionInfo, txtName.Text);
            chatForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
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
    }
}