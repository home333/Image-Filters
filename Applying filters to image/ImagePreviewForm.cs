using System.Drawing;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    /// <summary>
    /// Класс, предоставляющий основные методы формы "превью"
    /// </summary>
    public partial class ImagePreviewForm : Form
    {
        /// <summary>
        /// Конструктор 
        /// </summary>
        public ImagePreviewForm()
        {
            InitializeComponent();
            ChangedPicture.Image = ChangedBitmap;
        }

        #region Variables
        /// <summary>
        /// Измененное изображение
        /// </summary>
        private static Bitmap ChangedBitmap;
        /// <summary>
        /// Оригинальное изображение
        /// </summary>
        private static Bitmap OriginalBitmap;

        #endregion Variables

        /// <summary>
        /// Метод очистки всех зависимых объектов от данных
        /// </summary>
        public void CleanAllPictures()
        {
            OriginalBitmap = null;
            OrigPicture.Image = null;
            ChangedBitmap = null;
            ChangedPicture.Image = null;
        }

        /// <summary>
        /// Метод очистки объектов с оригинальным изображением
        /// </summary>
        public void DelOriginal()
        {
            OriginalBitmap = null;
            OrigPicture.Image = null;
        }

        /// <summary>
        /// Метод очистки объектов с оригинальным изображением
        /// 
        /// </summary>
        public void DelOutput()
        {
            ChangedBitmap = null;
            ChangedPicture.Image = null;
        }

        /// <summary>
        /// Метод заполнения всех объектов для измененного изображения
        /// </summary>
        /// <param name="input">Новое изображение</param>
        public void SetBitmap(Bitmap input)
        {
            ChangedBitmap = input;
            ChangedPicture.Image = ChangedBitmap;
        }
        /// <summary>
        /// Метод заполнения всех объектов для измененного оригинального изображения
        /// </summary>
        /// <param name="input"></param>
        public void SetDefaultBitmap(Bitmap input)
        {
            OriginalBitmap = input;
            OrigPicture.Image = OriginalBitmap;
        }
        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImagePreviewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}