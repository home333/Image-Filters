using System;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    sealed partial class AboutProgram : Form
    {
        public AboutProgram()
        {
            InitializeComponent();
            Text += String.Format("О программе {0}", "");
            labelProductName.Text += ": " + "Simple Painter";
            labelVersion.Text = String.Format("Версия {0}", "0.5.0.0");
            labelCopyright.Text += ": " + "DarkHunter";
            labelCompanyName.Text += ": " + "NO_COMPANY";
            textBoxDescription.Text += ": " +
                                       " Данная программа реализует работу с изображениями и их фильтрование. В наличие более 25 различных фильтров для обработки и некоторые дополнительные возможности.";
        }

        private void AboutProgram_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}