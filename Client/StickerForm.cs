using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packet;
namespace Client
{
    public partial class StickerForm : Form
    {
        public string stickername = "";
        public 
        List<PictureBox> stickerlist = new List<PictureBox>();
        public StickerForm(int x, int y)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
        }

        private void StickerForm_Load(object sender, EventArgs e)
        {
            string[] stickername = Directory.GetFiles(@"Emoji\", "*", SearchOption.TopDirectoryOnly);

            int stickercount = stickername.Length;
            for(int i =0; i < stickercount; ++i)
            {
                PictureBox sticker = new PictureBox();
                sticker.BackgroundImage = Image.FromFile(stickername[i]);
                sticker.Tag = Path.GetFileName(stickername[i]);
                sticker.BorderStyle = BorderStyle.FixedSingle;
                sticker.BackgroundImageLayout = ImageLayout.Zoom;
                sticker.Size = new Size(50, 50);
                sticker.Location = new Point(5 + 55 * (i % 5), 5 + 55 * (i / 5));
                sticker.MouseClick += Sticker_MouseClick;
                stickerlist.Add(sticker);
                this.Controls.Add(stickerlist[i]);
            }
        }

        private void Sticker_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            stickername = (string)pictureBox.Tag;
            this.Close();
        }
    }
}
