using System;
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

        private void btnCreateChat_Click(object sender, EventArgs e)
        {
            using (var nameDialog = new ModernInputDialog("Создание чата", "Введите ваше имя:"))
            {
                nameDialog.ShowDialog();
                if (!string.IsNullOrWhiteSpace(nameDialog.InputText)) //  && nameDialog.ShowDialog() == DialogResult.OK
                {
                    var chatForm = new ChatForm(true, "", nameDialog.InputText);
                    chatForm.Show();
                    this.Hide();
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