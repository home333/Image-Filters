
namespace SimplePainterNamespace
{
    partial class ChangeResolution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeResolution));
            this.DefWidthTextBox = new System.Windows.Forms.TextBox();
            this.DefHeightTextBox = new System.Windows.Forms.TextBox();
            this.NewHeight = new System.Windows.Forms.Label();
            this.NewWidth = new System.Windows.Forms.Label();
            this.NewHeightTextBox = new System.Windows.Forms.TextBox();
            this.NewWidthTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DefWidthTextBox
            // 
            this.DefWidthTextBox.Location = new System.Drawing.Point(7, 12);
            this.DefWidthTextBox.Name = "DefWidthTextBox";
            this.DefWidthTextBox.ReadOnly = true;
            this.DefWidthTextBox.Size = new System.Drawing.Size(100, 20);
            this.DefWidthTextBox.TabIndex = 0;
            // 
            // DefHeightTextBox
            // 
            this.DefHeightTextBox.Location = new System.Drawing.Point(113, 12);
            this.DefHeightTextBox.Name = "DefHeightTextBox";
            this.DefHeightTextBox.ReadOnly = true;
            this.DefHeightTextBox.Size = new System.Drawing.Size(99, 20);
            this.DefHeightTextBox.TabIndex = 1;
            // 
            // NewHeight
            // 
            this.NewHeight.AutoSize = true;
            this.NewHeight.Location = new System.Drawing.Point(113, 35);
            this.NewHeight.Name = "NewHeight";
            this.NewHeight.Size = new System.Drawing.Size(82, 13);
            this.NewHeight.TabIndex = 7;
            this.NewHeight.Text = "Новая высота:";
            // 
            // NewWidth
            // 
            this.NewWidth.AutoSize = true;
            this.NewWidth.Location = new System.Drawing.Point(4, 35);
            this.NewWidth.Name = "NewWidth";
            this.NewWidth.Size = new System.Drawing.Size(83, 13);
            this.NewWidth.TabIndex = 6;
            this.NewWidth.Text = "Новая ширина:";
            // 
            // NewHeightTextBox
            // 
            this.NewHeightTextBox.Location = new System.Drawing.Point(112, 51);
            this.NewHeightTextBox.Name = "NewHeightTextBox";
            this.NewHeightTextBox.Size = new System.Drawing.Size(99, 20);
            this.NewHeightTextBox.TabIndex = 5;
            // 
            // NewWidthTextBox
            // 
            this.NewWidthTextBox.Location = new System.Drawing.Point(7, 51);
            this.NewWidthTextBox.Name = "NewWidthTextBox";
            this.NewWidthTextBox.Size = new System.Drawing.Size(99, 20);
            this.NewWidthTextBox.TabIndex = 4;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(110, 77);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(101, 23);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "Применить";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.ClickStart_Button);
            // 
            // ChangeResolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 106);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.NewHeight);
            this.Controls.Add(this.NewWidth);
            this.Controls.Add(this.NewHeightTextBox);
            this.Controls.Add(this.NewWidthTextBox);
            this.Controls.Add(this.DefHeightTextBox);
            this.Controls.Add(this.DefWidthTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeResolution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChangeResolution";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DefWidthTextBox;
        private System.Windows.Forms.TextBox DefHeightTextBox;
        private System.Windows.Forms.Label NewHeight;
        private System.Windows.Forms.Label NewWidth;
        private System.Windows.Forms.TextBox NewHeightTextBox;
        private System.Windows.Forms.TextBox NewWidthTextBox;
        private System.Windows.Forms.Button OKButton;
    }
}