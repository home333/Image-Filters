using System;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, наследуемый от "Form". Реализует графический интерфейс.
    ///     В данном случае окно текстовых эффектов
    /// </summary>
    public partial class AddTextForm : Form
    {
        /// <summary>
        ///     Входное изображение
        /// </summary>
        private readonly Bitmap _inputbitmap;

        /// <summary>
        ///     Цвет фона
        /// </summary>
        private Color _fontColor = Color.Black;

        /// <summary>
        ///     Даенные о шрифте
        /// </summary>
        private Font _myFont;

        /// <summary>
        ///     Стандартный конструктор
        /// </summary>
        public AddTextForm()
        {
            InitializeComponent();
            _myFont = new Font("Calibri", 13F);
            CurrentFont.Text += "  " + _myFont.Name.ToString(CultureInfo.InvariantCulture) + "; Размер:" +
                                _myFont.Size.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Конструктор с изображением
        /// </summary>
        /// <param name="input">Изображение для редактирования</param>
        public AddTextForm(Bitmap input)
        {
            InitializeComponent();
            _inputbitmap = input;
            _myFont = new Font("Calibri", 13F);
            CurrentFont.Text += "  " + _myFont.Name + "; Размер:" + _myFont.Size.ToString(CultureInfo.InvariantCulture);
            PositionBox.SelectedIndex = 0;
        }

        /// <summary>
        ///     Публичное свойство для доступа к измененному изображению
        /// </summary>
        public Bitmap GetBitmap
        {
            get { return _inputbitmap; }
        }

        /// <summary>
        ///     Публичное свойство для доступа к данным о шрифте
        /// </summary>
        public Font GetFont
        {
            get { return _myFont; }
        }

        /// <summary>
        ///     Тело цикла добавления текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            var point = new PointF();
            switch (PositionBox.Items.IndexOf(PositionBox.Text))
            {
                case (0):
                    point.X = 0 + 6;
                    point.Y = 0 + 6;
                    break;
                case (1):
                    point.X = 0 + 6;
                    point.Y = _inputbitmap.Height - _myFont.Height - (_myFont.Height/2) - 8;
                    break;
                case (2):
                    point.X = _inputbitmap.Width - (_myFont.Size*StringToDraw.Text.Length);
                    point.Y = 0 + 6;
                    break;
                case (3):
                    point.X = _inputbitmap.Width - (_myFont.Size*StringToDraw.Text.Length);
                    point.Y = _inputbitmap.Height - _myFont.Height - (_myFont.Height/2) - 8;
                    break;
            }
            using (Graphics g = Graphics.FromImage(_inputbitmap))
            {
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.DrawString(StringToDraw.Text, _myFont, new SolidBrush(_fontColor), point);
            }
            Close();
        }

        private void AddEffects_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Обработка нажатия кнопки указания шрифта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFont_Click(object sender, EventArgs e)
        {
            var dlg = new FontDialog {ShowColor = true};
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                _fontColor = dlg.Color;
                _myFont = dlg.Font;
                CurrentFont.Text = "Текущий шрифт: " + _myFont.Name + "; Размер:" +
                                   _myFont.Size.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        ///     Небольшой метод обработки CheckBox пользовательсой позиции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserPositionCB_CheckedChanged(object sender, EventArgs e)
        {
            UsersCoords.Enabled = !UserPositionCB.Checked;
        }

        private void AddTextForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}