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
namespace Client
{
    public partial class LoginForm : Form
    {
        string localIP = getlocalIP();
        int localPort = 52053;
        public LoginForm()
        {
            InitializeComponent();
            this.button1.Left = (this.Width - this.button1.Width) / 2;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ptbClose.BackColor = Color.FromArgb(118, 22, 22);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            ptbClose.BackColor = Color.Transparent;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            SocketPacket packet = new SocketPacket(PacketType.REQCON, localIP, txbserverip.Text, localPort, 52052,txbName.Text);
            try
            {
                TcpClient client = new TcpClient(txbserverip.Text, 52052);
                byte[] data = SocketPacket.SerializedItem(packet);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                byte[] response = new byte[1024];
                int length = stream.Read(response, 0, 1024);
                SocketPacket returnpacket = SocketPacket.DeSerializedItem(response);
                stream.Close();
                client.Close();
                if (returnpacket.Message=="OK") //Kiem tra ten ton tai
                {
                    MainForm main = new MainForm(txbserverip.Text);
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Try again");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }
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
    }
}
