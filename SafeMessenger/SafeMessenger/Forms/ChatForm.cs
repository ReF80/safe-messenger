using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
// flag
namespace TelegramStyleMessenger
{
    public partial class ChatForm : Form
    {
        private bool isServer;
        private string connectionInfo;
        private string userName;

        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;

        private Thread listenThread;
        private bool isConnected;
        private CancellationTokenSource cancellationTokenSource;

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
            InitializeComponent();
            ApplyModernStyle();
            _ = InitializeChatAsync();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

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

        private void ListenForClients()
        {
            while (true)
            {
                try
                {
                    client = server.AcceptTcpClient();
                    stream = client.GetStream();
                    isConnected = true;

                    AddMessage("Клиент подключился!", true);
                    UpdateStatus("Подключено");

                    // Запускаем поток для прослушивания сообщений
                    Thread clientThread = new Thread(new ThreadStart(ListenForMessages));
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    break;
                }
                catch
                {
                    break;
                }
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

                // Отправляем имя пользователя
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
                                ReceiveFile(message.Substring(5));
                            }
                            else
                            {
                                AddMessage(message, false);
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

        private void ReceiveFile(string fileInfo)
        {
            try
            {
                string[] parts = fileInfo.Split('|');
                if (parts.Length != 3) return;

                string fileName = parts[0];
                long fileSize = long.Parse(parts[1]);
                int dataSize = int.Parse(parts[2]);

                byte[] fileData = new byte[dataSize];
                int bytesRead = 0;
                while (bytesRead < dataSize)
                {
                    int read = stream.Read(fileData, bytesRead, dataSize - bytesRead);
                    if (read == 0) break;
                    bytesRead += read;
                }

                if (InvokeRequired)
                {
                    Invoke(new Action<string>(f => {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.FileName = fileName;
                            saveDialog.Filter = "All files (*.*)|*.*";

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                File.WriteAllBytes(saveDialog.FileName, fileData);
                                AddMessage($"Файл {fileName} получен и сохранен", true);
                            }
                        }
                    }), fileName);
                }
            }
            catch (Exception ex)
            {
                AddMessage($"Ошибка получения файла: {ex.Message}", true);
            }
        }

        private void AddMessage(string message, bool isSystem = false)
        {
            if (messageContainer.InvokeRequired)
            {
                messageContainer.Invoke(new Action<string, bool>(AddMessage), message, isSystem);
            }
            else
            {
                var messagePanel = CreateMessagePanel(message, isSystem);
                messageContainer.Controls.Add(messagePanel);
                messageContainer.ScrollControlIntoView(messagePanel);
            }
        }

        private Panel CreateMessagePanel(string message, bool isSystem)
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Все файлы (*.*)|*.*";
                openDialog.Title = "Выберите файл для отправки";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    SendFile(openDialog.FileName);
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

        private void SendMessage()
        {
            if (!string.IsNullOrWhiteSpace(txtMessage.Text) && isConnected)
            {
                string message = $"{userName}: {txtMessage.Text}";
                SendMessageToServer(message);
                AddMessage(txtMessage.Text, false);
                txtMessage.Clear();
            }
        }

        private void SendFile(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 10 * 1024 * 1024) // 10MB limit
                {
                    AddMessage("Файл слишком большой (максимум 10MB)", true);
                    return;
                }

                byte[] fileData = File.ReadAllBytes(filePath);

                // Отправляем информацию о файле
                string fileInfoMessage = $"FILE:{fileInfo.Name}|{fileInfo.Length}|{fileData.Length}\n";
                byte[] infoData = Encoding.UTF8.GetBytes(fileInfoMessage);
                stream.Write(infoData, 0, infoData.Length);

                // Отправляем данные файла
                stream.Write(fileData, 0, fileData.Length);
                stream.Flush();

                AddMessage($"Отправлен файл: {fileInfo.Name}", true);
            }
            catch (Exception ex)
            {
                AddMessage($"Ошибка отправки файла: {ex.Message}", true);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // Корректное закрытие ресурсов
            isConnected = false;
            cancellationTokenSource?.Cancel();

            try
            {
                stream?.Close();
                client?.Close();
                server?.Stop();
            }
            catch
            {
                // Игнорируем ошибки при закрытии
            }
        }
    }
}