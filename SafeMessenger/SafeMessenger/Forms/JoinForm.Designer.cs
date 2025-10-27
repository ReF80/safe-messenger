using System.Drawing;
using System;
using System.Windows.Forms;

namespace TelegramStyleMessenger
{
    partial class JoinForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.button1);
            this.panelMain.Controls.Add(this.btnBack);
            this.panelMain.Controls.Add(this.btnConnect);
            this.panelMain.Controls.Add(this.txtPort);
            this.panelMain.Controls.Add(this.lblPort);
            this.panelMain.Controls.Add(this.txtIP);
            this.panelMain.Controls.Add(this.lblIP);
            this.panelMain.Controls.Add(this.txtName);
            this.panelMain.Controls.Add(this.lblName);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(400, 450);
            this.panelMain.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBack.Location = new System.Drawing.Point(80, 330);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(240, 35);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click_1);
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnConnect.Location = new System.Drawing.Point(80, 270);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(240, 45);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click_1);
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPort.Location = new System.Drawing.Point(80, 200);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(240, 25);
            this.txtPort.TabIndex = 6;
            this.txtPort.Text = "12345";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPort.Location = new System.Drawing.Point(80, 180);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(44, 19);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Порт:";
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIP.Location = new System.Drawing.Point(80, 140);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(240, 25);
            this.txtIP.TabIndex = 4;
            this.txtIP.Text = "127.0.0.1";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIP.Location = new System.Drawing.Point(80, 120);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(64, 19);
            this.lblIP.TabIndex = 3;
            this.lblIP.Text = "IP адрес:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(80, 80);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(240, 25);
            this.txtName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(80, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 19);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Ваше имя:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(80, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(236, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Подключение к чату";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.button1.Location = new System.Drawing.Point(80, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(240, 45);
            this.button1.TabIndex = 9;
            this.button1.Text = "Подключиться";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // JoinForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "JoinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подключение к чату";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panelMain;
        private Label lblTitle;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtIP;
        private Label lblIP;
        private TextBox txtPort;
        private Label lblPort;
        private Button btnConnect;
        private Button btnBack;

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

        private Button button1;
    }
}