﻿using System;
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
using System.Runtime.InteropServices;
using System.IO;
using DTO_LanChat;
using BUS_LanChat;
using Packet;
namespace Server
{
    public partial class MainForm : Form
    {
        UdpClient server;
        UdpClient sendserver;
        TcpListener listener;
        TcpClient tcpsender;
        BUS_users UsersBUS = new BUS_users();
        BUS_mess MessBUS = new BUS_mess();
        public MainForm()
        {
            InitializeComponent();
            lblIP.Text = "Server IP: " + getlocalIP();
            CheckForIllegalCrossThreadCalls = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            lblState.Text = "Server state: " + "Offline";
            UpdateDTG();
        }


        #region FormLoad
        private void ReceiveAndRespond()
        {
            server = new UdpClient(52054);
            sendserver = new UdpClient(52051);
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Any,52052);
            //server.Client.SendBufferSize = 1024 * 1024;
            //server.Client.ReceiveBufferSize = 1024 * 1024;
            //sendserver.Client.SendBufferSize = 1024 * 1024;
            //sendserver.Client.ReceiveBufferSize = 1024 * 1024;
        start:
            try
            {
                //IPEndPoint IPE = new IPEndPoint(IPAddress.Any, 52052);
                while (true)
                {
                    byte[] data = server.Receive(ref iPEnd);
                    SocketPacket packet = new SocketPacket();
                    packet = SocketPacket.DeSerializedItem(data);
                    ProcessPacket(packet);
                }
            }
            catch (Exception exception)
            {
                WriteToLog(exception);
                goto start;
            }
        }
        private void TcpReceiveAndRespond()
        {
            listener = new TcpListener(IPAddress.Parse(getlocalIP()), 52056);
            TcpClient client;
            listener.Start();
            try
            {
                while (true)
                {
                    client = listener.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(ThreadProc, client);
                }
            }
            catch(Exception e)
            {
                WriteToLog(e);
            }

        }
        private void ThreadProc(object obj)
        {
            var client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[316];
                    while (stream.Read(buffer, 0, buffer.Length) == 0)
                    { }
                    SocketPacket lengthpacket = SocketPacket.DeSerializedItem(buffer);
                    byte[] responsedata = new byte[lengthpacket.PacketLength];
                    SocketPacket responsetoclient = new SocketPacket(PacketType.NONE, getlocalIP(), lengthpacket.SenderIP, 52056, 52055, "OK");
                    stream.Write(Packet.SocketPacket.SerializedItem(responsetoclient), 0, Packet.SocketPacket.SerializedItem(responsetoclient).Length);
                    while (stream.Read(responsedata, 0, responsedata.Length) == 0)
                    { }
                    SocketPacket returnpacket = SocketPacket.DeSerializedItem(responsedata);
                    TcpProcessPacket(returnpacket, client);
                }
            }
            catch (Exception e)
            {

                WriteToLog(e);
                client.Close();
                Thread.CurrentThread.Abort();
            }
        }
        #endregion
        private void TcpProcessPacket(SocketPacket packet,TcpClient client)
        {
            switch(packet.packetType)
            {
                case PacketType.MESSAGETABLE:
                    MESSAGETABLE_packet_process(packet, client);
                    break;
                case PacketType.IMG:
                    IMG_packet_process(packet, client);
                    break;
                case PacketType.REQPROFILE:
                    REQPROFILE_packet_process(packet, client);
                    break;
                case PacketType.UPDATEAVATAR:
                    UPDATEAVATAR_packet_process(packet, client);
                    break;
            }
        }
        private void ProcessPacket(SocketPacket packet)
        {
            
            switch(packet.packetType)
            {
                //case PacketType.UPDATEAVATAR:
                //    UPDATEAVATAR_packet_process(packet);
                //    break;

                case PacketType.REQCON:
                    REQCON_packet_process(packet);
                    break;


                case PacketType.DISCON:
                    UsersBUS.UpdateUsers(UsersBUS.GetUsersID("usersip", packet.SenderIP)[0], "userstatus", false);
                    goto case PacketType.REQFRIEND;


                case PacketType.REQFRIEND:
                    REQFRIEND_packet_process(packet);
                    break;

                //case PacketType.MESSAGETABLE:
                //    MESSAGETABLE_packet_process(packet);
                //    break;

                case PacketType.MESSAGE:
                    MESSAGE_packet_process(packet);
                    break;


                case PacketType.REG:
                    REG_packet_process(packet);
                    break;

                case PacketType.UPDATEPROFILE:
                    UPDATEPROFILE_packet_process(packet);
                    break;

                case PacketType.STICKER:
                    STICKER_packet_process(packet);
                    break;

                case PacketType.REQUSERPROFILE:
                    REQUSERPROFILE_packet_process(packet);
                    break;
                //case PacketType.REQPROFILE:
                //    REQPROFILE_packet_process(packet);
                //    break;

                //case PacketType.IMG:
                //    IMG_packet_process(packet);
                //    break;
                case PacketType.CLOSE:
                    CLOSE_packet_process(packet);
                    break;
                default:break;
            }
        }
        #region PACKET PROCESS
        private void REQUSERPROFILE_packet_process(SocketPacket packet)
        {
            DTO_users user = UsersBUS.GetUsers(UsersBUS.GetUsersID(packet.SenderName));
            user.userpassword = "";
            user.useravatar = null;
            SocketPacket response = new SocketPacket(PacketType.REQUSERPROFILE, getlocalIP(), packet.SenderIP, 52054, 52052, packet.SenderName, user);
            byte[] data = SocketPacket.SerializedItem(response);
            sendserver.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(packet.SenderIP), 52052));
        }

        private void STICKER_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            int user1id = UsersBUS.GetUsersID(packet.SenderName);
            int user2id = UsersBUS.GetUsersID(packet.ReceiverName);
            string user1ip = packet.SenderIP;
            string user2ip = UsersBUS.GetUsersWith("username", packet.ReceiverName).Rows[0].Field<string>("userip");
            DTO_mess mess = new DTO_mess(0, user1id, user2id, packet.Message, packet.MessageType, DateTime.Now);
            MessBUS.InsertMess(mess);
            response.packetType = PacketType.REQUPDATEMESS;
            response.MessageRow = MessBUS.GetLastMess(user1id, user2id);
            response.Message = user2id.ToString();
            response.MessageType = 2;
            byte[] data3 = SocketPacket.SerializedItem(response);
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user1ip), 52052));
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user2ip), 52053));
        }

        private void UPDATEPROFILE_packet_process(SocketPacket packet)
        {
            DTO_users userinfo = UsersBUS.GetUsers(UsersBUS.GetUsersID(packet.SenderName));
            userinfo.userfullname = packet.userinfo.userfullname;
            userinfo.usergender = packet.userinfo.usergender;
            userinfo.userbirthday = packet.userinfo.userbirthday;
            userinfo.userphonenumber = packet.userinfo.userphonenumber;
            UsersBUS.UpdateUsers(userinfo);
            userinfo.useravatar = null;
            SocketPacket response = new SocketPacket(PacketType.UPDATEPROFILE, getlocalIP(), packet.SenderIP, 52054, 52052, packet.SenderName, userinfo);
            byte[] data = SocketPacket.SerializedItem(response);
            server.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(packet.SenderIP), 52053));
            UpdateDTG();
        }

        private void REQCON_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            if (UsersBUS.PasswordCheck(packet.SenderName, packet.Message))
            {
                UsersBUS.UpdateUsers(UsersBUS.GetUsersID(packet.SenderName), "userip", packet.SenderIP);
                response.Message = "OK";
            }
            else
                response.Message = "Wrong password!";
            response.packetType = PacketType.REQCON;
            byte[] data = SocketPacket.SerializedItem(response);
            server.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(packet.SenderIP), packet.SenderPort));
            UpdateDTG();
        }
        private void REQFRIEND_packet_process(SocketPacket packet)
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
        private void MESSAGETABLE_packet_process(SocketPacket packet,TcpClient client)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52056, packet.SenderPort);
            int user1id = UsersBUS.GetUsersID(packet.SenderName);
            int user2id = UsersBUS.GetUsersID(packet.ReceiverName);
            string user1ip = packet.SenderIP;
            string user2ip = UsersBUS.GetUsersWith("username", packet.ReceiverName).Rows[0].Field<string>("userip");
            response.packetType = PacketType.MESSAGETABLE;
            response.MessageTable = MessBUS.GetAllMess(user1id, user2id);
            response.Message = user1id.ToString();
            byte[] data = SocketPacket.SerializedItem(response);
            //sendserver.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(user1ip), 52052));
            TcpSendPacket(response, client);
        }
        private void MESSAGE_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            int user1id = UsersBUS.GetUsersID(packet.SenderName);
            int user2id = UsersBUS.GetUsersID(packet.ReceiverName);
            string user1ip = packet.SenderIP;
            string user2ip = UsersBUS.GetUsersWith("username", packet.ReceiverName).Rows[0].Field<string>("userip");
            DTO_mess mess = new DTO_mess(0, user1id, user2id, packet.Message, packet.MessageType, DateTime.Now);
            MessBUS.InsertMess(mess);
            response.packetType = PacketType.REQUPDATEMESS;
            response.MessageRow = MessBUS.GetLastMess(user1id,user2id);
            response.Message = user2id.ToString();
            response.MessageType = 1;
            byte[] data3 = SocketPacket.SerializedItem(response);
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user1ip), 52052));
            sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user2ip), 52053));
        }
        private void REG_packet_process(SocketPacket packet)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            if (!UsersBUS.isUsersHave("username", packet.SenderName))
            {
                UsersBUS.InsertUsers(new DTO_users(0, packet.SenderName, true, packet.SenderIP, packet.Message));
                response.Message = "OK";
            }
            else
                response.Message = "Username already taken";
            response.packetType = PacketType.REG;
            byte[] data = SocketPacket.SerializedItem(response);
            server.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(packet.SenderIP), packet.SenderPort));
            UpdateDTG();
        }
        private void UPDATEAVATAR_packet_process(SocketPacket packet, TcpClient client)
        {
            UsersBUS.UpdateUsers(UsersBUS.GetUsersID("username", packet.SenderName)[0], "useravatar", packet.image);
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52051, 52053);
            response.packetType = PacketType.UPDATEAVATAR;
            response.SenderName = packet.SenderName;
            response.image = packet.image;
            tcpsender = new TcpClient();
            try
            {
                tcpsender = new TcpClient();
                tcpsender.Connect(IPAddress.Parse(packet.SenderIP), 52057);
                TcpSendPacket(response, tcpsender);
                tcpsender.Close();
            }
            catch (Exception exception)
            {
                WriteToLog(exception);
            }
        }
        private void REQPROFILE_packet_process(SocketPacket packet, TcpClient client)
        {
            DTO_users user = UsersBUS.GetUsers(UsersBUS.GetUsersID(packet.SenderName));
            SocketPacket response = new SocketPacket(PacketType.REQPROFILE, getlocalIP(), packet.SenderIP, 52054, 52053,packet.SenderName,user);
            try
            {
                tcpsender = new TcpClient();
                tcpsender.Connect(IPAddress.Parse(packet.SenderIP), 52057);
                TcpSendPacket(response, tcpsender);
                tcpsender.Close();
            }
            catch (Exception exception)
            {
                WriteToLog(exception);
            }
            UpdateDTG();
        }
        public void IMG_packet_process(SocketPacket packet, TcpClient client)
        {
            SocketPacket response = new SocketPacket(PacketType.NONE, getlocalIP(), packet.SenderIP, 52054, packet.SenderPort);
            int user1id = UsersBUS.GetUsersID(packet.SenderName);
            int user2id = UsersBUS.GetUsersID(packet.ReceiverName);
            string user1ip = packet.SenderIP;
            string user2ip = UsersBUS.GetUsersWith("username", packet.ReceiverName).Rows[0].Field<string>("userip");
            DTO_mess mess = new DTO_mess(0, user1id, user2id, packet.image, DateTime.Now);
            MessBUS.InsertMess(mess);
            response.packetType = PacketType.REQUPDATEMESS;
            response.MessageRow = MessBUS.GetLastMess(user1id, user2id);
            response.Message = user2id.ToString();
            response.MessageType = 3;
            TcpSendPacket(response, client);
            try
            {
                tcpsender = new TcpClient();
                tcpsender.Connect(IPAddress.Parse(user2ip), 52057);
                TcpSendPacket(response, tcpsender);
                tcpsender.Close();
            }
            catch (Exception exception)
            {
                WriteToLog(exception);
            }
            //sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user1ip), 52052));
            //sendserver.Send(data3, data3.Length, new IPEndPoint(IPAddress.Parse(user2ip), 52053));
        }
        private void CLOSE_packet_process(SocketPacket packet)
        {
            Task.Run(() =>
            {
                UsersBUS.UpdateUsers(UsersBUS.GetUsersID(packet.SenderName), "userstatus", false);
                UpdateDTG();
            });
        }
        private void TcpSendPacket(SocketPacket packet, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] data = SocketPacket.SerializedItem(packet);
            byte[] datalength = Packet.SocketPacket.SerializedItem(new SocketPacket(PacketType.PACKETLENGTH, getlocalIP(), packet.ReceiverIP, 52056, 52055, data.Length));
            stream.Write(datalength, 0, datalength.Length);
            byte[] buffer = new byte[310];
            while(stream.Read(buffer, 0, buffer.Length)==0)
            { }
            if (Packet.SocketPacket.DeSerializedItem(buffer).Message == "OK")
                stream.Write(data, 0, data.Length);
        }
        #endregion

        #region GUI
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateDTG()
        {
            DataTable usertable = UsersBUS.GetAllUsers();
            DataView view = new DataView(usertable);
            DataTable usertable2 = view.ToTable(false, "userid", "username", "userstatus");
            dtgUser.DataSource = usertable2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                if (server.Client == null)
                    goto Run;
                MessageBox.Show("Server is already opened");
                return;
            }
            Run:
            ThreadStart threadstart1 = new ThreadStart(ReceiveAndRespond);
            thread = new Thread(threadstart1);
            ThreadStart threadstart2 = new ThreadStart(TcpReceiveAndRespond);
            thread2 = new Thread(threadstart2);
            thread.IsBackground = true;
            thread.Start();
            thread2.Start();
            lblState.Text = "Server state: " + "Online";
        }
        Thread thread;
        Thread thread2;
        private void button2_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                MessageBox.Show("Server is not opened yet.");
                return;
            }
            if(server.Client == null)
            {
                MessageBox.Show("Server is not opened yet.");
                return;
            }
            server.Close();
            sendserver.Close();
            listener.Stop();
            lblState.Text = "Server state: " + "Offline";
            thread.Abort();
            thread2.Abort();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ptbClose_MouseEnter(object sender, EventArgs e)
        {
            ptbClose.BackColor = Color.FromArgb(16, 16, 74);
        }

        private void ptbClose_MouseLeave(object sender, EventArgs e)
        {
            ptbClose.BackColor = Color.Transparent;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            //this.Visible = false;
            //notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            //notifyIcon1.Visible = false;
            this.Activate();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(16, 16, 74);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }


        #endregion

        #region Utility
        private void WriteToLog(Exception e)
        {
            try
            {
                File.AppendAllText("Log.txt", string.Format("[{0}] : {1}\n", DateTime.Now.ToString(), e.ToString()));
                label3.Visible = true;
            }
            catch
            { }
            //using (var stream = File.AppendText("Log.txt",))
            //{
            //    stream.WriteLine(string.Format("[{0}] : {1}\n", DateTime.Now.ToString(), e.Message)); 
            //    stream.Flush();
            //    stream.Close();
            //}
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
        #endregion

    }
}
