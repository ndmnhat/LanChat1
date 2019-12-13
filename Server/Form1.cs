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
        UdpClient server;
        UdpClient sendserver;
        BUS_users UsersBUS = new BUS_users();
        BUS_mess MessBUS = new BUS_mess();
        public Form1()
        {
            InitializeComponent();
            label1.Text = "IP: " + getlocalIP();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart threadstart1 = new ThreadStart(ReceiveAndRespond);
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
            server.Close();
            sendserver.Close();
        }
        private void ReceiveAndRespond()
        {
            server = new UdpClient(52054);
            sendserver = new UdpClient(52051);
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Any,52052);
            try
            {
                //IPEndPoint IPE = new IPEndPoint(IPAddress.Any, 52052);
                while (true)
                {
                    byte[] data = server.Receive(ref iPEnd);
                    SocketPacket packet = new SocketPacket();
                    packet = SocketPacket.DeSerializedItem(data);
                    label2.Text = packet.Message;
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
                    REQCON_packet_process(packet);
                    break;


                case PacketType.DISCON:
                    UsersBUS.UpdateUsers(UsersBUS.GetUsersID("usersip", packet.SenderIP)[0], "userstatus", false);
                    goto case PacketType.REQFRIEND;


                case PacketType.REQFRIEND:
                    REQFRIEND_packet_process(packet);
                    break;

                case PacketType.MESSAGETABLE:
                    MESSAGETABLE_packet_process(packet);
                    break;

                case PacketType.MESSAGE:
                    MESSAGE_packet_process(packet);
                    break;


                default:break;
            }
        }
        public void REQCON_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            if (!UsersBUS.isUsersHave("username", packet.Message))
                UsersBUS.InsertUsers(new DTO_users(0, packet.Message, true, packet.SenderIP));
            else
                UsersBUS.UpdateUsers(new DTO_users(UsersBUS.GetUsersID("username", packet.Message)[0], packet.Message, true, packet.SenderIP));
            response.packetType = PacketType.REQCON;
            response.Message = "OK";
            byte[] data = SocketPacket.SerializedItem(response);
            server.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(packet.SenderIP), packet.SenderPort));
        }
        public void REQFRIEND_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            string onlinefriend = "";
            int userncount = UsersBUS.GetUsersLength("userstatus", true);
            DataTable table = UsersBUS.GetUsersWith("userstatus", true);
            onlinefriend += userncount.ToString() + "\n";
            for (int i = 0; i < userncount; i++)
            {
                onlinefriend += string.Format("{0}:{1}\n", table.Rows[i].Field<string>("username"), table.Rows[i].Field<string>("userip"));
            }
            response.SenderPort = 52051;
            response.ReceiverPort = 52053;
            response.packetType = PacketType.REQFRIEND;
            response.Message = onlinefriend;
            byte[] data2 = SocketPacket.SerializedItem(response);
            for (int i = 0; i < userncount; i++)
            {
                string rip = table.Rows[i].Field<string>("userip");
                response.ReceiverIP = rip;
                sendserver.Send(data2, data2.Length, new IPEndPoint(IPAddress.Parse(rip), 52053));
            }
        }
        public void MESSAGETABLE_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            int user1id = UsersBUS.GetUsersID(packet.SenderName);
            int user2id = UsersBUS.GetUsersID(packet.ReceiverName);
            string user1ip = packet.SenderIP;
            string user2ip = UsersBUS.GetUsersWith("username", packet.ReceiverName).Rows[0].Field<string>("userip");
            response.packetType = PacketType.MESSAGETABLE;
            response.MessageTable = MessBUS.GetAllMess(user1id, user2id);
            response.Message = user1id.ToString();
            byte[] data3 = SocketPacket.SerializedItem(response);
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user1ip), 52052));
        }
        public void MESSAGE_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            int user1id = UsersBUS.GetUsersID(packet.SenderName);
            int user2id = UsersBUS.GetUsersID(packet.ReceiverName);
            string user1ip = packet.SenderIP;
            string user2ip = UsersBUS.GetUsersWith("username", packet.ReceiverName).Rows[0].Field<string>("userip");
            DTO_mess mess = new DTO_mess(0, user1id, user2id, packet.Message, packet.Message, packet.MessageType, DateTime.Now);
            MessBUS.InsertMess(mess);
            response.packetType = PacketType.REQUPDATEMESS;
            response.MessageRow = MessBUS.GetLastMess(user1id,user2id);
            response.Message = user2id.ToString();
            response.MessageType = 1;
            byte[] data3 = SocketPacket.SerializedItem(response);
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user1ip), 52052));
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user2ip), 52053));
        }
    }
}
