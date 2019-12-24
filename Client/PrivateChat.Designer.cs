namespace Client
{
    partial class PrivateChat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivateChat));
            this.pnChat = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ptbxClose = new System.Windows.Forms.PictureBox();
            this.ptbxMinimize = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ptbxGame = new System.Windows.Forms.PictureBox();
            this.ptbxSticker = new System.Windows.Forms.PictureBox();
            this.ptbxImage = new System.Windows.Forms.PictureBox();
            this.ptbxFile = new System.Windows.Forms.PictureBox();
            this.customTextBox1 = new Client.CustomControl.CustomTextBox();
            this.customRichTextBox1 = new Client.CustomControl.CustomRichTextBox();
            this.pnChat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxSticker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxFile)).BeginInit();
            this.SuspendLayout();
            // 
            // pnChat
            // 
            this.pnChat.BackColor = System.Drawing.Color.Transparent;
            this.pnChat.Controls.Add(this.label1);
            this.pnChat.Controls.Add(this.ptbxClose);
            this.pnChat.Controls.Add(this.ptbxMinimize);
            this.pnChat.Location = new System.Drawing.Point(0, 0);
            this.pnChat.Name = "pnChat";
            this.pnChat.Size = new System.Drawing.Size(563, 24);
            this.pnChat.TabIndex = 9;
            this.pnChat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnChat_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Helvetica Neue", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(23, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Private Chat: ";
            // 
            // ptbxClose
            // 
            this.ptbxClose.BackColor = System.Drawing.Color.Transparent;
            this.ptbxClose.BackgroundImage = global::Client.Properties.Resources.close;
            this.ptbxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbxClose.Location = new System.Drawing.Point(537, 0);
            this.ptbxClose.Name = "ptbxClose";
            this.ptbxClose.Size = new System.Drawing.Size(24, 24);
            this.ptbxClose.TabIndex = 4;
            this.ptbxClose.TabStop = false;
            this.ptbxClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ptbxClose_MouseClick);
            this.ptbxClose.MouseEnter += new System.EventHandler(this.ptbxClose_MouseEnter);
            this.ptbxClose.MouseLeave += new System.EventHandler(this.ptbxClose_MouseLeave);
            // 
            // ptbxMinimize
            // 
            this.ptbxMinimize.BackColor = System.Drawing.Color.Transparent;
            this.ptbxMinimize.BackgroundImage = global::Client.Properties.Resources.minimize;
            this.ptbxMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbxMinimize.Location = new System.Drawing.Point(513, 0);
            this.ptbxMinimize.Name = "ptbxMinimize";
            this.ptbxMinimize.Size = new System.Drawing.Size(24, 24);
            this.ptbxMinimize.TabIndex = 5;
            this.ptbxMinimize.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Client.Properties.Resources.btnsend;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(485, 407);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 56);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSend_MouseClick);
            // 
            // ptbxGame
            // 
            this.ptbxGame.BackgroundImage = global::Client.Properties.Resources.game;
            this.ptbxGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbxGame.Location = new System.Drawing.Point(152, 474);
            this.ptbxGame.Name = "ptbxGame";
            this.ptbxGame.Size = new System.Drawing.Size(34, 30);
            this.ptbxGame.TabIndex = 10;
            this.ptbxGame.TabStop = false;
            // 
            // ptbxSticker
            // 
            this.ptbxSticker.BackgroundImage = global::Client.Properties.Resources.sticker;
            this.ptbxSticker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbxSticker.Location = new System.Drawing.Point(112, 474);
            this.ptbxSticker.Name = "ptbxSticker";
            this.ptbxSticker.Size = new System.Drawing.Size(34, 30);
            this.ptbxSticker.TabIndex = 8;
            this.ptbxSticker.TabStop = false;
            this.ptbxSticker.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ptbxSticker_MouseClick);
            // 
            // ptbxImage
            // 
            this.ptbxImage.BackgroundImage = global::Client.Properties.Resources.image;
            this.ptbxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbxImage.Location = new System.Drawing.Point(32, 474);
            this.ptbxImage.Name = "ptbxImage";
            this.ptbxImage.Size = new System.Drawing.Size(34, 30);
            this.ptbxImage.TabIndex = 6;
            this.ptbxImage.TabStop = false;
            this.ptbxImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ptbxImage_MouseClick);
            // 
            // ptbxFile
            // 
            this.ptbxFile.BackgroundImage = global::Client.Properties.Resources.file;
            this.ptbxFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ptbxFile.Location = new System.Drawing.Point(72, 474);
            this.ptbxFile.Name = "ptbxFile";
            this.ptbxFile.Size = new System.Drawing.Size(34, 30);
            this.ptbxFile.TabIndex = 7;
            this.ptbxFile.TabStop = false;
            // 
            // customTextBox1
            // 
            this.customTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.customTextBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customTextBox1.BackgroundImage")));
            this.customTextBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customTextBox1.Location = new System.Drawing.Point(20, 401);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.Size = new System.Drawing.Size(459, 67);
            this.customTextBox1.TabIndex = 12;
            // 
            // customRichTextBox1
            // 
            this.customRichTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.customRichTextBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customRichTextBox1.BackgroundImage")));
            this.customRichTextBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customRichTextBox1.Location = new System.Drawing.Point(20, 30);
            this.customRichTextBox1.Name = "customRichTextBox1";
            this.customRichTextBox1.Size = new System.Drawing.Size(524, 354);
            this.customRichTextBox1.TabIndex = 11;
            // 
            // PrivateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(561, 509);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.ptbxGame);
            this.Controls.Add(this.customRichTextBox1);
            this.Controls.Add(this.pnChat);
            this.Controls.Add(this.ptbxSticker);
            this.Controls.Add(this.ptbxImage);
            this.Controls.Add(this.ptbxFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PrivateChat";
            this.Text = "PrivateChat";
            this.Load += new System.EventHandler(this.PrivateChat_Load);
            this.pnChat.ResumeLayout(false);
            this.pnChat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxSticker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox ptbxClose;
        private System.Windows.Forms.PictureBox ptbxMinimize;
        private System.Windows.Forms.PictureBox ptbxImage;
        private System.Windows.Forms.PictureBox ptbxFile;
        private System.Windows.Forms.PictureBox ptbxSticker;
        private System.Windows.Forms.Panel pnChat;
        private System.Windows.Forms.PictureBox ptbxGame;
        private CustomControl.CustomRichTextBox customRichTextBox1;
        private CustomControl.CustomTextBox customTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}