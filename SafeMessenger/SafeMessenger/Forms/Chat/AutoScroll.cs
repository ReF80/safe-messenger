using System;
using System.Drawing;
using System.Windows.Forms;
using TelegramStyleMessenger;

namespace SafeMessenger.Forms.Chat
{
    public class AutoScroll
    {
        ChatForm form;

        public AutoScroll(ChatForm chatForm)
        {
            form = chatForm;    
        }
        public void MessageContainerScroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.ThumbTrack || e.Type == ScrollEventType.ThumbPosition)
            {
                CheckAutoScrollStatus();
            }
        }

        public void CheckAutoScrollStatus()
        {
            if (!form.messageContainer.VerticalScroll.Visible)
            {
                form.autoScrollEnabled = true;
                form.btnScrollToBottom.Visible = false;
                return;
            }

            int currentScroll = form.messageContainer.VerticalScroll.Value;
            int maxScroll = form.messageContainer.VerticalScroll.Maximum - form.messageContainer.ClientSize.Height;

            form.autoScrollEnabled = (maxScroll - currentScroll) <= 20;
            form.btnScrollToBottom.Visible = !form.autoScrollEnabled;
        }

        public void BtnScrollToBottomClick(object sender, EventArgs e)
        {
            form.autoScrollEnabled = true;
            ScrollToBottom();
            form.btnScrollToBottom.Visible = false;
        }

        public void ScrollToBottom()
        {
            if (form.messageContainer.InvokeRequired)
            {
                form.messageContainer.Invoke(new Action(ScrollToBottom));
                return;
            }

            try
            {
                form.messageContainer.VerticalScroll.Value = form.messageContainer.VerticalScroll.Maximum;
                form.messageContainer.PerformLayout();
            }
            catch (Exception ex)
            {

            }
        }

        public const int WM_VSCROLL = 0x115;
        public const int SB_BOTTOM = 7;

        public void UpdateScrollButtonPos()
        {
            if (form.btnScrollToBottom != null && form.panelChat != null)
            {
                form.btnScrollToBottom.Location = new Point(
                    form.panelChat.Width - form.btnScrollToBottom.Width - 10,
                    form.panelChat.Height - form.btnScrollToBottom.Height - 10
                );
            }
        }

    }
}
