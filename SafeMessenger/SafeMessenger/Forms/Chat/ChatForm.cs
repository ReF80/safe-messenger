using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SafeMessenger.Forms.Algorithms;
using SafeMessenger.Forms.Chat;

namespace TelegramStyleMessenger
{
    public partial class ChatForm : Form
    {
        private bool isServer;
        private string connectionInfo;
        public string userName;

        private TcpListener server;
        private TcpClient client;
        public NetworkStream stream;

        private Thread listenThread;
        public bool isConnected;
        private CancellationTokenSource cancellationTokenSource;

        private MessagePanel messagePanel;
        Kuznechik kuznechik = new Kuznechik();
        AutoScroll autoScroll;
        SendReceiveFile file;

        public bool autoScrollEnabled = true;
        public const int SCROLL_BUFFER = 20;
        public Button btnScrollToBottom;


        public ChatForm(bool isServer, string connectionInfo, string userName = "")
        {
            this.isServer = isServer;
            this.connectionInfo = connectionInfo;
            this.userName = userName;

            if (string.IsNullOrEmpty(userName))
            {
                this.userName = "User_" + DateTime.Now.ToString("HHmmss");
            }
            cancellationTokenSource = new CancellationTokenSource();
            autoScroll = new AutoScroll(this);
            file = new SendReceiveFile(this);
            InitializeComponent();
            ApplyModernStyle();
            InitializeAutoScroll();
            _ = InitializeChatAsync();
        }

        private async Task InitializeChatAsync()
        {
            try
            {
                if (isServer)
                {
                    await StartServerAsync();
                }
                else
                {
                    await ConnectToServerAsync();
                }
            }
            catch (Exception ex)
            {
                AddMessage($"Ошибка инициализации: {ex.Message}", true);
            }
        }

        private async Task StartServerAsync()
        {
            try
            {
                int port = 12345;
                server = new TcpListener(IPAddress.Any, port);
                server.Start();

                // Получаем IP адрес
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                string ip = "";
                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ip = address.ToString();
                        break;
                    }
                }

                AddMessage($"Сервер запущен. Ваш IP: {ip}, Порт: {port}", true);
                AddMessage("Сообщите эти данные другому пользователю для подключения", true);

                UpdateStatus("Ожидание подключения...");

                // Асинхронное ожидание клиента
                client = await server.AcceptTcpClientAsync();
                stream = client.GetStream();
                isConnected = true;

                AddMessage("Клиент подключился!", true);
                UpdateStatus("Подключено");

                // Запускаем поток для прослушивания сообщений
                listenThread = new Thread(new ThreadStart(ListenForMessages));
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                AddMessage($"Ошибка запуска сервера: {ex.Message}", true);
            }
        }

        private async Task ConnectToServerAsync()
        {
            try
            {
                string[] parts = connectionInfo.Split(':');
                if (parts.Length != 2)
                {
                    AddMessage("Неверный формат подключения. Используйте IP:Порт", true);
                    return;
                }

                string ip = parts[0];
                int port = int.Parse(parts[1]);

                UpdateStatus("Подключение...");

                // Асинхронное подключение с таймаутом
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
                var connectTask = Task.Run(() =>
                {
                    client = new TcpClient();
                    client.Connect(ip, port);
                });

                var completedTask = await Task.WhenAny(connectTask, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    AddMessage("Таймаут подключения. Сервер не отвечает.", true);
                    return;
                }
                   
                stream = client.GetStream();
                isConnected = true;

                AddMessage($"Подключено к {ip}:{port}", true);
                UpdateStatus("Подключено");
                SendMessageToServer($"{userName} присоединился к чату");

                // Запускаем поток для прослушивания сообщений
                listenThread = new Thread(new ThreadStart(ListenForMessages));
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            catch (Exception ex)
            {
                AddMessage($"Ошибка подключения: {ex.Message}", true);
                UpdateStatus("Ошибка подключения");
            }
        }

        private void ListenForMessages()
        {
            byte[] buffer = new byte[4096];
            StringBuilder messageBuilder = new StringBuilder();

            while (isConnected)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuilder.Append(data);

                    // Обрабатываем полные сообщения
                    string fullMessage = messageBuilder.ToString();
                    string[] messages = fullMessage.Split('\n');

                    for (int i = 0; i < messages.Length - 1; i++)
                    {
                        string message = messages[i].Trim();
                        if (!string.IsNullOrEmpty(message))
                        {
                            if (message.StartsWith("FILE:"))
                            {
                                file.ReceiveFile(message.Substring(5));
                            }
                            else
                            {
                                //Decript message
                                var decriptMessange = kuznechik.DecriptMessange(message);
                                AddMessage(decriptMessange, false);
                                //Добавить запись в файл двух сообщений и его отправка
                            }
                        }
                    }

                    messageBuilder.Clear();
                    if (messages.Length > 0)
                    {
                        messageBuilder.Append(messages[messages.Length - 1]);
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        private void SendMessageToServer(string message)
        {
            if (!isConnected || stream == null) return;

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
            catch (Exception ex)
            {
                AddMessage($"Ошибка отправки: {ex.Message}", true);
            }
        }

        public void AddMessage(string message, bool isSystem = false)
        {
            if (messageContainer.InvokeRequired)
            {
                messageContainer.Invoke(new Action<string, bool>(AddMessage), message, isSystem);
            }
            else
            {
                try
                {
                    messageContainer.SuspendLayout();

                    var messagePanel = CreateMessagePanel(message, isSystem);
                    messageContainer.Controls.Add(messagePanel);

                    if (autoScrollEnabled)
                    {
                        autoScroll.ScrollToBottom();
                    }

                    messageContainer.ResumeLayout(true);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public Panel CreateMessagePanel(string message, bool isSystem)
        {
            var panel = new Panel();
            panel.AutoSize = true;
            panel.MaximumSize = new Size(350, 0);
            panel.Padding = new Padding(10, 8, 10, 8);
            panel.Margin = new Padding(5, 2, 5, 2);

            var label = new Label();
            label.AutoSize = true;
            label.MaximumSize = new Size(300, 0);
            label.Text = message;
            label.Font = new Font("Segoe UI", 10);
            label.Padding = new Padding(12, 8, 12, 8);

            if (isSystem)
            {
                panel.BackColor = Color.FromArgb(60, 60, 60);
                label.ForeColor = Color.LightGray;
                label.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            }
            else if (message.StartsWith(userName + ":"))
            {
                panel.BackColor = Color.FromArgb(0, 92, 163);
                label.ForeColor = Color.White;
                panel.Dock = DockStyle.Right;
            }
            else
            {
                panel.BackColor = Color.FromArgb(50, 50, 50);
                label.ForeColor = Color.White;
                panel.Dock = DockStyle.Left;
            }

            panel.Controls.Add(label);
            return panel;
        }

        private void SendMessage()
        {
            if (!string.IsNullOrWhiteSpace(txtMessage.Text) && isConnected)
            {
                string message = $"{userName}: {txtMessage.Text}";
                //Encript messange 
                SendMessageToServer(kuznechik.EncriptMessange(message));
                //Добавить запись в файл двух сообщений и его отправка
                AddMessage(txtMessage.Text, false);
                txtMessage.Clear();
            }
        }

        private void btnSend_Click(object sender, EventArgs e) => SendMessage();

        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Все файлы (*.*)|*.*";
                openDialog.Title = "Выберите файл для отправки";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    file.SendFile(openDialog.FileName);
                }
            }
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !ModifierKeys.HasFlag(Keys.Shift))
            {
                e.Handled = true;
                SendMessage();
            }
        }

        private void UpdateStatus(string status)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action<string>(UpdateStatus), status);
            }
            else
            {
                lblStatus.Text = status;
                lblTitle.Text = isServer ? "Созданный чат" : "Подключенный чат";
            }
        }

        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();
        private void MessageContainer_MouseWheel(object sender, MouseEventArgs e) => autoScroll.CheckAutoScrollStatus();
        private void MessageContainer_MouseEnter(object sender, EventArgs e) => autoScroll.CheckAutoScrollStatus();
        private void MessageContainer_Resize(object sender, EventArgs e) => autoScroll.UpdateScrollButtonPos();
        private void MessageContainer_Scroll(object sender, ScrollEventArgs e) => autoScroll.MessageContainerScroll(sender, e);
        public void UpdateScrollButtonPosition() => autoScroll.UpdateScrollButtonPos();

        private void BtnScrollToBottom_Click(object sender, EventArgs e) => autoScroll.BtnScrollToBottomClick(sender, e);
                [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}