using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
namespace SimplePainterNamespace
{
    /// <summary>
    /// Класс, содержащий методы работы с изображением в цветовом диапазоне (работа с пикселями)
    /// </summary>
    static class ColorDiapasoneFilters
    {

        public static Bitmap MedianFilter(Bitmap inputimage)
        {
            //Ширина изображения (для уменьшения обращений к свойству объекта)
            int width = inputimage.Width;
            //Высота изображения (для уменьшения обращений к свойству объекта)
            int height = inputimage.Height;
            //Инициализация объектов для хранения данных о цветовых компонентов и цветах пикселей
            List<byte> r = new List<byte>();
            List<byte> g = new List<byte>();
            List<byte> b = new List<byte>();
            List<Color> lst = new List<Color>();
            byte[] inputBytes = BitmapUnsafeMethods.GetBytes(inputimage);
           
            for (int y = 1; y < height - 2; y++)
            {
                for (int x = 1; x < width - 2; x++)
                {
                    r.Clear(); g.Clear(); b.Clear(); lst.Clear();
                    //Соседние пиксели от текущего

                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * y + (x - 1)) + 0], inputBytes[4 * (width * y + (x - 1)) + 1], inputBytes[4 * (width * y + (x - 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * y + x) + 0], inputBytes[4 * (width * y + x) + 1], inputBytes[4 * (width * y + x) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * y + (x + 1)) + 0], inputBytes[4 * (width * y + (x + 1)) + 1], inputBytes[4 * (width * y + (x + 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * (y - 1) + (x - 1)) + 0], inputBytes[4 * (width * (y - 1) + (x - 1)) + 1], inputBytes[4 * (width * (y - 1) + (x - 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * (y - 1) + x) + 0], inputBytes[4 * (width * (y - 1) + x) + 1], inputBytes[4 * (width * (y - 1) + x) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * (y - 1) + (x + 1)) + 0], inputBytes[4 * (width * (y - 1) + (x + 1)) + 1], inputBytes[4 * (width * (y - 1) + (x + 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * (y + 1) + (x - 1)) + 0], inputBytes[4 * (width * (y + 1) + (x - 1)) + 1], inputBytes[4 * (width * (y + 1) + (x - 1)) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * (y + 1) + x) + 0], inputBytes[4 * (width * (y + 1) + x) + 1], inputBytes[4 * (width * (y + 1) + x) + 2]));
                    lst.Add(Color.FromArgb(0, inputBytes[4 * (width * (y + 1) + (x + 1)) + 0], inputBytes[4 * (width * (y + 1) + (x + 1)) + 1], inputBytes[4 * (width * (y + 1) + (x + 1)) + 2]));

                    //Разбор информации о цветах пикселей по соответствующим компонентам
                    foreach (Color elm in lst)
                    {
                        r.Add(elm.R);
                        g.Add(elm.G);
                        b.Add(elm.B);
                    }
                    //Сортировка по возрастанию
                    r.Sort(); g.Sort(); b.Sort();
                    //Закраска текущего пикселя средним значением из списка
                    inputBytes[4 * (width * y + x) + 0] = (byte)r[4];
                    inputBytes[4 * (width * y + x) + 1] = (byte)g[4];
                    inputBytes[4 * (width * y + x) + 2] = (byte)b[4];

                }
            }

            return BitmapUnsafeMethods.GetBitmap(inputBytes, width, height);
                        
        
        }

        public static Bitmap Pixelate(Bitmap input, Int32 pixelateSize = 15)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(input);
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();

            
           
                for (Int32 xx = 0; xx < width -1; xx += pixelateSize)
                {
                    for (Int32 yy =0; yy < height -1; yy += pixelateSize)
                    {
                        Int32 offsetX = pixelateSize / 2;
                        Int32 offsetY = pixelateSize / 2;

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

            
            }
            return output;
        }



        /// <summary>
        /// Метод применения фильтра "Только синий"
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap ChangeColors(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(input);
            Color color;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        byte A, R, G, B;
                        color = run.GetPixel(x, y);
                        A = (byte)color.A;
                        R = (byte)(color.R);
                        G = (byte)(color.G);
                        B = (byte)(color.B);

                        run.SetPixel(x, y, Color.FromArgb((int)A, (int)G, (int)B, (int)R));
                    }
                }
                run.UnlockImage();
            }
            return output;
        }

        /// <summary>
        /// Метод для поворота изображения на 90 градусов по часовой стрелке
        /// </summary>
        /// <param name="input">изображение для обработки</param>
        /// <returns>Повернутое на 90 градусов по часовой стрелке изображение</returns>
        /// 
        public static Bitmap Clockwise(Bitmap input)
        {
            int width = input.Height;
            int height = input.Width;
            Bitmap output = new Bitmap(width, height);
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                BitmapUnsafeMethods toout = new BitmapUnsafeMethods(input);
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
            }
            return output;
        }

        public static Bitmap Shadow(Bitmap input, int Distance = 13, int Opacity=150)
        {
            Bitmap ShadowImg;
            Bitmap outputimg;
            int shWidth = input.Width / 11;
            int shHeight = input.Height / 11;
            ShadowImg = new Bitmap(shWidth, shHeight);
            using (Graphics g = Graphics.FromImage(ShadowImg))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillRectangle(new SolidBrush(Color.FromArgb(Opacity, Color.Black)),
                                     1, 1, shWidth, shHeight);

            }

            int d_shWidth = input.Width + Distance;
            int d_shHeight = input.Height + Distance;
            outputimg = new Bitmap(d_shWidth, d_shHeight);
            using (Graphics g = Graphics.FromImage(outputimg))
            {

                g.Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawImage(ShadowImg, new Rectangle(0, 0, d_shWidth, d_shHeight),
                                        0, 0, ShadowImg.Width, ShadowImg.Height, GraphicsUnit.Pixel);
                g.DrawImage(input,
                   new Rectangle(0, 0, input.Width, input.Height),
                          0, 0, input.Width, input.Height, GraphicsUnit.Pixel);

            }
            return outputimg;
        }
        /// <summary>
        ///  Метод для горизонтального отражения изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Mirroring(Bitmap input)
        {
            Bitmap output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();
                Color clr;
                Color clr2;
                int z;

                for (int y = 0; y < height - 1; y++)
                {
                    z = 0;
                    for (int x = width - 1; x > z; x--)
                    {
                        clr = run.GetPixel(x, y);
                        clr2 = run.GetPixel(z, y);
                        run.SetPixel(x, y, clr2);
                        run.SetPixel(z, y, clr);
                        z++;
                    }
                }
                run.UnlockImage();
            }

            return output;
        }

        /// <summary>
        /// Метод применения фильтра "Сдвиг" (вертикальный) для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap ShiftVertical(Bitmap input)
        {
            Bitmap output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            Random rand = new Random();

            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();

                for (int x = 0; x <= width - 1; x++)
                {
                    if (x % 2 != 0)
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
            }
            return output;
        }

        /// <summary>
        /// Метод применения фильтра "Сдвиг" (горизонтальный) для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Shift(Bitmap input)
        {
            Bitmap output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            Random rand = new Random();

            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();

                for (int y = 0; y <= height - 1; y++)
                {
                    if (y % 2 != 0)
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
            }
            return output;
        }

        /// <summary>
        /// Метод для поворота изображения на 90 градусов против часовой стрелки
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Anticlockwise(Bitmap input)
        {
            int width = input.Height;
            int height = input.Width;
            Bitmap output = new Bitmap(width, height);

            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                BitmapUnsafeMethods toout = new BitmapUnsafeMethods(input);
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
            }

            return output;
        }

        /// <summary>
        /// Метод для горизонтального отражения изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap VerticalMap(Bitmap input)
        {
            Bitmap output = new Bitmap(input);
            int width = output.Width;
            int height = output.Height;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();
                Color clr;
                Color clr2;
                int z;

                for (int x = 0; x < width - 1; x++)
                {
                    z = 0;
                    for (int y = height - 1; y > z; y--)
                    {
                        clr = run.GetPixel(x, y);
                        clr2 = run.GetPixel(x, z);
                        run.SetPixel(x, y, clr2);
                        run.SetPixel(x, z, clr);
                        z++;
                    }
                }
                run.UnlockImage();
            }

            return output;
        }
        /// <summary>
        /// Метод применения фильтра "Шум SaltAndPepper" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap NoiseSaltAndPepper(Bitmap input)
        {
            //Иничиализация объекта генерации случайных чисел для адресов пикселей
            //Иничиализация объекта генерации случайных чисел для случайности цвета
            Random myrandom = new Random();
            Random clrrnd = new Random();
            //Ширина изображения (для уменьшения обращений к свойству объекта)
            int width = input.Width;
            //Высота изображения (для уменьшения обращений к свойству объекта)
            int height = input.Height;
            //Количество пикселей на изображении
            int PixelsCount = width * height;
            //Количество пикселей для генерации шума
            int PixelsPersent = (10 * PixelsCount) / 100;
            Bitmap output = new Bitmap(input);
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();
                //Цикл по всем выбранным пикселям на изображении
                for (int i = 1; i <= PixelsPersent; i++)
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
            }
         
            
            return output;
        }
        /// <summary>
        /// Метод применения фильтра "Шум" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="percent">Процент применения</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Noise(Bitmap input, byte percent=25)
        {
            Bitmap output = new Bitmap(input);
            Random myrandom = new Random();
            int width = input.Width;
            int height = input.Height;
            int PixelsCount = width * height;
            int PixelsPersent = (percent * PixelsCount) / 100;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();

                for (int i = 1; i <= PixelsPersent; i++)
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
            }

            return output;
        }

        /// <summary>
        /// Метод применения фильтра "Только синий"
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap OnlyBlue(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(input);
            Color color;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);

                run.LockImage();
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        byte A, R, G, B;
                        color = run.GetPixel(x, y);
                        A = color.A;
                        R = (byte)(color.R * 0);
                        G = (byte)(color.G * 0);
                        B = (byte)(color.B);

                        run.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    }
                }
                run.UnlockImage();
            }
            return output;
        }

        /// <summary>
        /// Метод применения фильтра "Только красный"
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Armageddon(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(input);
            Color color;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                byte A, R, G, B;
                run.LockImage();
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        color = run.GetPixel(x, y);
                        A = color.A;
                        R = (byte)(color.R);
                        G = (byte)(color.G * 0);
                        B = (byte)(color.B * 0);

                        run.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    }
                }
                run.UnlockImage();
            }
            return output;
        }


        /// <summary>
        /// Метод применения фильтра "Ночной режим" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Nightvision(Bitmap input)
        {
            int width = input.Width;
            int height = input.Height;
            Bitmap output = new Bitmap(input);
            Color color;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                byte A, R, G, B;
                run.LockImage();
                for (int y = 0; y < height - 1; y++)
                {
                    for (int x = 0; x < width - 1; x++)
                    {
                        color = run.GetPixel(x, y);
                        A = color.A;
                        R = (byte)(color.R * 0);
                        G = (byte)(color.G);
                        B = (byte)(color.B * 0);

                        run.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    }
                }
                run.UnlockImage();
            }
            return output;
        }


        /// <summary>
        /// Метод применения фильтра "Рассеивание" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="percent">Процент применения</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap Dispersal(Bitmap input, byte percent = 100)
        {
            Color clr;
            Color clr2;
            Bitmap output = new Bitmap(input);
            Random myrandom = new Random();
            int width = input.Width;
            int height = input.Height;
            int PixelsCount = width * height;
            int PixelsPersent = (percent * PixelsCount) / 100;
            int Pixel1Width;
            int Pixel1Height;
            int Pixel2Width;
            int Pixel2Height;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();

                for (int i = 1; i <= PixelsPersent; i++)
                {
                    Pixel1Width = myrandom.Next(0, width);
                    Pixel1Height = myrandom.Next(0, height);

                    Pixel2Width = myrandom.Next(Pixel1Width - 5, Pixel1Width + 1 + 5);
                    Pixel2Height = myrandom.Next(Pixel1Height - 5, Pixel1Height + 1 + 5);
                    while ((Pixel2Width > Pixel1Width) || (Pixel2Width < 0))
                    {
                        Pixel2Width = myrandom.Next(Pixel1Width - 5, Pixel1Width + 1 + 5);
                    }
                    while ((Pixel2Height > Pixel1Height) || (Pixel2Height < 0))
                    {
                        Pixel2Height = myrandom.Next(Pixel1Height - 5, Pixel1Height + 1 + 5);
                    }

                    clr = run.GetPixel(Pixel2Width, Pixel2Height);
                    clr2 = run.GetPixel(Pixel1Width, Pixel1Height);
                    run.SetPixel(Pixel1Width, Pixel1Height, clr);
                    run.SetPixel(Pixel2Width, Pixel2Height, clr2);
                }
                run.UnlockImage();
            }

            return output;
        }


        /// <summary>
        /// Метод применения фильтра "Случайный выбор" для изображения
        /// </summary>
        /// <param name="input">Изображение</param>
        /// <param name="percent">Процент применения</param>
        /// <returns>Изображение с фильтром</returns>
        public static Bitmap RandomSelect(Bitmap input, byte percent = 100)
        {
            Color clr;
            Bitmap output = new Bitmap(input);
            Random myrandom = new Random();
            int width = input.Width;
            int height = input.Height;
            int PixelsCount = width * height;
            int PixelsPersent = (percent * PixelsCount) / 100;
            int PixelToSetColorWidth;
            int PixelToSetColorHeight;
            int CoordXToGrabColor;
            int CoordYToGrabColor;
            unsafe
            {
                BitmapUnsafeMethods run = new BitmapUnsafeMethods(output);
                run.LockImage();

                for (int i = 1; i <= PixelsPersent; i++)
                {
                    PixelToSetColorWidth = myrandom.Next(0, width);
                    PixelToSetColorHeight = myrandom.Next(0, height);

                    CoordXToGrabColor = myrandom.Next(PixelToSetColorWidth - 5, PixelToSetColorWidth + 1 + 5);
                    CoordYToGrabColor = myrandom.Next(PixelToSetColorHeight - 5, PixelToSetColorHeight + 1 + 5);
                    while ((CoordXToGrabColor > PixelToSetColorWidth) || (CoordXToGrabColor < 0))
                    {
                        CoordXToGrabColor = myrandom.Next(PixelToSetColorWidth - 5, PixelToSetColorWidth + 1 + 5);
                    }
                    while ((CoordYToGrabColor > PixelToSetColorHeight) || (CoordYToGrabColor < 0))
                    {
                        CoordYToGrabColor = myrandom.Next(PixelToSetColorHeight - 5, PixelToSetColorHeight + 1 + 5);
                    }
                    clr = run.GetPixel(CoordXToGrabColor, CoordYToGrabColor);
                    run.SetPixel(PixelToSetColorWidth, PixelToSetColorHeight, clr);
                }
                run.UnlockImage();
            }

            return output;
        }


        /// <summary>
        /// Метод применения фильтра "Сепия" для изображения
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
        /// Метод применения фильтра "оттенки серого" для изображения
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
        /// Метод для инвертирования изображения
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
        /// Извлеченный метод из методов работы с изображением способом ColorMatrix (для уменьшения повторяемости)
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
            ColorMatrix matrix = new ColorMatrix(matrref);
            attr.SetColorMatrix(matrix);
        }
    }
}