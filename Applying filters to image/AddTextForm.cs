using System;
using System.Drawing;
using System.Windows.Forms;


namespace SimplePainterNamespace
{
    /// <summary>
    /// Класс, наследуемый от "Form". Реализует графический интерфейс.
    /// В данном случае окно текстовых эффектов
    /// </summary>
    public partial class AddTextForm : Form
    {
        /// <summary>
        /// Цвет фона
        /// </summary>
        private Color FontColor = Color.Black;

        /// <summary>
        /// ВАходное изображение
        /// </summary>
        private Bitmap inputbitmap;

        /// <summary>
        /// Даенные о шрифте
        /// </summary>
        private Font MyFont;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public AddTextForm()
        {
            InitializeComponent();
            MyFont = new Font("Calibri", 13F);
            CurrentFont.Text += "  " + MyFont.Name.ToString() + "; Размер:" + MyFont.Size.ToString()  ;
        }
        /// <summary>
        /// Конструктор с изображением
        /// </summary>
        /// <param name="input">Изображение для редактирования</param>
        public AddTextForm(Bitmap input)
        {
            InitializeComponent();
            inputbitmap = input;
            MyFont = new Font("Calibri", 13F);
            CurrentFont.Text += "  " + MyFont.Name.ToString() + "; Размер:" + MyFont.Size.ToString();
            PositionBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Публичное свойство для доступа к измененному изображению
        /// </summary>
        public Bitmap GetBitmap
        {
            get { return inputbitmap; }
        }
        /// <summary>
        ///  Публичное свойство для доступа к данным о шрифте
        /// </summary>
        public Font GetFont
        {
            get { return MyFont; }
        }
        /// <summary>
        /// Тело цикла добавления текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            PointF point = new PointF();
            switch (PositionBox.Items.IndexOf(PositionBox.Text))
            {
                case (0): point.X = 0 + 6;
                    point.Y = 0 + 6;
                    break;
                case (1):
                    point.X = 0 + 6;
                    point.Y = inputbitmap.Height - MyFont.Height - (MyFont.Height / 2) - 8;
                    break;
                case (2):
                    point.X = inputbitmap.Width - (MyFont.Size * StringToDraw.Text.Length);
                    point.Y = 0 + 6;
                    break;
                case (3): point.X = inputbitmap.Width - (MyFont.Size * StringToDraw.Text.Length);
                    point.Y = inputbitmap.Height - MyFont.Height - (MyFont.Height / 2) - 8;
                    break;
            }
            using (Graphics g = Graphics.FromImage(inputbitmap))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.DrawString(StringToDraw.Text, MyFont, new SolidBrush(FontColor), point);
            }
            this.Close();
        }

        private void AddEffects_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Обработка нажатия кнопки указания шрифта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFont_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.ShowColor = true;
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                FontColor = dlg.Color;
                MyFont = dlg.Font;
                CurrentFont.Text = "Текущий шрифт: " + MyFont.Name.ToString() + "; Размер:" + MyFont.Size.ToString();
            }
            
        }
        /// <summary>
        /// Небольшой метод обработки CheckBox пользовательсой позиции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserPositionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (UserPositionCB.Checked) { UsersCoords.Enabled = false; }
            else { UsersCoords.Enabled = true; }
        }

        private void AddTextForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
