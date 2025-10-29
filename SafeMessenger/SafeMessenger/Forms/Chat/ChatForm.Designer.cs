using System.Drawing;
using System;
using System.Windows.Forms;

namespace TelegramStyleMessenger
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelChat = new System.Windows.Forms.Panel();
            this.messageContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panelInput = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.panelHeader.SuspendLayout();
            this.panelChat.SuspendLayout();
            this.panelInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblStatus);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(484, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(15, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(94, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Подключение...";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Telegram Chat";
            // 
            // panelChat
            // 
            this.panelChat.Controls.Add(this.messageContainer);
            this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChat.Location = new System.Drawing.Point(0, 60);
            this.panelChat.Name = "panelChat";
            this.panelChat.Padding = new System.Windows.Forms.Padding(10);
            this.panelChat.Size = new System.Drawing.Size(484, 351);
            this.panelChat.TabIndex = 1;
            // 
            // messageContainer
            // 
            this.messageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.messageContainer.Location = new System.Drawing.Point(10, 10);
            this.messageContainer.Name = "messageContainer";
            this.messageContainer.Size = new System.Drawing.Size(464, 331);
            this.messageContainer.TabIndex = 0;
            this.messageContainer.WrapContents = false;
            // 
            // panelInput
            // 
            this.panelInput.Controls.Add(this.btnAttach);
            this.panelInput.Controls.Add(this.btnSend);
            this.panelInput.Controls.Add(this.txtMessage);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelInput.Location = new System.Drawing.Point(0, 411);
            this.panelInput.Name = "panelInput";
            this.panelInput.Padding = new System.Windows.Forms.Padding(10);
            this.panelInput.Size = new System.Drawing.Size(484, 60);
            this.panelInput.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSend.Location = new System.Drawing.Point(439, 13);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(35, 35);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "➤";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttach.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnAttach.Location = new System.Drawing.Point(398, 13);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(35, 35);
            this.btnAttach.TabIndex = 1;
            this.btnAttach.Text = "📎";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMessage.Location = new System.Drawing.Point(10, 13);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(378, 35);
            this.txtMessage.TabIndex = 0;
            this.txtMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMessage_KeyPress);
            // 
            // ChatForm
            // 
            this.ClientSize = new System.Drawing.Size(484, 471);
            this.Controls.Add(this.panelChat);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Telegram Messenger";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelChat.ResumeLayout(false);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.ResumeLayout(false);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatForm_FormClosed);
        }


        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblStatus;
        private Panel panelChat;
        public FlowLayoutPanel messageContainer;
        private Panel panelInput;
        private TextBox txtMessage;
        private Button btnSend;
        private Button btnAttach;

        private void ApplyModernStyle()
        {
            // Темная тема
            this.BackColor = Color.FromArgb(32, 32, 32);
            panelHeader.BackColor = Color.FromArgb(42, 42, 42);
            panelChat.BackColor = Color.FromArgb(32, 32, 32);
            panelInput.BackColor = Color.FromArgb(42, 42, 42);
            messageContainer.BackColor = Color.FromArgb(32, 32, 32);

            // Заголовок
            lblTitle.ForeColor = Color.White;
            lblStatus.ForeColor = Color.LightGray;

            // Текстовое поле
            txtMessage.BackColor = Color.FromArgb(50, 50, 50);
            txtMessage.ForeColor = Color.White;
            txtMessage.BorderStyle = BorderStyle.FixedSingle;

            txtMessage.Enter += (s, e) => {
                txtMessage.BackColor = Color.FromArgb(70, 70, 70);
            };
            txtMessage.Leave += (s, e) => {
                txtMessage.BackColor = Color.FromArgb(50, 50, 50);
            };

            // Кнопки
            btnSend.BackColor = Color.FromArgb(0, 136, 204);
            btnSend.ForeColor = Color.White;
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 116, 184);
            btnSend.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 96, 164);

            btnAttach.BackColor = Color.FromArgb(60, 60, 60);
            btnAttach.ForeColor = Color.White;
            btnAttach.FlatAppearance.BorderSize = 0;
            btnAttach.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
        }
    }
}