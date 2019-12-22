using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Packet;
using Networking;
namespace Client
{
    public partial class PrivateChat : Form
    {
        string sendername;
        string receivername;
        string myip;
        string serverip;
        int myid;
        public PrivateChat()
        {
            InitializeComponent();
        }
        public PrivateChat(MainForm form,string receiverName)
        {
            InitializeComponent();
            sendername = form.name;
            receivername = receiverName;
            myip = MainForm.getlocalIP();
            serverip = form.serverip;
            form.UpdateMessageRequest += Form_UpdateMessageRequest;
            label1.Text += receiverName;
        }

        private void Form_UpdateMessageRequest(object sender, UpdateEventArgs e)
        {
            if (e.userid == myid)
                UpdateMessage(e.newMessage);
        }

        private void customRichTextBox1_richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PrivateChat_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                SocketPacket messagetablepacket = DataTranferer.SendAndReceive(serverip, 52054, new SocketPacket(PacketType.MESSAGETABLE, myip, serverip, 52052, 52054, sendername, receivername, "", 1));
                myid = Convert.ToInt32(messagetablepacket.Message);
                LoadMessage(messagetablepacket.MessageTable);
            });
        }

        private void btnSend_MouseClick(object sender, MouseEventArgs e)
        {
            string message = customTextBox1.textBox1.Text;
            Task.Run(() =>
            {
                SocketPacket messagepacket = DataTranferer.SendAndReceive(serverip, 52054, new SocketPacket(PacketType.MESSAGE, myip, serverip, 52052, 52054, sendername, receivername, message, 1));
                UpdateMessage(messagepacket.MessageRow);
            });
            customTextBox1.textBox1.Clear();
        }
        private void LoadMessage(DataTable table)
        {
            for(int i = 0; i < table.Rows.Count;i++)
            {
                if(Convert.ToInt32(table.Rows[i].Field<object>("senderid"))==myid)
                {
                    customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    customRichTextBox1.richTextBox1.SelectionColor = Color.Blue;
                    customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                    customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", sendername, Convert.ToDateTime(table.Rows[i].Field<object>("timesent")).ToString()));
                    customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    customRichTextBox1.richTextBox1.SelectionColor = Color.Blue;
                    customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Regular);
                    customRichTextBox1.richTextBox1.AppendText(string.Format("{0}\n", table.Rows[i].Field<string>("content")));
                }
                    
                else
                {
                    customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    customRichTextBox1.richTextBox1.SelectionColor = Color.Purple;
                    customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                    customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", receivername, Convert.ToDateTime(table.Rows[i].Field<object>("timesent")).ToString()));
                    customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    customRichTextBox1.richTextBox1.SelectionColor = Color.Purple;
                    customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Regular);
                    customRichTextBox1.richTextBox1.AppendText(string.Format("{0}\n", table.Rows[i].Field<string>("content")));
                }
            }
        }
        private void UpdateMessage(DataTable table)
        {
            DataRow row = table.Rows[0];
            if (Convert.ToInt32(row.Field<object>("senderid")) == myid)
            {
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Blue;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", sendername, Convert.ToDateTime(row.Field<object>("timesent")).ToString()));
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Blue;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Regular);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0}\n", row.Field<string>("content")));
            }

            else
            {
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Purple;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", receivername, Convert.ToDateTime(row.Field<object>("timesent")).ToString()));
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Purple;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Regular);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0}\n", row.Field<string>("content")));
            }
        }

        private void pnChat_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void ptbxClose_MouseEnter(object sender, EventArgs e)
        {
            ptbxClose.BackColor = Color.Gainsboro;
        }

        private void ptbxClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ptbxClose_MouseLeave(object sender, EventArgs e)
        {
            ptbxClose.BackColor = Color.Transparent;
        }
    }
}
