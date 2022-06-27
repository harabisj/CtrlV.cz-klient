namespace CtrlV
{
    partial class ImagePreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagePreview));
            this.ImgPreview = new System.Windows.Forms.PictureBox();
            this.showInBrowserButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImgPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // ImgPreview
            // 
            this.ImgPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImgPreview.Location = new System.Drawing.Point(9, 51);
            this.ImgPreview.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImgPreview.Name = "ImgPreview";
            this.ImgPreview.Size = new System.Drawing.Size(760, 483);
            this.ImgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ImgPreview.TabIndex = 0;
            this.ImgPreview.TabStop = false;
            // 
            // showInBrowserButton
            // 
            this.showInBrowserButton.Location = new System.Drawing.Point(9, 10);
            this.showInBrowserButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.showInBrowserButton.Name = "showInBrowserButton";
            this.showInBrowserButton.Size = new System.Drawing.Size(276, 37);
            this.showInBrowserButton.TabIndex = 1;
            this.showInBrowserButton.Text = "Zobrazit v prohlížeči";
            this.showInBrowserButton.UseVisualStyleBackColor = true;
            this.showInBrowserButton.Click += new System.EventHandler(this.showInBrowserButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(290, 10);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(276, 37);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Odstranit obrázek";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // ImagePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.showInBrowserButton);
            this.Controls.Add(this.ImgPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "ImagePreview";
            this.Text = "Náhled obrázku";
            ((System.ComponentModel.ISupportInitialize)(this.ImgPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImgPreview;
        private System.Windows.Forms.Button showInBrowserButton;
        private System.Windows.Forms.Button deleteButton;
    }
}