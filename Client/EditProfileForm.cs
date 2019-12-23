using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class EditProfileForm : Form
    {
        public EditProfileForm()
        {
            InitializeComponent();
            btnUpdate.font = new Font("Teko", 14.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnUpdate.textcolor = Color.Black;
            btnUpdate.ButtonText = "Update";
            btnUpdate.lowertext = 3;

            btnCancel.font = new Font("Teko", 14.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCancel.textcolor = Color.Black;
            btnCancel.ButtonText = "Cancel";
            btnCancel.lowertext = 3;
        }
    }
}
