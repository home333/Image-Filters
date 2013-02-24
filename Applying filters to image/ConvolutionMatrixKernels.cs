namespace SimplePainterNamespace
{
    /// <summary>
    ///     Сборник готовых матриц для использования в трансформации изображения методом свертки
    /// </summary>
    internal static class PredefinedKernels
    {
        public static readonly double[][] Laplasian =
            {
                new double[] {-1, -1, -1, -1, -1},
                new double[] {-1, -1, -1, -1, -1},
                new double[] {-1, -1, 24, -1, -1},
                new double[] {-1, -1, -1, -1, -1},
                new double[] {-1, -1, -1, -1, -1}
            };

        public static readonly double[][] Sobel1 =
            {
                new double[] {-1, 0, 1}, new double[] {-2, 0, 2},
                new double[] {-1, 0, 1}
            };

        public static readonly double[][] Sobel2 =
            {
                new double[] {-1, -2, -1}, new double[] {0, 0, 0},
                new double[] {1, 2, 1}
            };

        public static readonly double[][] EdgeDetect =
            {
                new double[] {0, -1, 0}, new double[] {-1, 4, -1},
                new double[] {0, -1, 0}
            };

        public static readonly double[][] EdgeUp =
            {
                new double[] {0, 0, 0}, new double[] {-1, 1, 0},
                new double[] {0, 0, 0}
            };

        public static readonly double[][] MeanRemoval =
            {
                new double[] {1, 1, 1}, new double[] {1, 1, 1},
                new double[] {1, 1, 1}
            };

        public static readonly double[][] GaussianBlur =
            {
                new double[] {1, 2, 3, 2, 1},
                new double[] {2, 4, 5, 4, 2},
                new double[] {3, 5, 6, 5, 3},
                new double[] {2, 4, 5, 4, 2},
                new double[] {1, 2, 3, 2, 1}
            };

        public static readonly double[][] KontrastCheck =
            {
                new double[] {0, 0, 0, 0, 0},
                new double[] {0, 0, -1, 0, 0},
                new double[] {0, -1, 5, -1, 0},
                new double[] {0, 0, -1, 0, 0},
                new double[] {0, 0, 0, 0, 0}
            };

        public static readonly double[][] Neon =
            {
                new double[] {-1, -20, -1}, new double[] {0, 0, 0},
                new double[] {1, 20, 1}
            };

        public static readonly double[][] Relief =
            {
                new double[] {-2, -1, 0}, new double[] {-1, 1, 1},
                new double[] {0, 1, 2}
            };

        public static readonly double[][] Sharpen =
            {
                new double[] {0, -2, 0}, new double[] {-2, 11, -2},
                new double[] {0, -2, 0}
            };
    }
}