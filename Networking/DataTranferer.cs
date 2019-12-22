﻿using System;
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
    public static class DataTranferer
    {
        static UdpClient client = new UdpClient(52052);

        static UdpClient sendclient = new UdpClient(52050);
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
        public static SocketPacket Receiver(string remoteip, int remoteport)
        {
            UdpClient receiver = new UdpClient(52054);
            IPEndPoint serveriPEnd = new IPEndPoint(IPAddress.Parse(remoteip), remoteport);
            byte[] data = receiver.Receive(ref serveriPEnd);
            SocketPacket returnpacket = SocketPacket.DeSerializedItem(data);
            return returnpacket;
        }
    }
}
