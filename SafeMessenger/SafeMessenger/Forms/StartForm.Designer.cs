using System.Drawing;
using System.Windows.Forms;

namespace TelegramStyleMessenger
{
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblFooter = new System.Windows.Forms.Label();
            this.btnJoinChat = new System.Windows.Forms.Button();
            this.btnCreateChat = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblFooter);
            this.panelMain.Controls.Add(this.btnJoinChat);
            this.panelMain.Controls.Add(this.btnCreateChat);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(400, 500);
            this.panelMain.TabIndex = 0;
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter.Location = new System.Drawing.Point(120, 400);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(169, 15);
            this.lblFooter.TabIndex = 3;
            this.lblFooter.Text = "Secure messaging • Version 1.0";
            // 
            // btnJoinChat
            // 
            this.btnJoinChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinChat.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnJoinChat.Location = new System.Drawing.Point(80, 250);
            this.btnJoinChat.Name = "btnJoinChat";
            this.btnJoinChat.Size = new System.Drawing.Size(240, 45);
            this.btnJoinChat.TabIndex = 2;
            this.btnJoinChat.Text = "Присоединиться к чату";
            this.btnJoinChat.UseVisualStyleBackColor = true;
            this.btnJoinChat.Click += new System.EventHandler(this.btnJoinChat_Click);
            // 
            // btnCreateChat
            // 
            this.btnCreateChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateChat.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCreateChat.Location = new System.Drawing.Point(80, 180);
            this.btnCreateChat.Name = "btnCreateChat";
            this.btnCreateChat.Size = new System.Drawing.Size(240, 45);
            this.btnCreateChat.TabIndex = 1;
            this.btnCreateChat.Text = "Создать новый чат";
            this.btnCreateChat.UseVisualStyleBackColor = true;
            this.btnCreateChat.Click += new System.EventHandler(this.btnCreateChat_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(80, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Safe  Messenger";
            // 
            // StartForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Safe  Messenger";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartForm_FormClosed);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelMain;
        private Label lblTitle;
        private Button btnCreateChat;
        private Button btnJoinChat;
        private Label lblFooter;

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
    }

}