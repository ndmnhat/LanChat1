using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;
using Packet;
namespace Networking
{
    public static class DataTransferer
    {
        static UdpClient client = new UdpClient(52052);

        static UdpClient sendclient = new UdpClient(52050);
        static TcpClient tcpclient = new TcpClient(new IPEndPoint(IPAddress.Parse(getlocalIP()),52055));
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
        public static SocketPacket SendAndReceive(string remoteip, int remoteport, SocketPacket packet)
        {
            client.Client.ReceiveTimeout = 5000;
            try
            {
                byte[] data = SocketPacket.SerializedItem(packet);
                IPEndPoint serveriPEnd = new IPEndPoint(IPAddress.Parse(remoteip), remoteport);
                client.Send(data, data.Length, serveriPEnd);
                byte[] response = client.Receive(ref serveriPEnd);
                SocketPacket returnpacket = SocketPacket.DeSerializedItem(response);
                return returnpacket;
            }
            catch (Exception exception)
            {
                return new SocketPacket(PacketType.NONE, getlocalIP(), remoteip,52052,remoteport,exception.Message);
            }
        }
        public static void Send(string remoteip, int remoteport, SocketPacket packet)
        {
            try
            {
                byte[] data = SocketPacket.SerializedItem(packet);
                IPEndPoint serveriPEnd = new IPEndPoint(IPAddress.Parse(remoteip), remoteport);
                sendclient.Send(data, data.Length, serveriPEnd);
            }
            catch (Exception)
            {
            }
        }
        public static SocketPacket TcpSendAndReceive(string remoteip, int remoteport, SocketPacket packet)
        {
            if (!tcpclient.Connected)
                tcpclient.Connect(remoteip,remoteport);
            try
            {
                byte[] data = Packet.SocketPacket.SerializedItem(packet);
                byte[] datalength = Packet.SocketPacket.SerializedItem(new SocketPacket(PacketType.PACKETLENGTH, getlocalIP(), remoteip, 52055, 52056, data.Length));
                NetworkStream stream = tcpclient.GetStream();
                stream.Write(datalength, 0, datalength.Length);
                byte[] buffer = new byte[310];
                stream.Read(buffer, 0, buffer.Length);
                if (Packet.SocketPacket.DeSerializedItem(buffer).Message == "OK")
                    stream.Write(data, 0, data.Length);
                byte[] buffer2 = new byte[316];
                while(stream.Read(buffer2, 0, buffer2.Length)==0)
                { }
                byte[] responsedata = new byte[SocketPacket.DeSerializedItem(buffer2).PacketLength];
                SocketPacket responsetoserver = new SocketPacket(PacketType.NONE, getlocalIP(), remoteip, 52055, 52056,"OK");
                stream.Write(Packet.SocketPacket.SerializedItem(responsetoserver), 0, Packet.SocketPacket.SerializedItem(responsetoserver).Length);
                while(stream.Read(responsedata, 0, responsedata.Length)==0)
                { }
                SocketPacket returnpacket = SocketPacket.DeSerializedItem(responsedata);
                return returnpacket;
            }
            catch (Exception exception)
            {
                return new SocketPacket(PacketType.NONE, getlocalIP(), remoteip, 52052, remoteport, exception.Message);
            }
        }
        public static void TcpSend(string remoteip, int remoteport, SocketPacket packet)
        {
            if (!tcpclient.Connected)
                tcpclient.Connect(remoteip, remoteport);
            try
            {
                byte[] data = Packet.SocketPacket.SerializedItem(packet);
                byte[] datalength = Packet.SocketPacket.SerializedItem(new SocketPacket(PacketType.PACKETLENGTH, getlocalIP(), remoteip, 52055, 52056, data.Length));
                NetworkStream stream = tcpclient.GetStream();
                stream.Write(datalength, 0, datalength.Length);
                byte[] buffer = new byte[310];
                stream.Read(buffer, 0, buffer.Length);
                if (Packet.SocketPacket.DeSerializedItem(buffer).Message == "OK")
                    stream.Write(data, 0, data.Length);
            }
            catch (Exception)
            {
            }
        }
    }
}
