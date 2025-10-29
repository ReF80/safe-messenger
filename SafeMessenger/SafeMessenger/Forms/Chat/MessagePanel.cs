using System;
using System.Drawing;
using System.Windows.Forms;

namespace TelegramStyleMessenger
{
    public class MessagePanel
    {
        ChatForm chatForm;
        
        public void AddMessage(string message, bool isSystem = false)
        {
            if (chatForm.messageContainer.InvokeRequired)
            {
                chatForm.messageContainer.Invoke(new Action<string, bool>(AddMessage), message, isSystem);
            }
            else
            {
                var messagePanel = chatForm.CreateMessagePanel(message, isSystem);
                chatForm.messageContainer.Controls.Add(messagePanel);
                chatForm.messageContainer.ScrollControlIntoView(messagePanel);
            }
        }
    }
}
