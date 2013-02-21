namespace SimplePainterNamespace
{
    partial class AdvancedEditor
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedEditor));
            this.ToolLayout = new System.Windows.Forms.TableLayoutPanel();
            this.MyMenu = new System.Windows.Forms.MenuStrip();
            this.FileDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenImage = new System.Windows.Forms.ToolStripMenuItem();
            this.Screenshot = new System.Windows.Forms.ToolStripMenuItem();
            this.главныйЭкранToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.всеЭкраныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.активноеОкноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.областьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseImage = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jPGToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bMPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pNGToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.FiltersMenuList = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.NormalViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomViewMode = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeElapsed = new System.Windows.Forms.ToolStripTextBox();
            this.DefPanel = new System.Windows.Forms.Panel();
            this.FiltersList = new System.Windows.Forms.TreeView();
            this.SomethingsPanel = new System.Windows.Forms.Panel();
            this.Somethings = new System.Windows.Forms.TabControl();
            this.FilterInformation = new System.Windows.Forms.TabPage();
            this.information = new System.Windows.Forms.RichTextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.OriginalImagePanel = new System.Windows.Forms.Panel();
            this.DefaultPicture = new System.Windows.Forms.PictureBox();
            this.OutputImagePanel = new System.Windows.Forms.Panel();
            this.EditedPicture = new System.Windows.Forms.PictureBox();
            this.EditedImageMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveImageContext = new System.Windows.Forms.ToolStripMenuItem();
            this.JPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteImageContext = new System.Windows.Forms.ToolStripMenuItem();
            this.SpecialMenuBar = new System.Windows.Forms.ToolStrip();
            this.HideShowLeft = new System.Windows.Forms.ToolStripButton();
            this.ZoomView = new System.Windows.Forms.ToolStripButton();
            this.Separator0 = new System.Windows.Forms.ToolStripSeparator();
            this.TextTools = new System.Windows.Forms.ToolStripButton();
            this.separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RandomFilter = new System.Windows.Forms.ToolStripButton();
            this.RezolutionTools = new System.Windows.Forms.ToolStripButton();
            this.separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseAll = new System.Windows.Forms.ToolStripButton();
            this.ReloadOriginal = new System.Windows.Forms.ToolStripButton();
            this.StatsImage = new System.Windows.Forms.ToolStripButton();
            this.separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SomeTools = new System.Windows.Forms.ToolStrip();
            this.ToolLayout.SuspendLayout();
            this.MyMenu.SuspendLayout();
            this.DefPanel.SuspendLayout();
            this.SomethingsPanel.SuspendLayout();
            this.Somethings.SuspendLayout();
            this.FilterInformation.SuspendLayout();
            this.OriginalImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultPicture)).BeginInit();
            this.OutputImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditedPicture)).BeginInit();
            this.EditedImageMenu.SuspendLayout();
            this.SpecialMenuBar.SuspendLayout();
            this.SomeTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolLayout
            // 
            resources.ApplyResources(this.ToolLayout, "ToolLayout");
            this.ToolLayout.Controls.Add(this.MyMenu, 0, 0);
            this.ToolLayout.Controls.Add(this.DefPanel, 0, 2);
            this.ToolLayout.Controls.Add(this.SomethingsPanel, 2, 2);
            this.ToolLayout.Controls.Add(this.SomeTools, 1, 1);
            this.ToolLayout.Controls.Add(this.OriginalImagePanel, 0, 1);
            this.ToolLayout.Controls.Add(this.OutputImagePanel, 2, 1);
            this.ToolLayout.Controls.Add(this.SpecialMenuBar, 1, 2);
            this.ToolLayout.Name = "ToolLayout";
            // 
            // MyMenu
            // 
            this.ToolLayout.SetColumnSpan(this.MyMenu, 3);
            this.MyMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileDialog,
            this.FiltersMenuList,
            this.ViewMode,
            this.AboutDialog,
            this.TimeElapsed});
            resources.ApplyResources(this.MyMenu, "MyMenu");
            this.MyMenu.Name = "MyMenu";
            // 
            // FileDialog
            // 
            this.FileDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileDialog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenImage,
            this.Screenshot,
            this.CloseImage,
            this.saveImageMenuItem,
            this.Exit});
            this.FileDialog.Image = global::SimplePainterNamespace.Properties.Resources.lightining;
            this.FileDialog.Name = "FileDialog";
            resources.ApplyResources(this.FileDialog, "FileDialog");
            // 
            // OpenImage
            // 
            this.OpenImage.Image = global::SimplePainterNamespace.Properties.Resources.OpenFile;
            this.OpenImage.Name = "OpenImage";
            resources.ApplyResources(this.OpenImage, "OpenImage");
            this.OpenImage.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // Screenshot
            // 
            this.Screenshot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.главныйЭкранToolStripMenuItem,
            this.всеЭкраныToolStripMenuItem,
            this.активноеОкноToolStripMenuItem,
            this.областьToolStripMenuItem});
            this.Screenshot.Image = global::SimplePainterNamespace.Properties.Resources.takescreen;
            this.Screenshot.Name = "Screenshot";
            resources.ApplyResources(this.Screenshot, "Screenshot");
            // 
            // главныйЭкранToolStripMenuItem
            // 
            this.главныйЭкранToolStripMenuItem.Name = "главныйЭкранToolStripMenuItem";
            resources.ApplyResources(this.главныйЭкранToolStripMenuItem, "главныйЭкранToolStripMenuItem");
            this.главныйЭкранToolStripMenuItem.Click += new System.EventHandler(this.PrimaryScreenScreenShotItem_Click);
            // 
            // всеЭкраныToolStripMenuItem
            // 
            this.всеЭкраныToolStripMenuItem.Name = "всеЭкраныToolStripMenuItem";
            resources.ApplyResources(this.всеЭкраныToolStripMenuItem, "всеЭкраныToolStripMenuItem");
            this.всеЭкраныToolStripMenuItem.Click += new System.EventHandler(this.AllScreenScreenShotMenuItem_Click);
            // 
            // активноеОкноToolStripMenuItem
            // 
            this.активноеОкноToolStripMenuItem.Name = "активноеОкноToolStripMenuItem";
            resources.ApplyResources(this.активноеОкноToolStripMenuItem, "активноеОкноToolStripMenuItem");
            this.активноеОкноToolStripMenuItem.Click += new System.EventHandler(this.ActiveWindowsScreenshot_Click);
            // 
            // областьToolStripMenuItem
            // 
            this.областьToolStripMenuItem.Name = "областьToolStripMenuItem";
            resources.ApplyResources(this.областьToolStripMenuItem, "областьToolStripMenuItem");
            this.областьToolStripMenuItem.Click += new System.EventHandler(this.ScreenRegion_Click);
            // 
            // CloseImage
            // 
            this.CloseImage.Image = global::SimplePainterNamespace.Properties.Resources.delete;
            this.CloseImage.Name = "CloseImage";
            resources.ApplyResources(this.CloseImage, "CloseImage");
            this.CloseImage.Click += new System.EventHandler(this.CloseImage_Click);
            // 
            // saveImageMenuItem
            // 
            this.saveImageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jPGToolStripMenuItem1,
            this.bMPToolStripMenuItem1,
            this.pNGToolStripMenuItem1});
            this.saveImageMenuItem.Image = global::SimplePainterNamespace.Properties.Resources.saveHS;
            this.saveImageMenuItem.Name = "saveImageMenuItem";
            resources.ApplyResources(this.saveImageMenuItem, "saveImageMenuItem");
            // 
            // jPGToolStripMenuItem1
            // 
            this.jPGToolStripMenuItem1.Name = "jPGToolStripMenuItem1";
            resources.ApplyResources(this.jPGToolStripMenuItem1, "jPGToolStripMenuItem1");
            this.jPGToolStripMenuItem1.Click += new System.EventHandler(this.JPGToolStripMenuItem_Click);
            // 
            // bMPToolStripMenuItem1
            // 
            this.bMPToolStripMenuItem1.Name = "bMPToolStripMenuItem1";
            resources.ApplyResources(this.bMPToolStripMenuItem1, "bMPToolStripMenuItem1");
            this.bMPToolStripMenuItem1.Click += new System.EventHandler(this.bMPToolStripMenuItem_Click);
            // 
            // pNGToolStripMenuItem1
            // 
            this.pNGToolStripMenuItem1.Name = "pNGToolStripMenuItem1";
            resources.ApplyResources(this.pNGToolStripMenuItem1, "pNGToolStripMenuItem1");
            this.pNGToolStripMenuItem1.Click += new System.EventHandler(this.PNGToolStripMenuItem_Click);
            // 
            // Exit
            // 
            this.Exit.Image = global::SimplePainterNamespace.Properties.Resources.DeleteHS;
            this.Exit.Name = "Exit";
            resources.ApplyResources(this.Exit, "Exit");
            this.Exit.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // FiltersMenuList
            // 
            this.FiltersMenuList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FiltersMenuList.Image = global::SimplePainterNamespace.Properties.Resources.filters;
            this.FiltersMenuList.Name = "FiltersMenuList";
            resources.ApplyResources(this.FiltersMenuList, "FiltersMenuList");
            // 
            // ViewMode
            // 
            this.ViewMode.AutoToolTip = true;
            this.ViewMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NormalViewMode,
            this.ZoomViewMode});
            this.ViewMode.Image = global::SimplePainterNamespace.Properties.Resources.ViewPictureMode;
            this.ViewMode.Name = "ViewMode";
            resources.ApplyResources(this.ViewMode, "ViewMode");
            // 
            // NormalViewMode
            // 
            this.NormalViewMode.Name = "NormalViewMode";
            resources.ApplyResources(this.NormalViewMode, "NormalViewMode");
            this.NormalViewMode.Click += new System.EventHandler(this.NormalViewMode_Click);
            // 
            // ZoomViewMode
            // 
            this.ZoomViewMode.Checked = true;
            this.ZoomViewMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ZoomViewMode.Name = "ZoomViewMode";
            resources.ApplyResources(this.ZoomViewMode, "ZoomViewMode");
            this.ZoomViewMode.Click += new System.EventHandler(this.ZoomViewMode_Click);
            // 
            // AboutDialog
            // 
            this.AboutDialog.AutoToolTip = true;
            this.AboutDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AboutDialog.Image = global::SimplePainterNamespace.Properties.Resources.About;
            this.AboutDialog.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.AboutDialog.Name = "AboutDialog";
            this.AboutDialog.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            resources.ApplyResources(this.AboutDialog, "AboutDialog");
            this.AboutDialog.Click += new System.EventHandler(this.AboutDialog_Click);
            // 
            // TimeElapsed
            // 
            this.TimeElapsed.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TimeElapsed.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TimeElapsed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TimeElapsed, "TimeElapsed");
            this.TimeElapsed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TimeElapsed.Name = "TimeElapsed";
            this.TimeElapsed.ReadOnly = true;
            // 
            // DefPanel
            // 
            this.DefPanel.Controls.Add(this.FiltersList);
            resources.ApplyResources(this.DefPanel, "DefPanel");
            this.DefPanel.Name = "DefPanel";
            // 
            // FiltersList
            // 
            this.FiltersList.BackColor = System.Drawing.SystemColors.Window;
            this.FiltersList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.FiltersList, "FiltersList");
            this.FiltersList.HideSelection = false;
            this.FiltersList.Name = "FiltersList";
            this.FiltersList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FiltersList_AfterSelect);
            // 
            // SomethingsPanel
            // 
            this.SomethingsPanel.Controls.Add(this.Somethings);
            this.SomethingsPanel.Controls.Add(this.StartButton);
            resources.ApplyResources(this.SomethingsPanel, "SomethingsPanel");
            this.SomethingsPanel.Name = "SomethingsPanel";
            // 
            // Somethings
            // 
            this.Somethings.Controls.Add(this.FilterInformation);
            resources.ApplyResources(this.Somethings, "Somethings");
            this.Somethings.Name = "Somethings";
            this.Somethings.SelectedIndex = 0;
            // 
            // FilterInformation
            // 
            this.FilterInformation.Controls.Add(this.information);
            resources.ApplyResources(this.FilterInformation, "FilterInformation");
            this.FilterInformation.Name = "FilterInformation";
            this.FilterInformation.UseVisualStyleBackColor = true;
            // 
            // information
            // 
            this.information.BackColor = System.Drawing.SystemColors.Window;
            this.information.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.information, "information");
            this.information.Name = "information";
            // 
            // StartButton
            // 
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.StartButton, "StartButton");
            this.StartButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.StartButton.FlatAppearance.BorderSize = 0;
            this.StartButton.Image = global::SimplePainterNamespace.Properties.Resources.Run;
            this.StartButton.Name = "StartButton";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // OriginalImagePanel
            // 
            resources.ApplyResources(this.OriginalImagePanel, "OriginalImagePanel");
            this.OriginalImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OriginalImagePanel.Controls.Add(this.DefaultPicture);
            this.OriginalImagePanel.Name = "OriginalImagePanel";
            // 
            // DefaultPicture
            // 
            resources.ApplyResources(this.DefaultPicture, "DefaultPicture");
            this.DefaultPicture.Name = "DefaultPicture";
            this.DefaultPicture.TabStop = false;
            // 
            // OutputImagePanel
            // 
            resources.ApplyResources(this.OutputImagePanel, "OutputImagePanel");
            this.OutputImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputImagePanel.Controls.Add(this.EditedPicture);
            this.OutputImagePanel.Name = "OutputImagePanel";
            // 
            // EditedPicture
            // 
            this.EditedPicture.ContextMenuStrip = this.EditedImageMenu;
            resources.ApplyResources(this.EditedPicture, "EditedPicture");
            this.EditedPicture.Name = "EditedPicture";
            this.EditedPicture.TabStop = false;
            
            // 
            // EditedImageMenu
            // 
            this.EditedImageMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageContext,
            this.DeleteImageContext});
            this.EditedImageMenu.Name = "EditedImageMenu";
            resources.ApplyResources(this.EditedImageMenu, "EditedImageMenu");
            // 
            // saveImageContext
            // 
            this.saveImageContext.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.JPGToolStripMenuItem,
            this.PNGToolStripMenuItem,
            this.BMPToolStripMenuItem});
            this.saveImageContext.Image = global::SimplePainterNamespace.Properties.Resources.saveHS;
            this.saveImageContext.Name = "saveImageContext";
            resources.ApplyResources(this.saveImageContext, "saveImageContext");
            // 
            // JPGToolStripMenuItem
            // 
            this.JPGToolStripMenuItem.Name = "JPGToolStripMenuItem";
            resources.ApplyResources(this.JPGToolStripMenuItem, "JPGToolStripMenuItem");
            this.JPGToolStripMenuItem.Click += new System.EventHandler(this.JPGToolStripMenuItem_Click);
            // 
            // PNGToolStripMenuItem
            // 
            this.PNGToolStripMenuItem.Name = "PNGToolStripMenuItem";
            resources.ApplyResources(this.PNGToolStripMenuItem, "PNGToolStripMenuItem");
            this.PNGToolStripMenuItem.Click += new System.EventHandler(this.PNGToolStripMenuItem_Click);
            // 
            // BMPToolStripMenuItem
            // 
            this.BMPToolStripMenuItem.Name = "BMPToolStripMenuItem";
            resources.ApplyResources(this.BMPToolStripMenuItem, "BMPToolStripMenuItem");
            this.BMPToolStripMenuItem.Click += new System.EventHandler(this.bMPToolStripMenuItem_Click);
            // 
            // DeleteImageContext
            // 
            this.DeleteImageContext.Image = global::SimplePainterNamespace.Properties.Resources.delete;
            this.DeleteImageContext.Name = "DeleteImageContext";
            resources.ApplyResources(this.DeleteImageContext, "DeleteImageContext");
            this.DeleteImageContext.Click += new System.EventHandler(this.DeletePictureMenuItem_Click);
            // 
            // SpecialMenuBar
            // 
            this.SpecialMenuBar.BackColor = System.Drawing.SystemColors.MenuBar;
            resources.ApplyResources(this.SpecialMenuBar, "SpecialMenuBar");
            this.SpecialMenuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SpecialMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HideShowLeft});
            this.SpecialMenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.SpecialMenuBar.Name = "SpecialMenuBar";
            // 
            // HideShowLeft
            // 
            this.HideShowLeft.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.HideShowLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HideShowLeft.Image = global::SimplePainterNamespace.Properties.Resources.hide;
            resources.ApplyResources(this.HideShowLeft, "HideShowLeft");
            this.HideShowLeft.Name = "HideShowLeft";
            this.HideShowLeft.Click += new System.EventHandler(this.HideShowLeft_Click);
            // 
            // ZoomView
            // 
            this.ZoomView.CheckOnClick = true;
            this.ZoomView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomView.Image = global::SimplePainterNamespace.Properties.Resources.View;
            resources.ApplyResources(this.ZoomView, "ZoomView");
            this.ZoomView.Name = "ZoomView";
            this.ZoomView.Click += new System.EventHandler(this.ZoomView_Click);
            // 
            // Separator0
            // 
            this.Separator0.Name = "Separator0";
            resources.ApplyResources(this.Separator0, "Separator0");
            // 
            // TextTools
            // 
            this.TextTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TextTools.Image = global::SimplePainterNamespace.Properties.Resources.TextboxHS;
            resources.ApplyResources(this.TextTools, "TextTools");
            this.TextTools.Name = "TextTools";
            this.TextTools.Click += new System.EventHandler(this.TextTools_Click);
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            resources.ApplyResources(this.separator1, "separator1");
            // 
            // RandomFilter
            // 
            this.RandomFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.RandomFilter, "RandomFilter");
            this.RandomFilter.Image = global::SimplePainterNamespace.Properties.Resources.random;
            this.RandomFilter.Name = "RandomFilter";
            this.RandomFilter.Click += new System.EventHandler(this.RandomFilter_Click);
            // 
            // RezolutionTools
            // 
            this.RezolutionTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.RezolutionTools, "RezolutionTools");
            this.RezolutionTools.Image = global::SimplePainterNamespace.Properties.Resources.Rename;
            this.RezolutionTools.Name = "RezolutionTools";
            this.RezolutionTools.Click += new System.EventHandler(this.RezolutionTools_Click);
            // 
            // separator2
            // 
            this.separator2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.separator2.Name = "separator2";
            resources.ApplyResources(this.separator2, "separator2");
            // 
            // CloseAll
            // 
            this.CloseAll.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseAll.Image = global::SimplePainterNamespace.Properties.Resources.CloseAll;
            resources.ApplyResources(this.CloseAll, "CloseAll");
            this.CloseAll.Name = "CloseAll";
            this.CloseAll.Click += new System.EventHandler(this.CloseAll_Click);
            // 
            // ReloadOriginal
            // 
            this.ReloadOriginal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ReloadOriginal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ReloadOriginal, "ReloadOriginal");
            this.ReloadOriginal.Image = global::SimplePainterNamespace.Properties.Resources.reload;
            this.ReloadOriginal.Name = "ReloadOriginal";
            this.ReloadOriginal.Click += new System.EventHandler(this.ReloadOriginal_Click);
            // 
            // StatsImage
            // 
            this.StatsImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.StatsImage, "StatsImage");
            this.StatsImage.Image = global::SimplePainterNamespace.Properties.Resources.info;
            this.StatsImage.Name = "StatsImage";
            this.StatsImage.Click += new System.EventHandler(this.StatsImage_Click);
            // 
            // separator3
            // 
            this.separator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.separator3.Name = "separator3";
            resources.ApplyResources(this.separator3, "separator3");
            // 
            // SomeTools
            // 
            this.SomeTools.BackColor = System.Drawing.SystemColors.MenuBar;
            resources.ApplyResources(this.SomeTools, "SomeTools");
            this.SomeTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SomeTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomView,
            this.Separator0,
            this.TextTools,
            this.separator1,
            this.RandomFilter,
            this.RezolutionTools,
            this.separator2,
            this.CloseAll,
            this.ReloadOriginal,
            this.StatsImage,
            this.separator3});
            this.SomeTools.Name = "SomeTools";
            // 
            // AdvancedEditor
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ToolLayout);
            this.KeyPreview = true;
            this.MainMenuStrip = this.MyMenu;
            this.Name = "AdvancedEditor";
            
            this.ToolLayout.ResumeLayout(false);
            this.ToolLayout.PerformLayout();
            this.MyMenu.ResumeLayout(false);
            this.MyMenu.PerformLayout();
            this.DefPanel.ResumeLayout(false);
            this.SomethingsPanel.ResumeLayout(false);
            this.Somethings.ResumeLayout(false);
            this.FilterInformation.ResumeLayout(false);
            this.OriginalImagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DefaultPicture)).EndInit();
            this.OutputImagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EditedPicture)).EndInit();
            this.EditedImageMenu.ResumeLayout(false);
            this.SpecialMenuBar.ResumeLayout(false);
            this.SpecialMenuBar.PerformLayout();
            this.SomeTools.ResumeLayout(false);
            this.SomeTools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ToolLayout;
        private System.Windows.Forms.MenuStrip MyMenu;
        private System.Windows.Forms.ToolStripMenuItem FileDialog;
        private System.Windows.Forms.ToolStripMenuItem AboutDialog;
        private System.Windows.Forms.PictureBox DefaultPicture;
        private System.Windows.Forms.ToolStripMenuItem OpenImage;
        private System.Windows.Forms.ToolStripMenuItem CloseImage;
        private System.Windows.Forms.PictureBox EditedPicture;
        private System.Windows.Forms.ContextMenuStrip EditedImageMenu;
        private System.Windows.Forms.ToolStripMenuItem saveImageContext;
        private System.Windows.Forms.ToolStripMenuItem DeleteImageContext;
        private System.Windows.Forms.Panel SomethingsPanel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TreeView FiltersList;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.ToolStripTextBox TimeElapsed;
        private System.Windows.Forms.ToolStripMenuItem JPGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PNGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BMPToolStripMenuItem;
        private System.Windows.Forms.Panel DefPanel;
        private System.Windows.Forms.Panel OriginalImagePanel;
        private System.Windows.Forms.Panel OutputImagePanel;
        private System.Windows.Forms.ToolStripMenuItem FiltersMenuList;
        private System.Windows.Forms.ToolStrip SpecialMenuBar;
        private System.Windows.Forms.ToolStripButton HideShowLeft;
        private System.Windows.Forms.ToolStripMenuItem ViewMode;
        private System.Windows.Forms.ToolStripMenuItem NormalViewMode;
        private System.Windows.Forms.ToolStripMenuItem ZoomViewMode;
        private System.Windows.Forms.ToolStripMenuItem saveImageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jPGToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bMPToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pNGToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem Screenshot;
        private System.Windows.Forms.ToolStripMenuItem главныйЭкранToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem всеЭкраныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem активноеОкноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem областьToolStripMenuItem;
        private System.Windows.Forms.TabControl Somethings;
        private System.Windows.Forms.TabPage FilterInformation;
        private System.Windows.Forms.RichTextBox information;
        private System.Windows.Forms.ToolStrip SomeTools;
        private System.Windows.Forms.ToolStripButton ZoomView;
        private System.Windows.Forms.ToolStripSeparator Separator0;
        private System.Windows.Forms.ToolStripButton TextTools;
        private System.Windows.Forms.ToolStripSeparator separator1;
        private System.Windows.Forms.ToolStripButton RandomFilter;
        private System.Windows.Forms.ToolStripButton RezolutionTools;
        private System.Windows.Forms.ToolStripSeparator separator2;
        private System.Windows.Forms.ToolStripButton CloseAll;
        private System.Windows.Forms.ToolStripButton ReloadOriginal;
        private System.Windows.Forms.ToolStripButton StatsImage;
        private System.Windows.Forms.ToolStripSeparator separator3;
    }
}

