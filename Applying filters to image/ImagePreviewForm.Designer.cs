namespace SimplePainterNamespace
{
    partial class ImagePreviewForm
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
            this.ChangedPicture = new System.Windows.Forms.PictureBox();
            this.OrigPicture = new System.Windows.Forms.PictureBox();
            this.TabControlBody = new System.Windows.Forms.TabControl();
            this.OriginalPictureTab = new System.Windows.Forms.TabPage();
            this.ChangedPictureTab = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.ChangedPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrigPicture)).BeginInit();
            this.TabControlBody.SuspendLayout();
            this.OriginalPictureTab.SuspendLayout();
            this.ChangedPictureTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChangedPicture
            // 
            this.ChangedPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangedPicture.Location = new System.Drawing.Point(3, 3);
            this.ChangedPicture.Name = "ChangedPicture";
            this.ChangedPicture.Size = new System.Drawing.Size(199, 307);
            this.ChangedPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ChangedPicture.TabIndex = 0;
            this.ChangedPicture.TabStop = false;
            // 
            // OrigPicture
            // 
            this.OrigPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrigPicture.Location = new System.Drawing.Point(3, 3);
            this.OrigPicture.Name = "OrigPicture";
            this.OrigPicture.Size = new System.Drawing.Size(199, 307);
            this.OrigPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OrigPicture.TabIndex = 1;
            this.OrigPicture.TabStop = false;
            // 
            // TabControlBody
            // 
            this.TabControlBody.Controls.Add(this.OriginalPictureTab);
            this.TabControlBody.Controls.Add(this.ChangedPictureTab);
            this.TabControlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlBody.Location = new System.Drawing.Point(0, 0);
            this.TabControlBody.Name = "TabControlBody";
            this.TabControlBody.SelectedIndex = 0;
            this.TabControlBody.Size = new System.Drawing.Size(213, 339);
            this.TabControlBody.TabIndex = 2;
            // 
            // OriginalPictureTab
            // 
            this.OriginalPictureTab.Controls.Add(this.OrigPicture);
            this.OriginalPictureTab.Location = new System.Drawing.Point(4, 22);
            this.OriginalPictureTab.Name = "OriginalPictureTab";
            this.OriginalPictureTab.Padding = new System.Windows.Forms.Padding(3);
            this.OriginalPictureTab.Size = new System.Drawing.Size(205, 313);
            this.OriginalPictureTab.TabIndex = 0;
            this.OriginalPictureTab.Text = "Оригинал";
            this.OriginalPictureTab.UseVisualStyleBackColor = true;
            // 
            // ChangedPictureTab
            // 
            this.ChangedPictureTab.Controls.Add(this.ChangedPicture);
            this.ChangedPictureTab.Location = new System.Drawing.Point(4, 22);
            this.ChangedPictureTab.Name = "ChangedPictureTab";
            this.ChangedPictureTab.Padding = new System.Windows.Forms.Padding(3);
            this.ChangedPictureTab.Size = new System.Drawing.Size(205, 313);
            this.ChangedPictureTab.TabIndex = 1;
            this.ChangedPictureTab.Text = "Итог";
            this.ChangedPictureTab.UseVisualStyleBackColor = true;
            // 
            // ImagePreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 339);
            this.Controls.Add(this.TabControlBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImagePreviewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Превью";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImagePreviewForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ChangedPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrigPicture)).EndInit();
            this.TabControlBody.ResumeLayout(false);
            this.OriginalPictureTab.ResumeLayout(false);
            this.ChangedPictureTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ChangedPicture;
        private System.Windows.Forms.PictureBox OrigPicture;
        private System.Windows.Forms.TabControl TabControlBody;
        private System.Windows.Forms.TabPage OriginalPictureTab;
        private System.Windows.Forms.TabPage ChangedPictureTab;
    }
}