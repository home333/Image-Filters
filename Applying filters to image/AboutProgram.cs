using System;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    partial class AboutProgram : Form
    {
        public AboutProgram()
        {
            InitializeComponent();
            this.Text += String.Format("О программе {0}", "");
            this.labelProductName.Text += ": " + "Simple Painter";
            this.labelVersion.Text = String.Format("Версия {0}", "0.5.0.0");
            this.labelCopyright.Text += ": " + "DarkHunter";
            this.labelCompanyName.Text += ": " + "NO_COMPANY";
            this.textBoxDescription.Text += ": " + " Данная программа реализует работу с изображениями и их фильтрование. В наличие более 25 различных фильтров для обработки и некоторые дополнительные возможности.";
        }

        private void AboutProgram_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}