using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
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
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM users", conn);
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
            using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@value", value);
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
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
            using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@username", user.username);
                sqlCmd.Parameters.AddWithValue("@userstatus", user.userstatus);
                sqlCmd.Parameters.AddWithValue("@userip", user.userip);
                sqlCmd.Parameters.AddWithValue("@userpassword", user.userpassword);
                sqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public void UpdateUsers(DTO_users user)
        {
            conn.Open();
            string sql = "UPDATE users SET username = @username, userstatus = @userstatus, userip = @userip, useravatar = @useravatar WHERE userid = @userid";
            using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
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
            using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@userid", userid);
                sqlCmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        public DTO_users GetUsers(int userid)
        {
            DTO_users user = new DTO_users();
            DataTable dataTable = new DataTable();
            conn.Open();
            string sql = "SELECT * FROM users WHERE userid = @userid";
            using (SqlCommand SqlCmd = new SqlCommand(sql,conn))
            {
                SqlCmd.Parameters.AddWithValue("@userid", userid);
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlCmd))
                {
                    dataAdapter.Fill(dataTable);
                }
            }
            user.userid = dataTable.Rows[0].Field<int>("userid");
            user.username = dataTable.Rows[0].Field<string>("username");
            user.userstatus = dataTable.Rows[0].Field<bool>("userstatus");
            user.userpassword = dataTable.Rows[0].Field<string>("userpassword");
            user.userip = dataTable.Rows[0].Field<string>("userip");
            //user.useravatar = byteArrayToImage(dataTable.Rows[0].Field<byte[]>("useravatar"));
            conn.Close();
            return user;
        }
        public bool isUsersHave(string Attribute, object value)
        {
            conn.Open();
            bool check;
            string sql = string.Format("SELECT COUNT(*) FROM users WHERE "+ Attribute +" = @value");
            using (SqlCommand cmd = new SqlCommand(sql, conn))
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
            string sql = "SELECT * FROM users WHERE "+ Attribute +" = @value";
            using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@value", value);
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                {
                    sqlAdapter.Fill(table);
                }
            }
            conn.Close();
            int[] id = new int[table.Rows.Count];
            for (int i = 0; i < id.Length; ++i)
                id[i] = table.Rows[i].Field<int>(0);
            return id;
        }
        public void UpdateUsers(int id, string Attribute, object value)
        {
            conn.Open();
            string sql = "UPDATE users SET "+ Attribute +" = @value WHERE userid = @userid";
            using (SqlCommand sqlCmd = new SqlCommand(sql, conn))
            {
                sqlCmd.Parameters.AddWithValue("@value", value);
                sqlCmd.Parameters.AddWithValue("@userid", id);
                sqlCmd.ExecuteNonQuery();
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
