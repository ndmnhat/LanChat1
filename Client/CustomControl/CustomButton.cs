using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.CustomControl
{
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();
        }
        public string ButtonText;
        public Color textcolor;
        public Font font = new Font("Oswald", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public int lowertext = 0;
        private void CustomButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float textwidth = getTextWidth(ButtonText, font);
            float textheight = getTextHeight(ButtonText, font);
            g.DrawString(ButtonText, font, new SolidBrush(textcolor), (float)(this.Width / 2 - textwidth / 2), (float)(this.Height / 2 - textheight / 2 + lowertext));
        }
        private float getTextWidth(string text, Font font)
        {
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(text, font);
            graphics.Dispose();
            return size.Width;
        }
        private float getTextHeight(string text, Font font)
        {
            Image fakeImage = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(fakeImage);
            SizeF size = graphics.MeasureString(text, font);
            graphics.Dispose();
            return size.Height;
        }
    }
}
