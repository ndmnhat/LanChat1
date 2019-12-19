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
    public partial class CustomLoginTextBox : UserControl
    {
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text { get { return textbox.Text; } set { textbox.Text = value; }  }

        public override Color ForeColor { get { return textbox.ForeColor; } set { textbox.ForeColor = value; } }
        public override Font Font { get { return textbox.Font; } set { textbox.Font = value; } }
        public CustomLoginTextBox()
        {
            InitializeComponent();
        }
    }
}
