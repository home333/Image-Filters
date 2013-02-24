using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, содержащий методы работы с изображением в цветовом диапазоне (работа с пикселями)
    /// </summary>
    internal static class ColorDiapasoneFilters
    {
        public static Bitmap MedianFilter(Bitmap inputimage)
        {
            //Ширина изображения (для уменьшения обращений к свойству объекта)
            int width = inputimage.Width;
            //Высота изображения (для уменьшения обращений к свойству объекта)
            int height = inputimage.Height;
            //Инициализация объектов для хранения данных о цветовых компонентов и цветах пикселей
            var r = new List<byte>();
            var g = new List<byte>();
            var b = new List<byte>();
            var lst = new List<Color>();
            byte[] inputBytes = BitmapUnsafeMethods.GetBytes(inputimage);

            for (int y = 1; y < height - 2; y++)
            {
                for (int x = 1; x < width - 2; x++)
                {
                    r.Clear();
                    g.Clear();
                    b.Clear();
                    lst.Clear();
                    //Соседние пиксели от текущего

                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*y + (x - 1)) + 0],
                                           inputBytes[4*(width*y + (x - 1)) + 1], inputBytes[4*(width*y + (x - 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*y + x) + 0], inputBytes[4*(width*y + x) + 1],
                                           inputBytes[4*(width*y + x) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*y + (x + 1)) + 0],
                                           inputBytes[4*(width*y + (x + 1)) + 1], inputBytes[4*(width*y + (x + 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*(y - 1) + (x - 1)) + 0],
                                           inputBytes[4*(width*(y - 1) + (x - 1)) + 1],
                                           inputBytes[4*(width*(y - 1) + (x - 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*(y - 1) + x) + 0],
                                           inputBytes[4*(width*(y - 1) + x) + 1], inputBytes[4*(width*(y - 1) + x) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*(y - 1) + (x + 1)) + 0],
                                           inputBytes[4*(width*(y - 1) + (x + 1)) + 1],
                                           inputBytes[4*(width*(y - 1) + (x + 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*(y + 1) + (x - 1)) + 0],
                                           inputBytes[4*(width*(y + 1) + (x - 1)) + 1],
                                           inputBytes[4*(width*(y + 1) + (x - 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*(y + 1) + x) + 0],
                                           inputBytes[4*(width*(y + 1) + x) + 1], inputBytes[4*(width*(y + 1) + x) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4*(width*(y + 1) + (x + 1)) + 0],
                                           inputBytes[4*(width*(y + 1) + (x + 1)) + 1],
                                           inputBytes[4*(width*(y + 1) + (x + 1)) + 2]));

                    //Разбор информации о цветах пикселей по соответствующим компонентам
                    foreach (Color elm in lst)
                    {
                        r.Add(elm.R);
                        g.Add(elm.G);
                        b.Add(elm.B);
                    }
                    //Сортировка по возрастанию
                    r.Sort();
                    g.Sort();
                    b.Sort();
                    //Закраска текущего пикселя средним значением из списка
                    inputBytes[4*(width*y + x) + 0] = r[4];
                    inputBytes[4*(width*y + x) + 1] = g[4];
                    inputBytes[4*(width*y + x) + 2] = b[4];
                }
            }

            return BitmapUnsafeMethods.GetBitmap(inputBytes, width, height);
        }

        public static Bitmap Pixelate(Bitmap input, Int32 pixelateSize = 15)
        {
            int width = input.Width;
            int height = input.Height;
            var output = new Bitmap(input);
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();


            for (Int32 xx = 0; xx < width - 1; xx += pixelateSize)
            {
                for (Int32 yy = 0; yy < height - 1; yy += pixelateSize)
                {
                    Int32 offsetX = pixelateSize/2;
                    Int32 offsetY = pixelateSize/2;

                    // make sure that the offset is within the boundry of the image
                    while (xx + offsetX >= width) offsetX--;
                    while (yy + offsetY >= height) offsetY--;

                    // get the pixel color in the center of the soon to be pixelated area
                    Color pixel = run.GetPixel(xx + offsetX, yy + offsetY);

                    // for each pixel in the pixelate size, set it to the center color
                    for (Int32 x = xx; x < xx + pixelateSize && x < width; x++)
                        for (Int32 y = yy; y < yy + pixelateSize && y < height; y++)
                            run.SetPixel(x, y, pixel);
                }
            }
            run.UnlockImage();
            return output;
        }


        /// <summary>
        ///     Метод применения фильтра "Только синий"
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap ChangeColors(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            var output = new Bitmap(input);
            Color color;
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    color = run.GetPixel(x, y);
                    byte a = color.A;
                    byte r = color.R;
                    byte g = color.G;
                    byte b = color.B;

                    run.SetPixel(x, y, Color.FromArgb(a, g, b, r));
                }
            }
            run.UnlockImage();
            return output;
        }

        /// <summary>
        ///     Метод для поворота изображения на 90 градусов по часовой стрелке
        /// </summary>
        /// <param name="input">изображение для обработки</param>
        /// <returns>Повернутое на 90 градусов по часовой стрелке изображение</returns>
        public static Bitmap Clockwise(Bitmap input)
        {
            int width = input.Height;
            int height = input.Width;
            var output = new Bitmap(width, height);
            var run = new BitmapUnsafeMethods(output);
            var toout = new BitmapUnsafeMethods(input);
            run.LockImage();
            toout.LockImage();

            for (int y = 0; y < width - 1; y++)
            {
                for (int x = 0; x < height - 1; x++)
                {
                    run.SetPixel(width - 1 - (y - 1), x, toout.GetPixel(x, y));
                }
            }
            run.UnlockImage();
            toout.UnlockImage();
            return output;
        }

        public static Bitmap Shadow(Bitmap input, int distance = 13, int opacity = 150)
        {
            int shWidth = input.Width/11;
            int shHeight = input.Height/11;
            var shadowImg = new Bitmap(shWidth, shHeight);
            using (Graphics g = Graphics.FromImage(shadowImg))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRectangle(new SolidBrush(Color.FromArgb(opacity, Color.Black)),
                                1, 1, shWidth, shHeight);
            }

            int dShWidth = input.Width + distance;
            int dShHeight = input.Height + distance;
            var outputimg = new Bitmap(dShWidth, dShHeight);
            using (Graphics g = Graphics.FromImage(outputimg))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawImage(shadowImg, new Rectangle(0, 0, dShWidth, dShHeight),
                            0, 0, shadowImg.Width, shadowImg.Height, GraphicsUnit.Pixel);
                g.DrawImage(input,
                            new Rectangle(0, 0, input.Width, input.Height),
                            0, 0, input.Width, input.Height, GraphicsUnit.Pixel);
            }
            return outputimg;
        }

        /// <summary>
        ///     Метод для горизонтального отражения изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Mirroring(Bitmap input)
        {
            var output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int y = 0; y < height - 1; y++)
            {
                int z = 0;
                for (int x = width - 1; x > z; x--)
                {
                    Color clr = run.GetPixel(x, y);
                    Color clr2 = run.GetPixel(z, y);
                    run.SetPixel(x, y, clr2);
                    run.SetPixel(z, y, clr);
                    z++;
                }
            }
            run.UnlockImage();

            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "Сдвиг" (вертикальный) для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap ShiftVertical(Bitmap input)
        {
            var output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            var rand = new Random();

            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int x = 0; x <= width - 1; x++)
            {
                if (x%2 != 0)
                {
                    int ii = rand.Next(1, 5);
                    ii = rand.Next(1, 5);
                    for (int i = 1; i <= ii; i++)
                    {
                        for (int y = height - 1; y > 1; y--)
                        {
                            run.SetPixel(x, y, run.GetPixel(x, y - 1));
                        }
                    }
                }
                else
                {
                    int ii = rand.Next(1, 5);
                    ii = rand.Next(1, 5);
                    for (int i = 1; i <= ii; i++)
                    {
                        for (int y = 0; y < height - 1; y++)
                        {
                            run.SetPixel(x, y, run.GetPixel(x, y + 1));
                        }
                    }
                }
            }
            run.UnlockImage();
            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "Сдвиг" (горизонтальный) для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Shift(Bitmap input)
        {
            var output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            var rand = new Random();

            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int y = 0; y <= height - 1; y++)
            {
                if (y%2 != 0)
                {
                    int ii = rand.Next(1, 5);
                    ii = rand.Next(1, 5);
                    for (int i = 1; i <= ii; i++)
                    {
                        for (int x = width - 1; x >= 1; x--)
                        {
                            run.SetPixel(x, y, run.GetPixel(x - 1, y));
                        }
                    }
                }
                else
                {
                    int ii = rand.Next(1, 5);
                    ii = rand.Next(1, 5);
                    for (int i = 1; i <= ii; i++)
                    {
                        for (int x = 0; x <= width - 1; x++)
                        {
                            run.SetPixel(x, y, run.GetPixel(x + 1, y));
                        }
                    }
                }
            }
            run.UnlockImage();
            return output;
        }

        /// <summary>
        ///     Метод для поворота изображения на 90 градусов против часовой стрелки
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Anticlockwise(Bitmap input)
        {
            int width = input.Height;
            int height = input.Width;
            var output = new Bitmap(width, height);

            var run = new BitmapUnsafeMethods(output);
            var toout = new BitmapUnsafeMethods(input);
            run.LockImage();
            toout.LockImage();

            for (int y = 0; y <= width - 1; y++)
            {
                for (int x = 0; x <= height - 1; x++)
                {
                    run.SetPixel(y, x, toout.GetPixel(x, y));
                }
            }
            run.UnlockImage();
            toout.UnlockImage();

            return output;
        }

        /// <summary>
        ///     Метод для горизонтального отражения изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap VerticalMap(Bitmap input)
        {
            var output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int x = 0; x < width - 1; x++)
            {
                int z = 0;
                for (int y = height - 1; y > z; y--)
                {
                    Color clr = run.GetPixel(x, y);
                    Color clr2 = run.GetPixel(x, z);
                    run.SetPixel(x, y, clr2);
                    run.SetPixel(x, z, clr);
                    z++;
                }
            }
            run.UnlockImage();

            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "Шум SaltAndPepper" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap NoiseSaltAndPepper(Bitmap input)
        {
            //Иничиализация объекта генерации случайных чисел для адресов пикселей
            //Иничиализация объекта генерации случайных чисел для случайности цвета
            var myrandom = new Random();
            var clrrnd = new Random();
            //Ширина изображения (для уменьшения обращений к свойству объекта)
            int width = input.Width;
            //Высота изображения (для уменьшения обращений к свойству объекта)
            int height = input.Height;
            //Количество пикселей на изображении
            int pixelsCount = width*height;
            //Количество пикселей для генерации шума
            int pixelsPersent = (10*pixelsCount)/100;
            var output = new Bitmap(input);
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();
            //Цикл по всем выбранным пикселям на изображении
            for (int i = 1; i <= pixelsPersent; i++)
            {
                //Закраска пикселей по случайному адресу белым или черным цветом
                if (clrrnd.Next(0, 2) == 0)
                {
                    run.SetPixel(myrandom.Next(0, width), myrandom.Next(0, height), Color.FromArgb(
                        255,
                        255,
                        255,
                        255
                                                                                        )
                        );
                }
                else
                {
                    run.SetPixel(myrandom.Next(0, width), myrandom.Next(0, height), Color.FromArgb(
                        255,
                        0,
                        0,
                        0
                                                                                        )
                        );
                }
            }
            run.UnlockImage();


            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "Шум" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="percent">Процент применения</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Noise(Bitmap input, byte percent = 25)
        {
            var output = new Bitmap(input);
            var myrandom = new Random();
            int width = input.Width;
            int height = input.Height;
            int pixelsCount = width*height;
            int pixelsPersent = (percent*pixelsCount)/100;
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int i = 1; i <= pixelsPersent; i++)
            {
                run.SetPixel(myrandom.Next(0, width), myrandom.Next(0, height), Color.FromArgb(
                    myrandom.Next(255),
                    myrandom.Next(255),
                    myrandom.Next(255),
                    myrandom.Next(255)
                                                                                    )
                    );
            }
            run.UnlockImage();

            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "Только синий"
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap OnlyBlue(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            var output = new Bitmap(input);
            var run = new BitmapUnsafeMethods(output);

            run.LockImage();
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    Color color = run.GetPixel(x, y);
                    byte a = color.A;
                    var r = (byte) (color.R*0);
                    var g = (byte) (color.G*0);
                    byte b = color.B;

                    run.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            run.UnlockImage();
            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "Только красный"
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Armageddon(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            var output = new Bitmap(input);
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    Color color = run.GetPixel(x, y);
                    byte a = color.A;
                    byte r = color.R;
                    var g = (byte) (color.G*0);
                    var b = (byte) (color.B*0);

                    run.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            run.UnlockImage();
            return output;
        }


        /// <summary>
        ///     Метод применения фильтра "Ночной режим" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Nightvision(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            var output = new Bitmap(input);
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    Color color = run.GetPixel(x, y);
                    byte a = color.A;
                    var r = (byte) (color.R*0);
                    byte g = color.G;
                    var b = (byte) (color.B*0);

                    run.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }
            run.UnlockImage();
            return output;
        }


        /// <summary>
        ///     Метод применения фильтра "Рассеивание" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="percent">Процент применения</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Dispersal(Bitmap input, byte percent = 100)
        {
            var output = new Bitmap(input);
            var myrandom = new Random();
            int width = input.Width;
            int height = input.Height;
            int pixelsCount = width*height;
            int pixelsPersent = (percent*pixelsCount)/100;
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int i = 1; i <= pixelsPersent; i++)
            {
                int pixel1Width = myrandom.Next(0, width);
                int pixel1Height = myrandom.Next(0, height);

                int pixel2Width = myrandom.Next(pixel1Width - 5, pixel1Width + 1 + 5);
                int pixel2Height = myrandom.Next(pixel1Height - 5, pixel1Height + 1 + 5);
                while ((pixel2Width > pixel1Width) || (pixel2Width < 0))
                {
                    pixel2Width = myrandom.Next(pixel1Width - 5, pixel1Width + 1 + 5);
                }
                while ((pixel2Height > pixel1Height) || (pixel2Height < 0))
                {
                    pixel2Height = myrandom.Next(pixel1Height - 5, pixel1Height + 1 + 5);
                }

                Color clr = run.GetPixel(pixel2Width, pixel2Height);
                Color clr2 = run.GetPixel(pixel1Width, pixel1Height);
                run.SetPixel(pixel1Width, pixel1Height, clr);
                run.SetPixel(pixel2Width, pixel2Height, clr2);
            }
            run.UnlockImage();

            return output;
        }


        /// <summary>
        ///     Метод применения фильтра "Случайный выбор" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="percent">Процент применения</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap RandomSelect(Bitmap input, byte percent = 100)
        {
            var output = new Bitmap(input);
            var myrandom = new Random();
            int width = input.Width;
            int height = input.Height;
            int pixelsCount = width*height;
            int pixelsPersent = (percent*pixelsCount)/100;
            var run = new BitmapUnsafeMethods(output);
            run.LockImage();

            for (int i = 1; i <= pixelsPersent; i++)
            {
                int pixelToSetColorWidth = myrandom.Next(0, width);
                int pixelToSetColorHeight = myrandom.Next(0, height);

                int coordXToGrabColor = myrandom.Next(pixelToSetColorWidth - 5, pixelToSetColorWidth + 1 + 5);
                int coordYToGrabColor = myrandom.Next(pixelToSetColorHeight - 5, pixelToSetColorHeight + 1 + 5);
                while ((coordXToGrabColor > pixelToSetColorWidth) || (coordXToGrabColor < 0))
                {
                    coordXToGrabColor = myrandom.Next(pixelToSetColorWidth - 5, pixelToSetColorWidth + 1 + 5);
                }
                while ((coordYToGrabColor > pixelToSetColorHeight) || (coordYToGrabColor < 0))
                {
                    coordYToGrabColor = myrandom.Next(pixelToSetColorHeight - 5, pixelToSetColorHeight + 1 + 5);
                }
                Color clr = run.GetPixel(coordXToGrabColor, coordYToGrabColor);
                run.SetPixel(pixelToSetColorWidth, pixelToSetColorHeight, clr);
            }
            run.UnlockImage();

            return output;
        }


        /// <summary>
        ///     Метод применения фильтра "Сепия" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Sepia(Bitmap input)
        {
            Bitmap output;
            ImageAttributes attr;
            CreateOutput(input, ColorMatrixKernels.Sepia, out output, out attr);
            using (Graphics g = Graphics.FromImage(output))
            {
                g.DrawImage(input, new Rectangle(0, 0, input.Width, input.Height),
                            0, 0, input.Width, input.Height, GraphicsUnit.Pixel, attr);
            }
            return output;
        }

        /// <summary>
        ///     Метод применения фильтра "оттенки серого" для изображения
        /// </summary>
        public static Bitmap Grayscale(Bitmap input)
        {
            Bitmap output;
            ImageAttributes attr;
            CreateOutput(input, ColorMatrixKernels.Grayscale, out output, out attr);
            using (Graphics g = Graphics.FromImage(output))
            {
                g.DrawImage(input, new Rectangle(0, 0, input.Width, input.Height),
                            0, 0, input.Width, input.Height, GraphicsUnit.Pixel, attr);
            }
            return output;
        }


        /// <summary>
        ///     Метод для инвертирования изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Invert(Bitmap input)
        {
            Bitmap output;
            ImageAttributes attr;
            CreateOutput(input, ColorMatrixKernels.Invert, out output, out attr);
            using (Graphics g = Graphics.FromImage(output))
            {
                g.DrawImage(input, new Rectangle(0, 0, input.Width, input.Height),
                            0, 0, input.Width, input.Height, GraphicsUnit.Pixel, attr);
            }
            return output;
        }


        /// <summary>
        ///     Извлеченный метод из методов работы с изображением способом ColorMatrix (для уменьшения повторяемости)
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="matrref">Матрица для фильтра ColorMatrix</param>
        /// <param name="output">Изображение (чистое, с координатами)</param>
        /// <param name="attr">Новые аттрибуты для применения </param>
        /// <returns>Изображение с фильтром</returns>
        private static void CreateOutput(Bitmap input, float[][] matrref, out Bitmap output, out ImageAttributes attr)
        {
            output = new Bitmap(input.Width, input.Height);
            attr = new ImageAttributes();
            var matrix = new ColorMatrix(matrref);
            attr.SetColorMatrix(matrix);
        }
    }
}