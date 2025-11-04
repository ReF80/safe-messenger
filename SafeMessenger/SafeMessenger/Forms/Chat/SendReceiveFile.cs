using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TelegramStyleMessenger;

namespace SafeMessenger.Forms.Chat
{
    public class SendReceiveFile
    {
        ChatForm form;
        public SendReceiveFile(ChatForm chatForm) 
        { 
            form = chatForm;
        }

        public void ReceiveFile(string fileInfo)
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
                    int read = form.stream.Read(fileData, bytesRead, dataSize - bytesRead);
                    if (read == 0) break;
                    bytesRead += read;
                }

                if (form.InvokeRequired)
                {
                    form.Invoke(new Action<string>(f => {
                        using (SaveFileDialog saveDialog = new SaveFileDialog())
                        {
                            saveDialog.FileName = fileName;
                            saveDialog.Filter = "All files (*.*)|*.*";

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                File.WriteAllBytes(saveDialog.FileName, fileData);
                                form.AddMessage($"Файл {fileName} получен и сохранен", true);
                            }
                        }
                    }), fileName);
                }
            }
            catch (Exception ex)
            {
                form.AddMessage($"Ошибка получения файла: {ex.Message}", true);
            }
        }

        public void SendFile(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 10 * 1024 * 1024)
                {
                    form.AddMessage("Файл слишком большой (максимум 10MB)", true);
                    return;
                }

                byte[] fileData = File.ReadAllBytes(filePath);

                string fileInfoMessage = $"FILE:{fileInfo.Name}|{fileInfo.Length}|{fileData.Length}\n";
                byte[] infoData = Encoding.UTF8.GetBytes(fileInfoMessage);
                form.stream.Write(infoData, 0, infoData.Length);
                form.stream.Write(fileData, 0, fileData.Length);
                form.stream.Flush();

                form.AddMessage($"Отправлен файл: {fileInfo.Name}", true);
            }
            catch (Exception ex)
            {
                form.AddMessage($"Ошибка отправки файла: {ex.Message}", true);
            }
        }
    }
}
