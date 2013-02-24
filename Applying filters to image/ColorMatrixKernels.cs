namespace SimplePainterNamespace
{
    /// <summary>
    ///     Класс, содержащий наборы матриц для применения ColorMatrix
    ///     P.S Такой вид массивов работает быстрее
    ///     TypeName
    ///     PreferJaggedArraysOverMultidimensional
    ///     CheckId
    ///     CA1814
    ///     Категория
    ///     Microsoft.Performance
    ///     Критическое изменение
    ///     Критическое изменение
    /// </summary>
    internal static class ColorMatrixKernels
    {
        public static readonly float[][] Grayscale =
            {
                new[] {.3f, .3f, .3f, 0, 0},
                new[] {.59f, .59f, .59f, 0, 0},
                new[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            };

        public static readonly float[][] Invert =
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, -1}
            };

        public static readonly float[][] Sepia =
            {
                new[] {0.393f, 0.349f, 0.272f, 0, 0},
                new[] {0.769f, 0.686f, 0.534f, 0, 0},
                new[] {0.189f, 0.168f, 0.131f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            };
    }
}