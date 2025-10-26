using System;
using System.Drawing;
using System.Windows.Forms;

namespace TelegramStyleMessenger
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            ApplyModernStyle();
        }

        private void ApplyModernStyle()
        {
            // Стиль для кнопок
            btnCreateChat.BackColor = Color.FromArgb(0, 136, 204);
            btnCreateChat.ForeColor = Color.White;
            btnCreateChat.FlatAppearance.BorderSize = 0;
            btnCreateChat.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 116, 184);
            btnCreateChat.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 96, 164);

            btnJoinChat.BackColor = Color.FromArgb(0, 136, 204);
            btnJoinChat.ForeColor = Color.White;
            btnJoinChat.FlatAppearance.BorderSize = 0;
            btnJoinChat.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 116, 184);
            btnJoinChat.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 96, 164);

            // Темная тема
            this.BackColor = Color.FromArgb(32, 32, 32);
            panelMain.BackColor = Color.FromArgb(32, 32, 32);
            lblTitle.ForeColor = Color.White;
            lblFooter.ForeColor = Color.Gray;
        }

        private void btnCreateChat_Click(object sender, EventArgs e)
        {
            using (var nameDialog = new ModernInputDialog("Создание чата", "Введите ваше имя:"))
            {
                if (nameDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(nameDialog.InputText))
                    {
                        var chatForm = new ChatForm(true, "", nameDialog.InputText);
                        chatForm.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void btnJoinChat_Click(object sender, EventArgs e)
        {
            var joinForm = new JoinForm();
            joinForm.Show();
            this.Hide();
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}