using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Drawing;
using System.IO;
using DTO_LanChat;
namespace DAL_LanChat
{
    public class DAL_users : dbConnection
    {
        public DataTable GetAllUsers()
        {
            conn.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM users", conn);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();
            return table;
        }
        public DataTable GetUsersWith(string Attribute, object value)
        {
            conn.Open();
            DataTable table = new DataTable();
            string sql = "SELECT * FROM users WHERE "+ Attribute +" = @value";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@value", value);
                using (SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter(sqlCmd))
                {
                    sqlAdapter.Fill(table);
                }
            }
            conn.Close();
            return table;
        }
        public void InsertUsers(DTO_users user)
        {
            conn.Open();
            string sql = "INSERT INTO users (username,userstatus,userip,userpassword) VALUES (@username,@userstatus,@userip,@userpassword)";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@username", user.username);
                sqlCmd.Parameters.AddWithValue("@userstatus",Convert.ToInt32(user.userstatus));
                sqlCmd.Parameters.AddWithValue("@userip", user.userip);
                sqlCmd.Parameters.AddWithValue("@userpassword", user.userpassword);
                sqlCmd.Prepare();
                sqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public void UpdateUsers(DTO_users user)
        {
            conn.Open();
            string sql = "UPDATE users SET username = @username, userstatus = @userstatus, userip = @userip, useravatar = @useravatar WHERE userid = @userid";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@username", user.username);
                sqlCmd.Parameters.AddWithValue("@userstatus", user.userstatus);
                sqlCmd.Parameters.AddWithValue("@userip", user.userip);
                sqlCmd.Parameters.AddWithValue("@userid", user.userid);
                sqlCmd.Parameters.AddWithValue("@useravatar", imageToByteArray(user.useravatar));
                sqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public void DeleteUsers(int userid)
        {
            conn.Open();
            string sql = "DELETE FROM users WHERE userid = @userid";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@userid", userid);
                sqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public DTO_users GetUsers(int userid)
        {
            DTO_users user = new DTO_users();
            conn.Open();
            string sql = "SELECT * FROM users WHERE userid = @userid";
            using (SQLiteCommand SqlCmd = new SQLiteCommand(sql,conn))
            {
                SqlCmd.Parameters.AddWithValue("@userid", userid);
                using(SQLiteDataReader dataReader = SqlCmd.ExecuteReader())
                {
                    dataReader.Read();
                    user.userid = dataReader.GetInt32(0);
                    user.username = dataReader.GetString(1);
                    user.userpassword = dataReader.GetString(2);
                    //user.userstatus = Convert.ToBoolean(dataReader.GetInt32(3));
                    user.userip = dataReader.GetString(4);
                }
            }
            //dataReader.Read();
            //user.userid = dataReader.GetInt32(0);
            //user.username = dataReader.GetString(1);
            //user.userpassword = dataReader.GetString(2);
            //user.userstatus = Convert.ToBoolean(dataReader.GetInt32(3));
            //user.userip = dataReader.GetString(4);
            //if(dataTable.Rows[0].Field<byte[]>("useravatar")!=null)
            //    user.useravatar = byteArrayToImage(dataTable.Rows[0].Field<byte[]>("useravatar"));
            conn.Close();
            return user;
        }
        public bool isUsersHave(string Attribute, object value)
        {
            conn.Open();
            bool check;
            string sql = string.Format("SELECT COUNT(*) FROM users WHERE "+ Attribute +" = @value");
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@value", value);
                check = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            conn.Close();
            return check;
        }
        public int[] GetUsersID(string Attribute,object value)
        {
            conn.Open();
            DataTable table = new DataTable();
            int[] id;
            string sql = "SELECT * FROM users WHERE "+ Attribute +" = @value";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@value", value);
                using (SQLiteDataReader dataReader = sqlCmd.ExecuteReader())
                {
                    id = new int[dataReader.StepCount];
                    for (int i = 0; dataReader.Read(); ++i)
                    {
                        id[i] = dataReader.GetInt32(0);
                    }
                }
            }
            conn.Close();
            return id;
        }
        public void UpdateUsers(int id, string Attribute, object value)
        {
            conn.Open();
            string sql = "UPDATE users SET "+ Attribute +" = @val WHERE userid = " + id;
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                if (Attribute == "useravatar")
                    sqlCmd.Parameters.AddWithValue("@val", imageToByteArray((Image)value));
                else
                    sqlCmd.Parameters.AddWithValue("@val", value);

                //sqlCmd.Parameters.AddWithValue("@userid", id);
                sqlCmd.Prepare();
                int k = sqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
