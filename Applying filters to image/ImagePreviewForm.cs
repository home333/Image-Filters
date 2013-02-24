using System.Drawing;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, предоставляющий основные методы формы "превью"
    /// </summary>
    public partial class ImagePreviewForm : Form
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public ImagePreviewForm()
        {
            InitializeComponent();
            ChangedPicture.Image = _changedBitmap;
        }

        #region Variables

        /// <summary>
        ///     Измененное изображение
        /// </summary>
        private static Bitmap _changedBitmap;

        /// <summary>
        ///     Оригинальное изображение
        /// </summary>
        private static Bitmap _originalBitmap;

        #endregion Variables

        /// <summary>
        ///     Метод очистки всех зависимых объектов от данных
        /// </summary>
        public void CleanAllPictures()
        {
            _originalBitmap = null;
            OrigPicture.Image = null;
            _changedBitmap = null;
            ChangedPicture.Image = null;
        }

        /// <summary>
        ///     Метод очистки объектов с оригинальным изображением
        /// </summary>
        public void DelOriginal()
        {
            _originalBitmap = null;
            OrigPicture.Image = null;
        }

        /// <summary>
        ///     Метод очистки объектов с оригинальным изображением
        /// </summary>
        public void DelOutput()
        {
            _changedBitmap = null;
            ChangedPicture.Image = null;
        }

        /// <summary>
        ///     Метод заполнения всех объектов для измененного изображения
        /// </summary>
        /// <param name="input">Новое изображение</param>
        public void SetBitmap(Bitmap input)
        {
            _changedBitmap = input;
            ChangedPicture.Image = _changedBitmap;
        }

        /// <summary>
        ///     Метод заполнения всех объектов для измененного оригинального изображения
        /// </summary>
        /// <param name="input"></param>
        public void SetDefaultBitmap(Bitmap input)
        {
            _originalBitmap = input;
            OrigPicture.Image = _originalBitmap;
        }

        /// <summary>
        ///     Обработчик закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImagePreviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}