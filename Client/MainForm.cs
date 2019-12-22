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
using System.Net.Sockets;
using System.Net;
using Packet;
using Networking;
using Client.CustomControl;
using System.Threading;
using System.Drawing.Drawing2D;
namespace Client
{
    public delegate void MessageEventHandler(object sender, UpdateEventArgs e);
    public partial class MainForm : Form
    {
        UdpClient receiver = new UdpClient(52053);
        public string serverip { get; private set; }
        public string name { get; private set; }
        List<Tiles> FriendListTiles = new List<Tiles>();
        public event MessageEventHandler UpdateMessageRequest;
        bool isConnected = false;
        public MainForm()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        public MainForm(string svip, string username)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            serverip = svip;
            name = username;

            //SocketPacket reqfriendpacket = new SocketPacket(PacketType.REQFRIEND, getlocalIP(), serverip, 52052, 52054);
            //DataTranferer.Send(serverip, 52054, reqfriendpacket);
            //ThreadStart receivethread = new ThreadStart(ReceiveFromServer);
            //Thread thread1 = new Thread(receivethread);
            //thread1.IsBackground = true;
            //thread1.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            Task.Run(() => ReceiveFromServer());
            label1.Text = name;
            SocketPacket reqfriendpacket = new SocketPacket(PacketType.REQFRIEND, getlocalIP(), serverip, 52052, 52054);
            while (true)
                if (isConnected)
                { Task.Delay(1000); break; }
            DataTranferer.Send(serverip, 52054, reqfriendpacket);

            ptbAvatar.BackgroundImage = CropImage(Image.FromFile("a.jpg"));
        }

        private void ptbClose_MouseEnter(object sender, EventArgs e)
        {
            this.ptbClose.BackColor = Color.FromArgb(207, 207, 207);
        }

        private void ptbClose_MouseLeave(object sender, EventArgs e)
        {
            this.ptbClose.BackColor = Color.Transparent;
        }

        private void ptbClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ptbMinimize_MouseEnter(object sender, EventArgs e)
        {
            this.ptbMinimize.BackColor = Color.FromArgb(207, 207, 207);
        }

        private void ptbMinimize_MouseLeave(object sender, EventArgs e)
        {
            this.ptbMinimize.BackColor = Color.Transparent;
        }

        private void ptbMinimize_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
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

        public static string getlocalIP()
        {
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }
        public void GetFriendList(string st)
        {
            FriendListTiles.Clear();
            string[] separator = { "\n" };
            string[] subst = st.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int FriendCount = Convert.ToInt32(subst[0]);
            for (int i = 0; i < FriendCount; ++i)
            {
                string[] subsubst = subst[i + 1].Split(':');
                //if (subsubst[1] == name)
                //continue;
                Tiles friendtiles = new Tiles();
                friendtiles.TilesName.Text = subsubst[0];
                friendtiles.TilesIP = subsubst[1];
                friendtiles.Location = new Point(30 + 164 * (i % 3), 0 + 177 * (i / 3));
                friendtiles.Size = new Size(114,107);
                friendtiles.MouseClick += delegate (object sender, MouseEventArgs e) { Friendtiles_MouseClick(sender, e, this, subsubst[0]); };
                FriendListTiles.Add(friendtiles);
                //this.Controls.Add(FriendListTiles[i]);
                this.Invoke((MethodInvoker)delegate
                {
                    this.pnlFriend.Controls.Add(FriendListTiles[i]);
                });
            }
        }

        private void Friendtiles_MouseClick(object sender, MouseEventArgs e, MainForm form, string receivername)
        {
            PrivateChat chat = new PrivateChat(form, receivername);
            chat.Show();
        }

        private void ReceiveFromServer()
        {
            while (true)
            {
                IPEndPoint serveriPEnd = new IPEndPoint(IPAddress.Parse(serverip), 52051);
                isConnected = true;
                byte[] data = receiver.Receive(ref serveriPEnd);
                SocketPacket returnpacket = SocketPacket.DeSerializedItem(data);
                //MessageBox.Show(returnpacket.Message);
                switch (returnpacket.packetType)
                {
                    case PacketType.REQFRIEND:
                        GetFriendList(returnpacket.Message);
                        break;

                    case PacketType.REQUPDATEMESS:
                        UpdateMessageRequest(this, new UpdateEventArgs(Int32.Parse(returnpacket.Message),returnpacket.MessageRow));
                        break;

                    case PacketType.UPDATEPROFILE:
                        this.ptbAvatar.BackgroundImage = CropImage(returnpacket.image);
                        break;
                }
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SocketPacket disconnect = new SocketPacket(PacketType.DISCON, getlocalIP(), serverip, 52052, 52054);
            DataTranferer.Send(serverip, 52052, disconnect);
        }
        bool open = false;

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            //while(panel2.Opacity )
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush brush = new LinearGradientBrush(panel1.ClientRectangle, ColorTranslator.FromHtml("#ee4540"), ColorTranslator.FromHtml("#801336"), LinearGradientMode.Vertical);
            g.FillRectangle(brush, panel1.ClientRectangle);
            g.Dispose();
        }
        

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.ptbEditAvatar.BackgroundImage = Client.Properties.Resources.editptb2;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.ptbEditAvatar.BackgroundImage = Client.Properties.Resources.editptb;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if(open.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(open.FileName);
                EditAvatarForm editform = new EditAvatarForm(image);
                if(editform.ShowDialog() == DialogResult.OK)
                {
                    DataTranferer.Send(serverip, 52054, new SocketPacket(PacketType.UPDATEPROFILE, getlocalIP(), serverip, 52052, 52054, name, image));
                }
            }
        }
        Image CropImage(Image img)
        {
            int x = img.Width / 2;
            int y = img.Height / 2;
            int r = Math.Min(x, y);

            Bitmap tmp = null;
            tmp = new Bitmap(2 * r, 2 * r);
            using (Graphics g = Graphics.FromImage(tmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TranslateTransform(tmp.Width / 2, tmp.Height / 2);
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(0 - r, 0 - r, 2 * r, 2 * r);
                Region rg = new Region(gp);
                g.SetClip(rg, CombineMode.Replace);
                Bitmap bmp = new Bitmap(img);
                g.DrawImage(bmp, new Rectangle(-r, -r, 2 * r, 2 * r), new Rectangle(x - r, y - r, 2 * r, 2 * r), GraphicsUnit.Pixel);
            }
            return tmp;
        }
    }
    public class UpdateEventArgs : EventArgs
    {
        public int userid { get; set; }
        public DataTable newMessage { get; set; }
        public UpdateEventArgs(int uid, DataTable row)
        {
            userid = uid;
            newMessage = row;
        }
    }

}
