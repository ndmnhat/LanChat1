using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using DTO_LanChat;
using BUS_LanChat;
using Packet;
namespace Server
{
    public partial class Form1 : Form
    {
        TcpListener server;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "IP: " + getlocalIP();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart threadstart1 = new ThreadStart(Listen);
            Thread thread = new Thread(threadstart1);
            thread.IsBackground = true;
            thread.Start();
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

        private void button2_Click(object sender, EventArgs e)
        {
            server.Stop();
        }
        private void Listen()
        {
            server = new TcpListener(IPAddress.Parse(getlocalIP()),52052);
            server.Start();
            try
            {
                //IPEndPoint IPE = new IPEndPoint(IPAddress.Any, 52052);
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    //CopyTo a memorystream to create a automatically-sized buffer
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);
                    byte[] data = ms.ToArray();
                    SocketPacket packet = SocketPacket.DeSerializedItem(data);
                    label2.Text = packet.ToString();
                    ProcessPacket(packet);
                }
            }
            catch (Exception exception)
            {
                label3.Text = exception.Message;
            }
        }
        public void ProcessPacket(SocketPacket packet)
        {
            switch(packet.packetType)
            {
                case PacketType.REQCON:
                    break;
            }
        }
    }
}
