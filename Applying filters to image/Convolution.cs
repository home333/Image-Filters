using System.Drawing;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, предоставляющий метод применения функции свертки для изображения
    /// </summary>
    internal static class Convolution
    {
        /// <summary>
        /// </summary>
        /// <param name="input">Изображение для применения метода свертки</param>
        /// <param name="kernelmatrix">Апертура (иначе говоря ядро свертки)</param>
        /// <returns>Изображения с примененным фильтром</returns>
        public static Bitmap StartConvolution(Bitmap input, double[][] kernelmatrix)
        {
            byte[] inputBytes = BitmapUnsafeMethods.GetBytes(input);
            var outputBytes = new byte[inputBytes.Length];

            int width = input.Width;
            int height = input.Height;

            // Jagged array (faster, but another syntax)
            int kernelWidth = kernelmatrix[1].GetLength(0);
            int kernelHeight = kernelmatrix.Length;

            //
            double rSum, gSum, bSum, kSum;
            double kernelVal;
            byte r, g, b;
            int pixelPosX, pixelPosY;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rSum = 0;
                    gSum = 0;
                    bSum = 0;
                    kSum = 0;
                    for (int i = 0; i < kernelWidth; i++)
                    {
                        for (int j = 0; j < kernelHeight; j++)
                        {
                            pixelPosX = x + (i - (kernelWidth/2));
                            pixelPosY = y + (j - (kernelHeight/2));
                            if ((pixelPosX < 0) ||
                                (pixelPosX >= width) ||
                                (pixelPosY < 0) ||
                                (pixelPosY >= height)) continue;

                            r = inputBytes[4*(width*pixelPosY + pixelPosX) + 0];
                            g = inputBytes[4*(width*pixelPosY + pixelPosX) + 1];
                            b = inputBytes[4*(width*pixelPosY + pixelPosX) + 2];

                            kernelVal = kernelmatrix[i][j];

                            rSum += r*kernelVal;
                            gSum += g*kernelVal;
                            bSum += b*kernelVal;

                            kSum += kernelVal;
                        }
                    }

                    if (kSum <= 0) kSum = 1;

                    rSum /= kSum;
                    if (rSum < 0) rSum = 0;
                    if (rSum > 255) rSum = 255;

                    gSum /= kSum;
                    if (gSum < 0) gSum = 0;
                    if (gSum > 255) gSum = 255;

                    bSum /= kSum;
                    if (bSum < 0) bSum = 0;
                    if (bSum > 255) bSum = 255;


                    outputBytes[4*(width*y + x) + 0] = (byte) rSum;
                    outputBytes[4*(width*y + x) + 1] = (byte) gSum;
                    outputBytes[4*(width*y + x) + 2] = (byte) bSum;
                }
            }

            return BitmapUnsafeMethods.GetBitmap(outputBytes, width, height);
        }
    }
}