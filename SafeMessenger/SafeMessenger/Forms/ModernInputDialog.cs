using System;
using System.Drawing;
using System.Windows.Forms;

namespace TelegramStyleMessenger
{
    public partial class ModernInputDialog : Form
    {
        private TextBox txtInput;
        private Button btnOk;
        private Button btnCancel;
        private Label lblPrompt;

        public string InputText => txtInput.Text;

        public ModernInputDialog(string title, string prompt)
        {
            InitializeComponent(title, prompt);
            ApplyModernStyle();
        }

        private void InitializeComponent(string title, string prompt)
        {
            this.Text = title;
            this.Size = new Size(350, 200);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblPrompt = new Label();
            lblPrompt.Text = prompt;
            lblPrompt.Font = new Font("Segoe UI", 10);
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new Point(20, 20);

            txtInput = new TextBox();
            txtInput.Size = new Size(290, 35);
            txtInput.Location = new Point(20, 60);
            txtInput.Font = new Font("Segoe UI", 10);

            btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Size = new Size(120, 35);
            btnOk.Location = new Point(60, 110);
            btnOk.DialogResult = DialogResult.OK;

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Size = new Size(120, 35);
            btnCancel.Location = new Point(190, 110);
            btnCancel.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { lblPrompt, txtInput, btnOk, btnCancel });
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void ApplyModernStyle()
        {
            this.BackColor = Color.FromArgb(32, 32, 32);

            lblPrompt.ForeColor = Color.White;

            txtInput.BackColor = Color.FromArgb(50, 50, 50);
            txtInput.ForeColor = Color.White;
            txtInput.BorderStyle = BorderStyle.FixedSingle;

            var buttonStyle = new Action<Button>((btn) => {
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 10);
                btn.BackColor = Color.FromArgb(0, 136, 204);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.Cursor = Cursors.Hand;
            });

            buttonStyle(btnOk);

            btnCancel.BackColor = Color.FromArgb(60, 60, 60);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
        }
    }

    public class ModernMessageBox : Form
    {
        public ModernMessageBox(string title, string message)
        {
            InitializeComponent(title, message);
            ApplyModernStyle();
        }

        private void InitializeComponent(string title, string message)
        {
            this.Text = title;
            this.Size = new Size(300, 180);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblMessage = new Label();
            lblMessage.Text = message;
            lblMessage.Font = new Font("Segoe UI", 10);
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(20, 30);
            lblMessage.MaximumSize = new Size(260, 0);

            var btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Size = new Size(80, 35);
            btnOk.Location = new Point(110, 100);
            btnOk.DialogResult = DialogResult.OK;

            this.Controls.AddRange(new Control[] { lblMessage, btnOk });
            this.AcceptButton = btnOk;
        }

        private void ApplyModernStyle()
        {
            this.BackColor = Color.FromArgb(32, 32, 32);

            var lblMessage = (Label)this.Controls[0];
            lblMessage.ForeColor = Color.White;

            var btnOk = (Button)this.Controls[1];
            btnOk.FlatStyle = FlatStyle.Flat;
            btnOk.BackColor = Color.FromArgb(0, 136, 204);
            btnOk.ForeColor = Color.White;
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Cursor = Cursors.Hand;
        }
    }
}