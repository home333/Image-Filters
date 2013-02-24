using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SimplePainterNamespace
{
    /// <summary>
    ///     �����, �������������� ������������ ������ ������ � �������������
    /// </summary>
    public unsafe class BitmapUnsafeMethods
    {
        /// <summary>
        ///     ������� �����������
        /// </summary>
        private readonly Bitmap _workingBitmap;

        /// <summary>
        ///     �������� ��������� �����������
        /// </summary>
        private BitmapData _bitmapData;

        /// <summary>
        ///     �������� ������ �� ���������
        /// </summary>
        private Byte* _pBase = null;

        /// <summary>
        ///     ��������� ��������
        /// </summary>
        private PixelData* _pixelData = null;

        /// <summary>
        ///     ������
        /// </summary>
        private int _width;

        /// <summary>
        ///     ����������� ������
        /// </summary>
        /// <param name="inputBitmap">����������� ��� ������</param>
        public BitmapUnsafeMethods(Bitmap inputBitmap)
        {
            _workingBitmap = inputBitmap;
        }

        /// <summary>
        ///     �������� ���� ���������� ������� �� ����������
        ///     WARNING! �� ����������� ��������� ������� �������
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
        ///     ��������� ����������� �� ������������ � ������ ��� ������
        ///     �� ��������� ALPHA �����!
        ///     ��������� bitmapData, pBase
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
        ///     ���������� ����� ������ �� ������������� � ������ �����������
        /// </summary>
        /// <param name="bytescount">���������� ������ ��� �����������</param>
        /// <returns>������ ������</returns>
        public byte[] ReturnBytesFromLockImage(int bytescount)
        {
            var outputarr = new byte[bytescount];
            Marshal.Copy((IntPtr) _pBase, outputarr, 0, bytescount);
            return outputarr;
        }

        /// <summary>
        ///     ��������� ����������� �� ������������ � ������ ��� ������
        ///     ��������� ALPHA �����!
        ///     ��������� bitmapData, pBase
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
        ///     ���������� ��������� �� ��������� �����������
        /// </summary>
        /// <returns>��������� �� ��������� �����������</returns>
        public IntPtr ReturnBitmapDataPointer()
        {
            return (IntPtr) _pBase;
        }

        /// <summary>
        ///     �������� ������� �� �����������
        /// </summary>
        /// <param name="xcoord">���������� X</param>
        /// <param name="ycoord">���������� Y</param>
        /// <returns>���� �������</returns>
        public Color GetPixel(int xcoord, int ycoord)
        {
            _pixelData = (PixelData*) (_pBase + ycoord*_width + xcoord*sizeof (PixelData));
            return Color.FromArgb(_pixelData->Alpha, _pixelData->Red, _pixelData->Green, _pixelData->Blue);
        }

        /// <summary>
        ///     �������� ���� ���������� �������
        /// </summary>
        /// <param name="xcoord">���������� X</param>
        /// <param name="ycoord">���������� Y</param>
        /// <param name="color">���� �������</param>
        public void SetPixel(int xcoord, int ycoord, Color color)
        {
            var data = (PixelData*) (_pBase + ycoord*_width + xcoord*sizeof (PixelData));
            data->Alpha = color.A;
            data->Red = color.R;
            data->Green = color.G;
            data->Blue = color.B;
        }

        /// <summary>
        ///     ������������ ����������� � ������
        /// </summary>
        public void UnlockImage()
        {
            _workingBitmap.UnlockBits(_bitmapData);
            _bitmapData = null;
            _pBase = null;
        }

        /// <summary>
        ///     ����� �������������� ����������� �� ������ ������
        /// </summary>
        /// <param name="input">����� ������, �������������� �����������</param>
        /// <param name="width">������ �����������</param>
        /// <param name="height">������ �����������</param>
        /// <returns>���������� Bitmap</returns>
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
        ///     ����� ��� ��������� ������� ������ � �����������
        /// </summary>
        /// <param name="input">����������� ��� ��������� ������ ������</param>
        /// <returns>
        ///     ����� ������ � �����������
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
        ///     ��������� ������ �������
        ///     ��������� ����� ToString();
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