using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using Packet;
namespace Client
{
    public partial class PrivateChat : Form
    {
        string sendername;
        string receivername;
        string myip;
        string receiverip;
        string serverip;
        int myid;
        StickerForm stickerform;
        bool isOpenStickerForm = false;
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
            CheckForIllegalCrossThreadCalls = false;
            stickerform = new StickerForm(ptbxSticker.Location.X + 70, ptbxSticker.Location.Y - 150);
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
                SocketPacket messagetablepacket = DataTransferer.TcpSendAndReceive(serverip, 52056, new SocketPacket(PacketType.MESSAGETABLE, myip, serverip, 52052, 52054, sendername, receivername, "", 1));
                myid = Convert.ToInt32(messagetablepacket.Message);
                LoadMessage(messagetablepacket.MessageTable);
                SocketPacket friendinfo = DataTransferer.SendAndReceive(serverip, 52054, new SocketPacket(PacketType.REQUSERPROFILE, myip, serverip, 52052, 52054, receivername,sendername,"",1));
                receiverip = friendinfo.userinfo.userip;
            });
            
        }

        private void btnSend_MouseClick(object sender, MouseEventArgs e)
        {
            string message = customTextBox1.textBox1.Text;
            Task.Run(() =>
            {
                SocketPacket messagepacket = DataTransferer.SendAndReceive(serverip, 52054, new SocketPacket(PacketType.MESSAGE, myip, serverip, 52052, 52054, sendername, receivername, message, 1));
                UpdateMessage(messagepacket.MessageRow);
            });
            customTextBox1.textBox1.Clear();
        }
        private void LoadMessage(DataTable table)
        {
            for(int i = 0; i < table.Rows.Count;i++)
            {
                switch(Convert.ToInt32(table.Rows[i].Field<object>("messtype")))
                {
                    case 1:
                        UpdateMessageType1(table.Rows[i]);
                        break;
                    case 2:
                        UpdateMessageType2(table.Rows[i]);
                        break;
                    case 3:
                        UpdateMessageType3(table.Rows[i]);
                        break;
                }
            }
        }

        private void UpdateMessage(DataTable table)
        {
            DataRow row = table.Rows[0];
            switch(Convert.ToInt32(row.Field<object>("messtype")))
            {
                case 1:
                    UpdateMessageType1(row);
                    break;
                case 2:
                    UpdateMessageType2(row);
                    break;
                case 3:
                    UpdateMessageType3(row);
                    break;
            }

        }
        private void UpdateMessageType2(DataRow row)
        {
            if (Convert.ToInt32(row.Field<object>("senderid")) == myid)
            {
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Blue;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", sendername, Convert.ToDateTime(row.Field<object>("timesent")).ToString()));
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                Image image = new Bitmap(Image.FromFile(string.Format(@"Emoji\{0}", row.Field<string>("sticker"))), new Size(75, 75));
                Thread thread = new Thread(() => PasteImage(image));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                customRichTextBox1.richTextBox1.AppendText("\n");
            }

            else
            {
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Purple;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", receivername, Convert.ToDateTime(row.Field<object>("timesent")).ToString()));
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                Image image = new Bitmap(Image.FromFile(string.Format(@"Emoji\{0}", row.Field<string>("sticker"))),new Size(75,75));
                Thread thread = new Thread(() => PasteImage(image));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                customRichTextBox1.richTextBox1.AppendText("\n");
            }
        }
        private void UpdateMessageType1(DataRow row)
        {
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
        private void UpdateMessageType3(DataRow row)
        {
            if (Convert.ToInt32(row.Field<object>("senderid")) == myid)
            {
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Blue;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", sendername, Convert.ToDateTime(row.Field<object>("timesent")).ToString()));
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                Image image = byteArrayToImage(row.Field<byte[]>("image"));
                Thread thread = new Thread(() => PasteImage(image));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                customRichTextBox1.richTextBox1.AppendText("\n");
            }

            else
            {
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                customRichTextBox1.richTextBox1.SelectionColor = Color.Purple;
                customRichTextBox1.richTextBox1.SelectionFont = new Font("Helvetica Neue", 10, FontStyle.Bold);
                customRichTextBox1.richTextBox1.AppendText(string.Format("{0} ({1}):\n", receivername, Convert.ToDateTime(row.Field<object>("timesent")).ToString()));
                customRichTextBox1.richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                Image image = byteArrayToImage(row.Field<byte[]>("image"));
                Thread thread = new Thread(() => PasteImage(image));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                customRichTextBox1.richTextBox1.AppendText("\n");
            }
        }
        private void PasteImage(Image image)
        {
            customRichTextBox1.richTextBox1.ReadOnly = false;
            Clipboard.SetImage(image);
            customRichTextBox1.richTextBox1.Paste();
            customRichTextBox1.richTextBox1.ReadOnly = true;
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

        private void ptbxImage_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(open.FileName);
                Task.Run(() =>
                {
                    SocketPacket returnpacket = DataTransferer.TcpSendAndReceive(serverip, 52056, new SocketPacket(PacketType.IMG, myip, serverip, 52055, 52056, sendername, receivername, image));
                    UpdateMessage(returnpacket.MessageRow);
                });
            }
        }
        Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void ptbxSticker_MouseClick(object sender, MouseEventArgs e)
        {
            if (isOpenStickerForm == true)
            {
                stickerform.Hide();
                isOpenStickerForm = false;

            }
            else
            {
                Task.Run(() =>
                {
                    isOpenStickerForm = true;
                    stickerform.ShowDialog();
                    if (stickerform.stickername != "")
                    {
                        SocketPacket returnpacket = DataTransferer.SendAndReceive(serverip, 52054, new SocketPacket(PacketType.STICKER, myip, serverip, 52052, 52054, sendername, receivername, stickerform.stickername, 2));
                        UpdateMessage(returnpacket.MessageRow);
                    }
                });
            }
        }

        private void ptbxGame_MouseClick(object sender, MouseEventArgs e)
        {
            Caro caro = new Caro(myip, receiverip);
        }
    }
}
