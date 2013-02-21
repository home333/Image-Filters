using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SimplePainterNamespace
{
    /// <summary>
    /// �����, �������������� ������������ ������ ������ � �������������
    /// </summary>
    unsafe public class BitmapUnsafeMethods
    {
        /// <summary>
        /// ��������� ������ �������
        /// ��������� ����� ToString();
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
        /// ������� �����������
        /// </summary>
        private Bitmap workingBitmap = null;
        /// <summary>
        /// ������
        /// </summary>
        private int width = 0;
        /// <summary>
        /// �������� ��������� �����������
        /// </summary>
        private BitmapData bitmapData = null;
        /// <summary>
        /// �������� ������ �� ���������
        /// </summary>
        private Byte* pBase = null;

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="inputBitmap">����������� ��� ������</param>
        public BitmapUnsafeMethods(Bitmap inputBitmap)
        {
            workingBitmap = inputBitmap;
        }

        /// <summary>
        /// ��������� ����������� �� ������������ � ������ ��� ������
        /// �� ��������� ALPHA �����!
        /// ��������� bitmapData, pBase
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
        /// ���������� ����� ������ �� ������������� � ������ �����������
        /// </summary>
        /// <param name="bytescount">���������� ������ ��� �����������</param>
        /// <returns>������ ������</returns>
        public byte[] ReturnBytesFromLockImage(int bytescount)
        {
            byte[] outputarr = new byte[bytescount];
            System.Runtime.InteropServices.Marshal.Copy((IntPtr)pBase, outputarr, 0, bytescount);
            return outputarr;
        }

        /// <summary>
        /// ��������� ����������� �� ������������ � ������ ��� ������
        ///��������� ALPHA �����!
        /// ��������� bitmapData, pBase
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
        /// ���������� ��������� �� ��������� �����������
        /// </summary>
        /// <returns>��������� �� ��������� �����������</returns>
        public IntPtr ReturnBitmapDataPointer()
        {
            return (IntPtr)pBase;
        }

        /// <summary>
        /// ��������� ��������
        /// </summary>
        private PixelData* pixelData = null;

        /// <summary>
        /// �������� ������� �� �����������
        /// </summary>
        /// <param name="xcoord">���������� X</param>
        /// <param name="ycoord">���������� Y</param>
        /// <returns>���� �������</returns>
        public Color GetPixel(int xcoord, int ycoord)
        {
            pixelData = (PixelData*)(pBase + ycoord * width + xcoord * sizeof(PixelData));
            return Color.FromArgb(pixelData->alpha, pixelData->red, pixelData->green, pixelData->blue);
        }

        /// <summary>
        ///  �������� ���� ���������� ������� �� ����������
        ///  WARNING! �� ����������� ��������� ������� �������
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
        /// �������� ���� ���������� �������
        /// </summary>
        /// <param name="xcoord">���������� X</param>
        /// <param name="ycoord">���������� Y</param>
        /// <param name="color">���� �������</param>
        public void SetPixel(int xcoord, int ycoord, Color color)
        {
            PixelData* data = (PixelData*)(pBase + ycoord * width + xcoord * sizeof(PixelData));
            data->alpha = color.A;
            data->red = color.R;
            data->green = color.G;
            data->blue = color.B;
        }

        /// <summary>
        /// ������������ ����������� � ������
        /// </summary>
        public void UnlockImage()
        {
            workingBitmap.UnlockBits(bitmapData);
            bitmapData = null;
            pBase = null;
        }

        /// <summary>
        /// ����� �������������� ����������� �� ������ ������
        /// </summary>
        /// <param name="input">����� ������, �������������� �����������</param>
        /// <param name="width">������ �����������</param>
        /// <param name="height">������ �����������</param>
        /// <returns>���������� Bitmap</returns>
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
        /// ����� ��� ��������� ������� ������ � �����������
        /// </summary>
        /// <param name="input">����������� ��� ��������� ������ ������</param>
        /// <returns>
        /// ����� ������ � �����������
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