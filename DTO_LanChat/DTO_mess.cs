using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace DTO_LanChat
{
    public class DTO_mess
    {
        public int messid { get; set; }
        public int senderid { get; set; }
        public int receiverid { get; set; }
        public string content { get; set; }
        public string sticker { get; set; }
        public Image image { get; set; }
        public int messtype { get; set; }
        public DateTime timesent { get; set; }
        public DTO_mess()
        {
            messid = 0;
            senderid = 0;
            receiverid = 0;
            content = "";
            sticker = "";
            messtype = 0;
            timesent = DateTime.Now;
        }
        public DTO_mess(int mid, int sid, int rid, string text, int type, DateTime time)
        {
            messid = mid;
            senderid = sid;
            receiverid = rid;
            if (type == 1)
                content = text;
            else
                sticker = text;
            messtype = type;
            timesent = time;
        }
        public DTO_mess(int mid, int sid, int rid,Image bitmap, DateTime time)
        {
            messid = mid;
            senderid = sid;
            receiverid = rid;
            image = bitmap;
            messtype = 3;
            timesent = time;
        }
    }
}
