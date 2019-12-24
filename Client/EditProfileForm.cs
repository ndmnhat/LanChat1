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
        public string name { get { return txbName.Text; } }
        public string gender { get { return cbbGender.Text; } }
        public DateTime birthday { get { return dtpBirthday.Value; } }
        public string phone { get { return txbPhone.Text; } }
        public EditProfileForm(string name, string gender, DateTime birthday, string phone)
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

            txbName.Text = name;
            txbPhone.Text = phone;
            cbbGender.SelectedItem = gender;
            dtpBirthday.Value = birthday;
        }

        private void btnCancel_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnUpdate_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
