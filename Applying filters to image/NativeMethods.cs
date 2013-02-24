using System;
using System.Runtime.InteropServices;

namespace SimplePainterNamespace
{
    internal static class NativeMethods
    {
        /// <summary>
        ///     Вовращает указатель на активное окно Windows
        /// </summary>
        /// <returns>IntPtr на окно</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        /// <summary>
        ///     Возвращает область указанного окна
        /// </summary>
        /// <param name="hWnd">HW окна</param>
        /// <param name="rect">структура для возврата данных области</param>
        /// <returns>true - все ок, иначе false</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}