using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, наследуемый от "Form". Реализует графический интерфейс
    ///     В данном случае - окно выбора нового размера изображения
    /// </summary>
    public partial class ChangeResolution : Form
    {
        
        /// <summary>
        ///     Контейнер изображения для работы
        /// </summary>
        private Bitmap _workBitmap;

        /// <summary>
        ///     Конструктор с изображением
        /// </summary>
        /// <param name="input">Изображение для последуюзей работы</param>
        public ChangeResolution(Bitmap input)
        {
            InitializeComponent();
            if (input != null)
            {
                _workBitmap = input;
                DefHeightTextBox.Text = input.Height.ToString();
                DefWidthTextBox.Text = input.Width.ToString();
            }
        }

        /// <summary>
        ///     Основной конструктор
        ///     Оставлен для будущего применения.
        ///     НЕ ИСПОЛЬЗОВАТЬ
        /// </summary>
        public ChangeResolution()
        {
            InitializeComponent();

        }

        /// <summary>
        ///     Возвращает измененное изображение
        /// </summary>
        public Bitmap ReturnImage
        {
            get { return _workBitmap; }
        }

        /// <summary>
        ///     Обработчик нажатия кнопки "Применить"
        ///     Запускает преобразование и закрывает форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickStart_Button(object sender, EventArgs e)
        {
            if (NewWidthTextBox.TextLength != 0 || NewHeightTextBox.TextLength != 0)
            {
                _workBitmap = NewResolution(Convert.ToInt32(NewWidthTextBox.Text),
                                            Convert.ToInt32(NewHeightTextBox.Text));
                Close();
            }
        }

        /// <summary>
        ///     Метод изменения размеров изображения в WorkImage
        /// </summary>
        /// <param name="x">новая ширина</param>
        /// <param name="y">новая высота</param>
        /// <returns>новое изображение</returns>
        private Bitmap NewResolution(int x, int y)
        {
            var size = new Size {Width = x, Height = y};
            var output = new Bitmap(_workBitmap, size);
            return output;
        }
    }
}