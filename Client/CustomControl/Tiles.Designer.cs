namespace Client.CustomControl
{
    partial class Tiles
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TilesName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TilesName
            // 
            this.TilesName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TilesName.BackColor = System.Drawing.Color.Transparent;
            this.TilesName.Font = new System.Drawing.Font("Quicksand", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TilesName.ForeColor = System.Drawing.Color.Black;
            this.TilesName.Location = new System.Drawing.Point(0, 69);
            this.TilesName.Name = "TilesName";
            this.TilesName.Size = new System.Drawing.Size(114, 36);
            this.TilesName.TabIndex = 0;
            this.TilesName.Text = "Username";
            this.TilesName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TilesName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tiles_MouseDown);
            this.TilesName.MouseEnter += new System.EventHandler(this.Tiles_MouseEnter);
            this.TilesName.MouseLeave += new System.EventHandler(this.Tiles_MouseLeave);
            this.TilesName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tiles_MouseUp);
            // 
            // Tiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::Client.Properties.Resources.tiles;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.TilesName);
            this.DoubleBuffered = true;
            this.Name = "Tiles";
            this.Size = new System.Drawing.Size(114, 107);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tiles_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Tiles_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Tiles_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tiles_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label TilesName;
    }
}
