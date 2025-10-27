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

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            if (isConnecting) return;

            var startForm = new StartForm();
            startForm.Show();
            this.Hide();
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                btnBack.Enabled = false; 
                btnConnect.Text = "Подключение...";


                string connectionInfo = $"{txtIP.Text}:{txtPort.Text}";
                var chatForm = new ChatForm(false, connectionInfo, txtName.Text);

                chatForm.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                btnConnect.Enabled = true;
                btnBack.Enabled = true;
                btnConnect.Text = "Подключиться";

                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}