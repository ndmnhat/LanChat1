using System;
using System.Drawing;
namespace DTO_LanChat
{
    public class DTO_users
    {
        public int userid { get; set; }
        public string username { get; set; }
        public bool userstatus { get; set; }
        public string userip { get; set; }
        public string userpassword { get; set; }
        public Image useravatar { get; set; }
        public DTO_users()
        {
            userid = 0;
            username = "";
            userstatus = false;
            userip = "0.0.0.0";
        }
        public DTO_users(int id, string name, bool status)
        {
            userid = id;
            username = name;
            userstatus= status;
        }
        public DTO_users(int id, string name, bool status, string ip)
        {
            userid = id;
            username = name;
            userstatus = status;
            userip = ip;
        }
        public DTO_users(int id, string name, bool status, string ip, string password)
        {
            userid = id;
            username = name;
            userstatus = status;
            userip = ip;
            userpassword = password;
        }
        public DTO_users(int id, string name, bool status, string ip, string password, Image avatar)
        {
            userid = id;
            username = name;
            userstatus = status;
            userip = ip;
            userpassword = password;
            useravatar = avatar;
        }
    }
}
