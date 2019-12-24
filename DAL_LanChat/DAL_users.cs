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
        public DAL_users()
        {
            conn.Open();
        }
        ~DAL_users()
        {
            conn.Close();    
        }
        public DataTable GetAllUsers()
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM users", conn);
            DataTable table = new DataTable();
            da.Fill(table);
            conn.Close();
            return table;
        }
        public DataTable GetUsersWith(string Attribute, object value)
        {
            //conn.Open();
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
            //conn.Close();
            return table;
        }
        public void InsertUsers(DTO_users user)
        {
            //conn.Open();
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
            //conn.Close();
        }
        public void UpdateUsers(DTO_users user)
        {
            //conn.Open();
            string sql = "UPDATE users SET username = @username, userfullname = @userfullname, userbirthday = @userbirthday, usergender = @usergender, userphonenumber = @userphonenumber, userstatus = @userstatus, userip = @userip, useravatar = @useravatar WHERE userid = @userid";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@username", user.username);
                sqlCmd.Parameters.AddWithValue("@userstatus", user.userstatus);
                sqlCmd.Parameters.AddWithValue("@userip", user.userip);
                sqlCmd.Parameters.AddWithValue("@userid", user.userid);
                sqlCmd.Parameters.AddWithValue("@useravatar", imageToByteArray(user.useravatar));
                sqlCmd.Parameters.AddWithValue("@userfullname", user.userfullname);
                sqlCmd.Parameters.AddWithValue("@usergender", user.usergender);
                sqlCmd.Parameters.AddWithValue("@userbirthday", user.userbirthday.ToString());
                sqlCmd.Parameters.AddWithValue("@userphonenumber", user.userphonenumber);
                sqlCmd.ExecuteNonQuery();
            }
            //conn.Close();
        }
        public void DeleteUsers(int userid)
        {
            //conn.Open();
            string sql = "DELETE FROM users WHERE userid = @userid";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@userid", userid);
                sqlCmd.ExecuteNonQuery();
            }
            //conn.Close();
        }
        public DTO_users GetUsers(int userid)
        {
            DTO_users user = new DTO_users();
            //conn.Open();
            string sql = "SELECT * FROM users WHERE userid = @userid";
            using (SQLiteCommand SqlCmd = new SQLiteCommand(sql,conn))
            {
                SqlCmd.Parameters.AddWithValue("@userid", userid);
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(SqlCmd))
                {
                    using (DataTable table = new DataTable())
                    {
                        dataAdapter.Fill(table);
                        user.userid = Convert.ToInt32(table.Rows[0].Field<object>("userid"));
                        user.username = Convert.ToString(table.Rows[0].Field<object>("username"));
                        user.userpassword = Convert.ToString(table.Rows[0].Field<object>("userpassword"));
                        user.userstatus = Convert.ToBoolean(table.Rows[0].Field<object>("userstatus"));
                        user.userip = Convert.ToString(table.Rows[0].Field<object>("userip"));
                        user.userfullname = Convert.ToString(table.Rows[0].Field<object>("userfullname"));
                        user.usergender = Convert.ToString(table.Rows[0].Field<object>("usergender"));
                        user.userbirthday = Convert.ToDateTime(table.Rows[0].Field<object>("userbirthday"));
                        user.userphonenumber = Convert.ToString(table.Rows[0].Field<object>("userphonenumber"));
                        user.useravatar = byteArrayToImage((byte[])table.Rows[0].Field<object>("useravatar"));
                    }
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
            //conn.Close();
            return user;
        }
        public bool isUsersHave(string Attribute, object value)
        {
            //conn.Open();
            bool check;
            string sql = string.Format("SELECT COUNT(*) FROM users WHERE "+ Attribute +" = @value");
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@value", value);
                check = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            //conn.Close();
            return check;
        }
        public int[] GetUsersID(string Attribute,object value)
        {
            //conn.State = ConnectionState.
            //conn.Open();
            DataTable table = new DataTable();
            int[] id;
            string sql = "SELECT * FROM users WHERE "+ Attribute +" = @value";
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@value", value);
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlCmd))
                {
                    dataAdapter.Fill(table);
                    id = new int[table.Rows.Count];
                    for (int i = 0; i < table.Rows.Count; ++i)
                    {
                        id[i] = Convert.ToInt32(table.Rows[i].Field<object>("userid"));
                    }
                }
            }
            //conn.Close();
            return id;
        }
        public void UpdateUsers(int id, string Attribute, object value)
        {
            //conn.Open();
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
            //conn.Close();
        }
        byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
                return null;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
