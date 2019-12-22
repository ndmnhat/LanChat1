using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using DTO_LanChat;
namespace DAL_LanChat
{
    public class DAL_mess : dbConnection
    {
        public DataTable GetAllMess()
        {
            DataTable result = new DataTable();
            conn.Open();
            string sql = "SELECT * FROM mess ORDER BY timesent ASC";
            using (SQLiteCommand SqlCmd = new SQLiteCommand(sql,conn))
            {
                using (SQLiteDataAdapter sqlData = new SQLiteDataAdapter(SqlCmd))
                {
                    sqlData.Fill(result);
                }
            }
            conn.Close();
            return result;
        }
        public DataTable GetAllMess(int user1id, int user2id)
        {
            DataTable result = new DataTable();
            conn.Open();
            string sql = "SELECT * FROM mess WHERE (senderid = @user1id and receiverid = @user2id) or (senderid = @user2id and receiverid = @user1id) ORDER BY timesent ASC ";
            using (SQLiteCommand SqlCmd = new SQLiteCommand(sql, conn))
            {
                SqlCmd.Parameters.AddWithValue("@user1id", user1id);
                SqlCmd.Parameters.AddWithValue("@user2id", user2id);
                using (SQLiteDataAdapter sqlData = new SQLiteDataAdapter(SqlCmd))
                {
                    sqlData.Fill(result);
                }
            }
            conn.Close();
            return result;
        }
        public void InsertMess(DTO_mess mess)
        {
            conn.Open();
            string sql = "INSERT INTO mess (senderid,receiverid,content,sticker,messtype,timesent) VALUES (@SenderID,@ReceiverID,@Content,@Sticker,@MessType,@TimeSent)";
            using (SQLiteCommand SqlCmd = new SQLiteCommand(sql,conn))
            {
                SqlCmd.Parameters.AddWithValue("@SenderID", mess.senderid);
                SqlCmd.Parameters.AddWithValue("@ReceiverID", mess.receiverid);
                SqlCmd.Parameters.AddWithValue("@Content", mess.content);
                SqlCmd.Parameters.AddWithValue("@Sticker", mess.sticker);
                SqlCmd.Parameters.AddWithValue("@MessType", mess.messtype.ToString());
                SqlCmd.Parameters.AddWithValue("@TimeSent", mess.timesent.ToString());
                SqlCmd.Prepare();
                SqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
