using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL_LanChat;
using DTO_LanChat;
namespace BUS_LanChat
{
    public class BUS_mess
    {
        DAL_mess DAL = new DAL_mess();
        public DataTable GetAllMess()
        {
            return DAL.GetAllMess();
        }
        public DataTable GetAllMess(int user1id, int user2id)
        {
            return DAL.GetAllMess(user1id, user2id);
        }
        public void InsertMess(DTO_mess mess)
        {
            DAL.InsertMess(mess);
        }
        public DataTable GetLastMess(int user1id, int user2id)
        {
            DataTable table = DAL.GetAllMess(user1id, user2id);
            DataTable res = table.Clone();
            res.ImportRow(table.Rows[table.Rows.Count - 1]);
            return res;
        }
    }
}
