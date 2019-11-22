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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.button1.Left = (this.Width - this.button1.Width) / 2;
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

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.ShowDialog();
            this.Close();
        }
    }
}
