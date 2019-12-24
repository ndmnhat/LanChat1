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
    public partial class Tiles : UserControl
    {
        public string TilesIP;
        public Tiles()
        {
            InitializeComponent();
        }

        private void Tiles_MouseEnter(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Client.Properties.Resources.tiles2;
        }

        private void Tiles_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = global::Client.Properties.Resources.tiles;
        }

        private void Tiles_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = global::Client.Properties.Resources.tiles3;
        }

        private void Tiles_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = global::Client.Properties.Resources.tiles2;
        }
    }
}
