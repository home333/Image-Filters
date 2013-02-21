namespace SimplePainterNamespace
{
    partial class AddTextForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "MyFont")]
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTextForm));
            this.StringToDraw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectFontButton = new System.Windows.Forms.Button();
            this.CurrentFont = new System.Windows.Forms.Label();
            this.PositionBox = new System.Windows.Forms.ComboBox();
            this.TextPosition = new System.Windows.Forms.Label();
            this.FAcceptButton = new System.Windows.Forms.Button();
            this.UserPositionCB = new System.Windows.Forms.CheckBox();
            this.UsersCoords = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StringToDraw
            // 
            this.StringToDraw.Location = new System.Drawing.Point(133, 12);
            this.StringToDraw.Multiline = true;
            this.StringToDraw.Name = "StringToDraw";
            this.StringToDraw.Size = new System.Drawing.Size(214, 20);
            this.StringToDraw.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Текст для добавления:";
            // 
            // SelectFontButton
            // 
            this.SelectFontButton.Location = new System.Drawing.Point(214, 38);
            this.SelectFontButton.Name = "SelectFontButton";
            this.SelectFontButton.Size = new System.Drawing.Size(133, 23);
            this.SelectFontButton.TabIndex = 2;
            this.SelectFontButton.Text = "Шрифт";
            this.SelectFontButton.UseVisualStyleBackColor = true;
            this.SelectFontButton.Click += new System.EventHandler(this.SetFont_Click);
            // 
            // CurrentFont
            // 
            this.CurrentFont.AutoSize = true;
            this.CurrentFont.Location = new System.Drawing.Point(3, 100);
            this.CurrentFont.Name = "CurrentFont";
            this.CurrentFont.Size = new System.Drawing.Size(91, 13);
            this.CurrentFont.TabIndex = 3;
            this.CurrentFont.Text = "Текущий шрифт:";
            // 
            // PositionBox
            // 
            this.PositionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PositionBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PositionBox.FormattingEnabled = true;
            this.PositionBox.Items.AddRange(new object[] {
            "Лево, верх",
            "Лево, низ",
            "Право, верх",
            "Право, низ"});
            this.PositionBox.Location = new System.Drawing.Point(63, 38);
            this.PositionBox.Name = "PositionBox";
            this.PositionBox.Size = new System.Drawing.Size(145, 21);
            this.PositionBox.TabIndex = 4;
            // 
            // TextPosition
            // 
            this.TextPosition.AutoSize = true;
            this.TextPosition.Location = new System.Drawing.Point(3, 43);
            this.TextPosition.Name = "TextPosition";
            this.TextPosition.Size = new System.Drawing.Size(54, 13);
            this.TextPosition.TabIndex = 5;
            this.TextPosition.Text = "Позиция:";
            // 
            // FAcceptButton
            // 
            this.FAcceptButton.Location = new System.Drawing.Point(214, 118);
            this.FAcceptButton.Name = "FAcceptButton";
            this.FAcceptButton.Size = new System.Drawing.Size(133, 23);
            this.FAcceptButton.TabIndex = 6;
            this.FAcceptButton.Text = "Применить";
            this.FAcceptButton.UseVisualStyleBackColor = true;
            this.FAcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // UserPositionCB
            // 
            this.UserPositionCB.AutoSize = true;
            this.UserPositionCB.Location = new System.Drawing.Point(6, 70);
            this.UserPositionCB.Name = "UserPositionCB";
            this.UserPositionCB.Size = new System.Drawing.Size(166, 17);
            this.UserPositionCB.TabIndex = 7;
            this.UserPositionCB.Text = "Произвольные координаты";
            this.UserPositionCB.UseVisualStyleBackColor = true;
            this.UserPositionCB.CheckedChanged += new System.EventHandler(this.UserPositionCB_CheckedChanged);
            // 
            // UsersCoords
            // 
            this.UsersCoords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UsersCoords.Enabled = false;
            this.UsersCoords.Location = new System.Drawing.Point(178, 70);
            this.UsersCoords.Name = "UsersCoords";
            this.UsersCoords.Size = new System.Drawing.Size(51, 20);
            this.UsersCoords.TabIndex = 8;
            // 
            // AddTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 144);
            this.Controls.Add(this.UsersCoords);
            this.Controls.Add(this.UserPositionCB);
            this.Controls.Add(this.FAcceptButton);
            this.Controls.Add(this.TextPosition);
            this.Controls.Add(this.PositionBox);
            this.Controls.Add(this.CurrentFont);
            this.Controls.Add(this.SelectFontButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StringToDraw);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddTextForm";
            this.Text = "Добавить текст";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddTextForm_FormClosing);
            this.Load += new System.EventHandler(this.AddEffects_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox StringToDraw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectFontButton;
        private System.Windows.Forms.Label CurrentFont;
        private System.Windows.Forms.ComboBox PositionBox;
        private System.Windows.Forms.Label TextPosition;
        private System.Windows.Forms.Button FAcceptButton;
        private System.Windows.Forms.CheckBox UserPositionCB;
        private System.Windows.Forms.TextBox UsersCoords;
    }
}