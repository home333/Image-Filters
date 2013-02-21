using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SimplePainterNamespace
{
    /// <summary>
    /// Класс, представляющий небезопасные методы работы с изображениями
    /// </summary>
    unsafe public class BitmapUnsafeMethods
    {
        /// <summary>
        /// Структура цветов пикселя
        /// Описывает метод ToString();
        /// </summary>
        private struct PixelData
        {
            public byte blue;
            public byte green;
            public byte red;
            public byte alpha;

            public override string ToString()
            {
                return "(" + alpha.ToString() + ", " + red.ToString() + ", " + green.ToString() + ", " + blue.ToString() + ")";
            }
        }

        /// <summary>
        /// Рабочее изображение
        /// </summary>
        private Bitmap workingBitmap = null;
        /// <summary>
        /// Ширина
        /// </summary>
        private int width = 0;
        /// <summary>
        /// Атрибуты точечного изображения
        /// </summary>
        private BitmapData bitmapData = null;
        /// <summary>
        /// Байтовая ссылка на указатель
        /// </summary>
        private Byte* pBase = null;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="inputBitmap">изображение для работы</param>
        public BitmapUnsafeMethods(Bitmap inputBitmap)
        {
            workingBitmap = inputBitmap;
        }

        /// <summary>
        /// Блокирует изображение из конструктора в памяти для работы
        /// НЕ УЧИТЫВАЕТ ALPHA КАНАЛ!
        /// Заполняет bitmapData, pBase
        /// </summary>
        public void LockImageWithoutAlpha()
        {
            Rectangle bounds = new Rectangle(Point.Empty, workingBitmap.Size);

            width = (int)(bounds.Width * sizeof(PixelData));
            if (width % 4 != 0) width = 4 * (width / 4 + 1);

            //Lock Image
            bitmapData = workingBitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
            pBase = (Byte*)bitmapData.Scan0.ToPointer();
        }

        /// <summary>
        /// Возвращает набор байтов из закрепленного в памяти изображения
        /// </summary>
        /// <param name="bytescount">количество байтов для возвращения</param>
        /// <returns>массив байтов</returns>
        public byte[] ReturnBytesFromLockImage(int bytescount)
        {
            byte[] outputarr = new byte[bytescount];
            System.Runtime.InteropServices.Marshal.Copy((IntPtr)pBase, outputarr, 0, bytescount);
            return outputarr;
        }

        /// <summary>
        /// Блокирует изображение из конструктора в памяти для работы
        ///УЧИТЫВАЕТ ALPHA КАНАЛ!
        /// Заполняет bitmapData, pBase
        /// </summary>
        public void LockImage()
        {
            Rectangle bounds = new Rectangle(Point.Empty, workingBitmap.Size);

            width = (int)(bounds.Width * sizeof(PixelData));
            if (width % 4 != 0) width = 4 * (width / 4 + 1);

            //Lock Image
            bitmapData = workingBitmap.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            pBase = (Byte*)bitmapData.Scan0.ToPointer();
        }

        /// <summary>
        /// Возвращает указатель на аттрибуты изображения
        /// </summary>
        /// <returns>указатель на аттрибуты изображения</returns>
        public IntPtr ReturnBitmapDataPointer()
        {
            return (IntPtr)pBase;
        }

        /// <summary>
        /// Структура пикселей
        /// </summary>
        private PixelData* pixelData = null;

        /// <summary>
        /// Получает пиксель по координатам
        /// </summary>
        /// <param name="xcoord">Координата X</param>
        /// <param name="ycoord">Координата Y</param>
        /// <returns>Цвет пикселя</returns>
        public Color GetPixel(int xcoord, int ycoord)
        {
            pixelData = (PixelData*)(pBase + ycoord * width + xcoord * sizeof(PixelData));
            return Color.FromArgb(pixelData->alpha, pixelData->red, pixelData->green, pixelData->blue);
        }

        /// <summary>
        ///  Получает цвет следующего пикселя из аттрибутов
        ///  WARNING! Не учитывается матричная позиция пикселя
        /// </summary>
        public Color GetPixelNext
        {
            get
            {
                pixelData++;
                return Color.FromArgb(pixelData->alpha, pixelData->red, pixelData->green, pixelData->blue);
            }
        }

        /// <summary>
        /// Изменяет цвет указанного пикселя
        /// </summary>
        /// <param name="xcoord">Координата X</param>
        /// <param name="ycoord">Координата Y</param>
        /// <param name="color">Цвет пикселя</param>
        public void SetPixel(int xcoord, int ycoord, Color color)
        {
            PixelData* data = (PixelData*)(pBase + ycoord * width + xcoord * sizeof(PixelData));
            data->alpha = color.A;
            data->red = color.R;
            data->green = color.G;
            data->blue = color.B;
        }

        /// <summary>
        /// Разблокирует изображение в памяти
        /// </summary>
        public void UnlockImage()
        {
            workingBitmap.UnlockBits(bitmapData);
            bitmapData = null;
            pBase = null;
        }

        /// <summary>
        /// Метод восстановления изображения из набора байтов
        /// </summary>
        /// <param name="input">Набор байтов, представляющий изображение</param>
        /// <param name="width">Ширина изображения</param>
        /// <param name="height">высота изображения</param>
        /// <returns>Возвращает Bitmap</returns>
        public static Bitmap GetBitmap(byte[] input, int width, int height)
        {
            if (input.Length % 4 != 0) throw new ArgumentException(string.Format("ERROR! BitmapData is corrupted!"));
            Bitmap output = new Bitmap(width, height);
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImageWithoutAlpha();

                System.Runtime.InteropServices.Marshal.Copy(input, 0, run.ReturnBitmapDataPointer(), input.Length);
                run.UnlockImage();
                return output;
            }
        }

        /// <summary>
        /// Метод для получения массива байтов с изображения
        /// </summary>
        /// <param name="input">Изображение для получения набора байтов</param>
        /// <returns>
        /// набор байтов с изображения
        /// </returns>
        public static byte[] GetBytes(Bitmap input)
        {
            int bytesCount = input.Width * input.Height * 4;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(input);
                run.LockImageWithoutAlpha();

                byte[] output = run.ReturnBytesFromLockImage(bytesCount);

                run.UnlockImage();
                return output;
            }
        }
    }
}