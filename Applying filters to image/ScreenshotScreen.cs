using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SimplePainterNamespace
{
    internal class ScreenshotScreen
    {
        public static Bitmap ScreenshotedRegion;

        /// <summary> Position of the cursor relative to the start of the capture </summary>
        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")] private static Point
            _cursorPosition;

        public static Bitmap AllScreenScreenShot()
        {
            Rectangle rectanglezise = Rectangle.Empty;

            foreach (Screen s in Screen.AllScreens)
                rectanglezise = Rectangle.Union(rectanglezise, s.Bounds);

            var screenshot = new Bitmap(rectanglezise.Width, rectanglezise.Height,
                                        PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(rectanglezise.X, rectanglezise.Y,
                                 0, 0, rectanglezise.Size, CopyPixelOperation.SourceCopy);
            }
            return screenshot;
        }

        public static Bitmap ScreenShotPrimaryScreen()
        {
            var screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                        Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                 Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size,
                                 CopyPixelOperation.SourceCopy);
            }
            return screenshot;
        }

        /// <summary> Capture a specific window and return it as a bitmap </summary>
        /// <param name="handle">hWnd (handle) of the window to capture</param>
        public static Bitmap CaptureActiveWindow(IntPtr handle)
        {
            var rect = new NativeMethods.Rect();
            NativeMethods.GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            _cursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);

            var screenshot = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

            return screenshot;
        }
    }
}