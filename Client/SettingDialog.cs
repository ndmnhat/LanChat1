using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Client
{
    public partial class SettingDialog : Form
    {
        public string ServerIP { get; private set; } 
        public SettingDialog()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ServerIP = Client.Properties.Settings.Default.ServerIP;
            textBox1.Text = ServerIP;
        }

        private void btnOK_MouseClick(object sender, MouseEventArgs e)
        {
            ServerIP = textBox1.Text;
            this.Hide();
        }

        private void btnCancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.IndianRed;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.LightCoral;
        }

        private void ptbMinimize_MouseEnter(object sender, EventArgs e)
        {
            ptbMinimize.BackColor = Color.IndianRed;
        }

        private void ptbMinimize_MouseLeave(object sender, EventArgs e)
        {
            ptbMinimize.BackColor = Color.LightCoral;
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
    }
}
