using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, представляющий небезопасные методы работы с изображениями
    /// </summary>
    public unsafe class BitmapUnsafeMethods
    {
        /// <summary>
        ///     Рабочее изображение
        /// </summary>
        private readonly Bitmap _workingBitmap;

        /// <summary>
        ///     Атрибуты точечного изображения
        /// </summary>
        private BitmapData _bitmapData;

        /// <summary>
        ///     Байтовая ссылка на указатель
        /// </summary>
        private Byte* _pBase = null;

        /// <summary>
        ///     Структура пикселей
        /// </summary>
        private PixelData* _pixelData = null;

        /// <summary>
        ///     Ширина
        /// </summary>
        private int _width;

        /// <summary>
        ///     Конструктор класса
        /// </summary>
        /// <param name="inputBitmap">изображение для работы</param>
        public BitmapUnsafeMethods(Bitmap inputBitmap)
        {
            _workingBitmap = inputBitmap;
        }

        /// <summary>
        ///     Получает цвет следующего пикселя из аттрибутов
        ///     WARNING! Не учитывается матричная позиция пикселя
        /// </summary>
        public Color GetPixelNext
        {
            get
            {
                _pixelData++;
                return Color.FromArgb(_pixelData->Alpha, _pixelData->Red, _pixelData->Green, _pixelData->Blue);
            }
        }

        /// <summary>
        ///     Блокирует изображение из конструктора в памяти для работы
        ///     НЕ УЧИТЫВАЕТ ALPHA КАНАЛ!
        ///     Заполняет bitmapData, pBase
        /// </summary>
        public void LockImageWithoutAlpha()
        {
            var bounds = new Rectangle(Point.Empty, _workingBitmap.Size);

            _width = bounds.Width*sizeof (PixelData);
            if (_width%4 != 0) _width = 4*(_width/4 + 1);

            //Lock Image
            _bitmapData = _workingBitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            _pBase = (Byte*) _bitmapData.Scan0.ToPointer();
        }

        /// <summary>
        ///     Возвращает набор байтов из закрепленного в памяти изображения
        /// </summary>
        /// <param name="bytescount">количество байтов для возвращения</param>
        /// <returns>массив байтов</returns>
        public byte[] ReturnBytesFromLockImage(int bytescount)
        {
            var outputarr = new byte[bytescount];
            Marshal.Copy((IntPtr) _pBase, outputarr, 0, bytescount);
            return outputarr;
        }

        /// <summary>
        ///     Блокирует изображение из конструктора в памяти для работы
        ///     УЧИТЫВАЕТ ALPHA КАНАЛ!
        ///     Заполняет bitmapData, pBase
        /// </summary>
        public void LockImage()
        {
            var bounds = new Rectangle(Point.Empty, _workingBitmap.Size);

            _width = bounds.Width*sizeof (PixelData);
            if (_width%4 != 0) _width = 4*(_width/4 + 1);

            //Lock Image
            _bitmapData = _workingBitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _pBase = (Byte*) _bitmapData.Scan0.ToPointer();
        }

        /// <summary>
        ///     Возвращает указатель на аттрибуты изображения
        /// </summary>
        /// <returns>указатель на аттрибуты изображения</returns>
        public IntPtr ReturnBitmapDataPointer()
        {
            return (IntPtr) _pBase;
        }

        /// <summary>
        ///     Получает пиксель по координатам
        /// </summary>
        /// <param name="xcoord">Координата X</param>
        /// <param name="ycoord">Координата Y</param>
        /// <returns>Цвет пикселя</returns>
        public Color GetPixel(int xcoord, int ycoord)
        {
            _pixelData = (PixelData*) (_pBase + ycoord*_width + xcoord*sizeof (PixelData));
            return Color.FromArgb(_pixelData->Alpha, _pixelData->Red, _pixelData->Green, _pixelData->Blue);
        }

        /// <summary>
        ///     Изменяет цвет указанного пикселя
        /// </summary>
        /// <param name="xcoord">Координата X</param>
        /// <param name="ycoord">Координата Y</param>
        /// <param name="color">Цвет пикселя</param>
        public void SetPixel(int xcoord, int ycoord, Color color)
        {
            var data = (PixelData*) (_pBase + ycoord*_width + xcoord*sizeof (PixelData));
            data->Alpha = color.A;
            data->Red = color.R;
            data->Green = color.G;
            data->Blue = color.B;
        }

        /// <summary>
        ///     Разблокирует изображение в памяти
        /// </summary>
        public void UnlockImage()
        {
            _workingBitmap.UnlockBits(_bitmapData);
            _bitmapData = null;
            _pBase = null;
        }

        /// <summary>
        ///     Метод восстановления изображения из набора байтов
        /// </summary>
        /// <param name="input">Набор байтов, представляющий изображение</param>
        /// <param name="width">Ширина изображения</param>
        /// <param name="height">высота изображения</param>
        /// <returns>Возвращает Bitmap</returns>
        public static Bitmap GetBitmap(byte[] input, int width, int height)
        {
            if (input.Length%4 != 0) throw new ArgumentException(string.Format("ERROR! BitmapData is corrupted!"));
            var output = new Bitmap(width, height);
            var run = new BitmapUnsafeMethods(output);
            run.LockImageWithoutAlpha();

            Marshal.Copy(input, 0, run.ReturnBitmapDataPointer(), input.Length);
            run.UnlockImage();
            return output;
        }

        /// <summary>
        ///     Метод для получения массива байтов с изображения
        /// </summary>
        /// <param name="input">Изображение для получения набора байтов</param>
        /// <returns>
        ///     набор байтов с изображения
        /// </returns>
        public static byte[] GetBytes(Bitmap input)
        {
            int bytesCount = input.Width*input.Height*4;
            var run = new BitmapUnsafeMethods(input);
            run.LockImageWithoutAlpha();

            byte[] output = run.ReturnBytesFromLockImage(bytesCount);

            run.UnlockImage();
            return output;
        }

        /// <summary>
        ///     Структура цветов пикселя
        ///     Описывает метод ToString();
        /// </summary>
        private struct PixelData
        {
            public byte Alpha;
            public byte Blue;
            public byte Green;
            public byte Red;

            public override string ToString()
            {
                return "(" + Alpha + ", " + Red + ", " + Green + ", " + Blue + ")";
            }
        }
    }
}