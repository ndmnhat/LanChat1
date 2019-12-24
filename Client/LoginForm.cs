using Networking;
using Packet;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace Client
{
    public partial class LoginForm : Form
    {
        string localIP = getlocalIP();
        SettingDialog setting = new SettingDialog();
        string serverip;
        public LoginForm()
        {
            InitializeComponent();
            //this.button1.Left = (this.Width - this.button1.Width) / 2;
            LoadCustom();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            serverip = setting.ServerIP;
        }
        private void LoadCustom()
        {
            this.btnSignUp.ButtonText = "Sign up";
            this.btnSignUp.textcolor = ColorTranslator.FromHtml("#e68080");
            this.btnSignIn.ButtonText = "Sign in";
            this.btnSignIn.textcolor = ColorTranslator.FromHtml("#160f81");

            this.txbUsername.ForeColor = Color.LightGray;
            this.txbUsername.textbox.Location = new Point(22, 0);

            this.txbPassword.ForeColor = Color.LightGray;
            this.txbPassword.textbox.Location = new Point(22, 0);

            this.txbSignUpUsername.ForeColor = Color.LightGray;
            this.txbSignUpUsername.textbox.Location = new Point(22, 0);

            this.txbSignUpPassword.ForeColor = Color.LightGray;
            this.txbSignUpPassword.textbox.Location = new Point(22, 0);

            this.txbSignUpPassword2.ForeColor = Color.LightGray;
            this.txbSignUpPassword2.textbox.Location = new Point(22, 0);
            //Font txbpassfont = new Font("Quicksand", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.txbPassword.textbox.UseSystemPasswordChar = true;
            //this.txbPassword.textbox.Font = txbpassfont;
            //this.txbPassword.textbox.Location = new Point(22, 3);

            //this.txbSignupPassword.textbox.UseSystemPasswordChar = true;
            //this.txbSignupPassword.textbox.Font = txbpassfont;
            //this.txbSignupPassword.textbox.Location = new Point(22, 3);

            //this.txbSignupPassword2.textbox.UseSystemPasswordChar = true;
            //this.txbSignupPassword2.textbox.Font = txbpassfont;
            //this.txbSignupPassword2.textbox.Location = new Point(22, 3);
            this.Select(false,false);
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ptbClose.BackColor = Color.FromArgb(118, 22, 22);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            ptbClose.BackColor = Color.Transparent;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_MouseClick(object sender, MouseEventArgs e)
        {
            //this.Visible = false;

            SocketPacket packet = new SocketPacket(PacketType.REQCON, localIP, serverip, 52052, 52054, txbUsername.textbox.Text, "", txbPassword.textbox.Text, 0); ;
            SocketPacket returnpacket = DataTransferer.SendAndReceive(serverip, 52054, packet);
            if (returnpacket.Message == "OK")
            {
                this.Hide();
                MainForm main = new MainForm(serverip, txbUsername.textbox.Text);
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show(returnpacket.Message);
            }
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

        private void ptbSetting_MouseClick(object sender, MouseEventArgs e)
        {
            setting.ShowDialog();
            Client.Properties.Settings.Default.ServerIP = setting.ServerIP;
            Client.Properties.Settings.Default.Save();
            serverip = Client.Properties.Settings.Default.ServerIP;
        }

        private void ptbSetting_MouseEnter(object sender, EventArgs e)
        {
            ptbSetting.BackColor = Color.FromArgb(118, 22, 22);
        }

        private void ptbSetting_MouseLeave(object sender, EventArgs e)
        {
            ptbSetting.BackColor = Color.Transparent;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, ColorTranslator.FromHtml("#ff7e8b"), ColorTranslator.FromHtml("#223367"), LinearGradientMode.Horizontal);
            Graphics g = e.Graphics;
            g.FillRectangle(brush, this.ClientRectangle);
        }

        private void btnSignUp_MouseClick(object sender, MouseEventArgs e)
        {
            if (txbSignUpPassword.textbox.Text != txbSignUpPassword2.textbox.Text)
            {
                MessageBox.Show("Wrong confirm password!");
                return;
            }
            SocketPacket packet = new SocketPacket(PacketType.REG, localIP, serverip, 52052, 52054, txbSignUpUsername.textbox.Text, "", txbSignUpPassword.textbox.Text, 0);
            SocketPacket returnpacket = DataTransferer.SendAndReceive(serverip, 52054, packet);
            if (returnpacket.Message == "OK")
                MessageBox.Show("You have successfully registered!");
            else
                MessageBox.Show(returnpacket.Message);
        }
        #region textbox
        private void txbUsername_Enter(object sender, EventArgs e)
        {
            txbUsername.ForeColor = Color.Black;
            if(txbUsername.Text == "Username")
                txbUsername.Text = "";
        }

        private void txbUsername_Leave(object sender, EventArgs e)
        {
            if (txbUsername.Text == "")
            {
                txbUsername.ForeColor = Color.LightGray;
                txbUsername.Text = "Username";
            }
        }

        private void txbPassword_Enter(object sender, EventArgs e)
        {
            txbPassword.ForeColor = Color.Black;
            txbPassword.textbox.Font = new Font("Quicksand", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); ;
            txbPassword.textbox.Location = new Point(22, 7);
            if (txbPassword.Text == "Password")
            {
                txbPassword.Text = "";
                txbPassword.textbox.UseSystemPasswordChar = true;
            }
        }

        private void txbPassword_Leave(object sender, EventArgs e)
        {
            if (txbPassword.Text == "")
            {
                txbPassword.textbox.UseSystemPasswordChar = false;
                txbPassword.textbox.Location = new Point(22, 0);
                txbPassword.textbox.Font = new Font("Quicksand", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); ;
                txbPassword.ForeColor = Color.LightGray;
                txbPassword.Text = "Password";
            }
        }

        private void txbSignUpPassword_Enter(object sender, EventArgs e)
        {
            txbSignUpPassword.ForeColor = Color.Black;
            txbSignUpPassword.textbox.Font = new Font("Quicksand", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); ;
            txbSignUpPassword.textbox.Location = new Point(22, 7);
            if (txbSignUpPassword.Text == "Password")
            {
                txbSignUpPassword.Text = "";
                txbSignUpPassword.textbox.UseSystemPasswordChar = true;
            }
        }

        private void txbSignUpPassword_Leave(object sender, EventArgs e)
        {
            if (txbSignUpPassword.Text == "")
            {
                txbSignUpPassword.textbox.UseSystemPasswordChar = false;
                txbSignUpPassword.textbox.Location = new Point(22, 0);
                txbSignUpPassword.textbox.Font = new Font("Quicksand", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); ;
                txbSignUpPassword.ForeColor = Color.LightGray;
                txbSignUpPassword.Text = "Password";
            }
        }

        private void txbSignUpPassword2_Enter(object sender, EventArgs e)
        {
            txbSignUpPassword2.ForeColor = Color.Black;
            txbSignUpPassword2.textbox.Font = new Font("Quicksand", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); ;
            txbSignUpPassword2.textbox.Location = new Point(22, 7);
            if (txbSignUpPassword2.Text == "Confirm Password")
            {
                txbSignUpPassword2.Text = "";
                txbSignUpPassword2.textbox.UseSystemPasswordChar = true;
            }
        }

        private void txbSignUpPassword2_Leave(object sender, EventArgs e)
        {
            if (txbSignUpPassword2.Text == "")
            {
                txbSignUpPassword2.textbox.UseSystemPasswordChar = false;
                txbSignUpPassword2.textbox.Location = new Point(22, 0);
                txbSignUpPassword2.textbox.Font = new Font("Quicksand", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); ;
                txbSignUpPassword2.ForeColor = Color.LightGray;
                txbSignUpPassword2.Text = "Confirm Password";
            }
        }
        private void txbSignUpUsername_Enter(object sender, EventArgs e)
        {
            txbSignUpUsername.ForeColor = Color.Black;
            if (txbSignUpUsername.Text == "Username")
                txbSignUpUsername.Text = "";
        }

        private void txbSignUpUsername_Leave(object sender, EventArgs e)
        {
            if (txbSignUpUsername.Text == "")
            {
                txbSignUpUsername.ForeColor = Color.LightGray;
                txbSignUpUsername.Text = "Username";
            }
        }
        #endregion
    }
}
