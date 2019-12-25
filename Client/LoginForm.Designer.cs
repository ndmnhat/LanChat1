namespace Client
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lblWelcome = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSignup = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ptbSetting = new System.Windows.Forms.PictureBox();
            this.ptbClose = new System.Windows.Forms.PictureBox();
            this.ptbMinimize = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnSignUp = new Client.CustomControl.CustomButton();
            this.btnSignIn = new Client.CustomControl.CustomButton();
            this.txbSignUpPassword2 = new Client.CustomControl.CustomLoginTextBox();
            this.txbPassword = new Client.CustomControl.CustomLoginTextBox();
            this.txbSignUpPassword = new Client.CustomControl.CustomLoginTextBox();
            this.txbSignUpUsername = new Client.CustomControl.CustomLoginTextBox();
            this.txbUsername = new Client.CustomControl.CustomLoginTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.BackColor = System.Drawing.Color.Transparent;
            this.lblWelcome.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(411, 44);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(220, 37);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome back,";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Josefin Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(31, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Don\'t have an account?";
            // 
            // lblSignup
            // 
            this.lblSignup.AutoSize = true;
            this.lblSignup.BackColor = System.Drawing.Color.Transparent;
            this.lblSignup.Font = new System.Drawing.Font("Open Sans", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSignup.ForeColor = System.Drawing.Color.White;
            this.lblSignup.Location = new System.Drawing.Point(29, 53);
            this.lblSignup.Name = "lblSignup";
            this.lblSignup.Size = new System.Drawing.Size(190, 38);
            this.lblSignup.TabIndex = 1;
            this.lblSignup.Text = "Sign up here";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Client.Properties.Resources.dotLine;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(338, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 228);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // ptbSetting
            // 
            this.ptbSetting.BackColor = System.Drawing.Color.Transparent;
            this.ptbSetting.BackgroundImage = global::Client.Properties.Resources.settings;
            this.ptbSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbSetting.Location = new System.Drawing.Point(611, 10);
            this.ptbSetting.Name = "ptbSetting";
            this.ptbSetting.Size = new System.Drawing.Size(24, 24);
            this.ptbSetting.TabIndex = 3;
            this.ptbSetting.TabStop = false;
            this.ptbSetting.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ptbSetting_MouseClick);
            this.ptbSetting.MouseEnter += new System.EventHandler(this.ptbSetting_MouseEnter);
            this.ptbSetting.MouseLeave += new System.EventHandler(this.ptbSetting_MouseLeave);
            // 
            // ptbClose
            // 
            this.ptbClose.BackColor = System.Drawing.Color.Transparent;
            this.ptbClose.BackgroundImage = global::Client.Properties.Resources.close_white_108x108;
            this.ptbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ptbClose.Location = new System.Drawing.Point(671, 11);
            this.ptbClose.Name = "ptbClose";
            this.ptbClose.Size = new System.Drawing.Size(20, 20);
            this.ptbClose.TabIndex = 3;
            this.ptbClose.TabStop = false;
            this.ptbClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.ptbClose.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.ptbClose.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // ptbMinimize
            // 
            this.ptbMinimize.BackColor = System.Drawing.Color.Transparent;
            this.ptbMinimize.BackgroundImage = global::Client.Properties.Resources.minimizewhite;
            this.ptbMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ptbMinimize.Location = new System.Drawing.Point(641, 10);
            this.ptbMinimize.Name = "ptbMinimize";
            this.ptbMinimize.Size = new System.Drawing.Size(24, 24);
            this.ptbMinimize.TabIndex = 3;
            this.ptbMinimize.TabStop = false;
            this.ptbMinimize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ptbMinimize_MouseClick);
            this.ptbMinimize.MouseEnter += new System.EventHandler(this.ptbMinimize_MouseEnter);
            this.ptbMinimize.MouseLeave += new System.EventHandler(this.ptbMinimize_MouseLeave);
            // 
            // btnSignUp
            // 
            this.btnSignUp.BackColor = System.Drawing.Color.Transparent;
            this.btnSignUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSignUp.BackgroundImage")));
            this.btnSignUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSignUp.Location = new System.Drawing.Point(97, 239);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(144, 64);
            this.btnSignUp.TabIndex = 5;
            this.btnSignUp.TabStop = false;
            this.btnSignUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSignUp_MouseClick);
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.Transparent;
            this.btnSignIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSignIn.BackgroundImage")));
            this.btnSignIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSignIn.Location = new System.Drawing.Point(460, 222);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(144, 64);
            this.btnSignIn.TabIndex = 5;
            this.btnSignIn.TabStop = false;
            this.btnSignIn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSignIn_MouseClick);
            // 
            // txbSignUpPassword2
            // 
            this.txbSignUpPassword2.BackColor = System.Drawing.Color.Transparent;
            this.txbSignUpPassword2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txbSignUpPassword2.BackgroundImage")));
            this.txbSignUpPassword2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txbSignUpPassword2.Location = new System.Drawing.Point(49, 188);
            this.txbSignUpPassword2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbSignUpPassword2.Name = "txbSignUpPassword2";
            this.txbSignUpPassword2.Size = new System.Drawing.Size(250, 31);
            this.txbSignUpPassword2.TabIndex = 3;
            this.txbSignUpPassword2.Text = "Confirm Password";
            this.txbSignUpPassword2.Enter += new System.EventHandler(this.txbSignUpPassword2_Enter);
            this.txbSignUpPassword2.Leave += new System.EventHandler(this.txbSignUpPassword2_Leave);
            // 
            // txbPassword
            // 
            this.txbPassword.BackColor = System.Drawing.Color.Transparent;
            this.txbPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txbPassword.BackgroundImage")));
            this.txbPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txbPassword.Location = new System.Drawing.Point(411, 160);
            this.txbPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(250, 31);
            this.txbPassword.TabIndex = 5;
            this.txbPassword.Text = "Password";
            this.txbPassword.Enter += new System.EventHandler(this.txbPassword_Enter);
            this.txbPassword.Leave += new System.EventHandler(this.txbPassword_Leave);
            // 
            // txbSignUpPassword
            // 
            this.txbSignUpPassword.BackColor = System.Drawing.Color.Transparent;
            this.txbSignUpPassword.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txbSignUpPassword.BackgroundImage")));
            this.txbSignUpPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txbSignUpPassword.Location = new System.Drawing.Point(49, 144);
            this.txbSignUpPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbSignUpPassword.Name = "txbSignUpPassword";
            this.txbSignUpPassword.Size = new System.Drawing.Size(250, 31);
            this.txbSignUpPassword.TabIndex = 2;
            this.txbSignUpPassword.Text = "Password";
            this.txbSignUpPassword.Enter += new System.EventHandler(this.txbSignUpPassword_Enter);
            this.txbSignUpPassword.Leave += new System.EventHandler(this.txbSignUpPassword_Leave);
            // 
            // txbSignUpUsername
            // 
            this.txbSignUpUsername.BackColor = System.Drawing.Color.Transparent;
            this.txbSignUpUsername.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txbSignUpUsername.BackgroundImage")));
            this.txbSignUpUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txbSignUpUsername.Location = new System.Drawing.Point(49, 100);
            this.txbSignUpUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbSignUpUsername.Name = "txbSignUpUsername";
            this.txbSignUpUsername.Size = new System.Drawing.Size(250, 31);
            this.txbSignUpUsername.TabIndex = 1;
            this.txbSignUpUsername.Text = "Username";
            this.txbSignUpUsername.Enter += new System.EventHandler(this.txbSignUpUsername_Enter);
            this.txbSignUpUsername.Leave += new System.EventHandler(this.txbSignUpUsername_Leave);
            // 
            // txbUsername
            // 
            this.txbUsername.BackColor = System.Drawing.Color.Transparent;
            this.txbUsername.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txbUsername.BackgroundImage")));
            this.txbUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.txbUsername.Location = new System.Drawing.Point(411, 107);
            this.txbUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbUsername.Name = "txbUsername";
            this.txbUsername.Size = new System.Drawing.Size(250, 31);
            this.txbUsername.TabIndex = 4;
            this.txbUsername.Text = "Username";
            this.txbUsername.Enter += new System.EventHandler(this.txbUsername_Enter);
            this.txbUsername.Leave += new System.EventHandler(this.txbUsername_Leave);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.ClientSize = new System.Drawing.Size(705, 330);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.txbSignUpPassword2);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbSignUpPassword);
            this.Controls.Add(this.txbSignUpUsername);
            this.Controls.Add(this.txbUsername);
            this.Controls.Add(this.ptbSetting);
            this.Controls.Add(this.ptbMinimize);
            this.Controls.Add(this.ptbClose);
            this.Controls.Add(this.lblSignup);
            this.Controls.Add(this.lblWelcome);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LoginForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ptbClose;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.PictureBox ptbSetting;
        private CustomControl.CustomLoginTextBox txbUsername;
        private CustomControl.CustomLoginTextBox txbPassword;
        private CustomControl.CustomButton btnSignIn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSignup;
        private CustomControl.CustomLoginTextBox txbSignUpUsername;
        private CustomControl.CustomLoginTextBox txbSignUpPassword;
        private CustomControl.CustomLoginTextBox txbSignUpPassword2;
        private CustomControl.CustomButton btnSignUp;
        private System.Windows.Forms.PictureBox ptbMinimize;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}

