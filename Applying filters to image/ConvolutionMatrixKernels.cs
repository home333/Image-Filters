namespace SimplePainterNamespace
{
    /// <summary>
    /// Сборник готовых матриц для использования в трансформации изображения методом свертки
    /// </summary>
    internal static class PredefinedKernels
    {
          
        public readonly static double[][] Laplasian = {new double[] {-1, -1, -1, -1, -1},
                                                    new double[] {-1, -1, -1, -1, -1},
                                                    new double[] {-1, -1, 24, -1, -1},
                                                    new double[] {-1, -1, -1, -1, -1},
                                                    new double[] {-1, -1, -1, -1, -1}};
           
        public readonly static double[][] Sobel1 = { new double[] { -1, 0, 1 }, new double[] { -2, 0, 2 }, new double[] { -1, 0, 1 } };
        public readonly static double[][] Sobel2 = { new double[] { -1, -2, -1 }, new double[] { 0, 0, 0 }, new double[] { 1, 2, 1 } };
       
        public readonly static double[][] EdgeDetect = { new double[] { 0, -1, 0 }, new double[] { -1, 4, -1 }, new double[] { 0, -1, 0 } };
        public readonly static double[][] EdgeUp = { new double[] { 0, 0, 0 }, new double[] { -1, 1, 0 }, new double[] { 0, 0, 0 } };
        public readonly static double[][] MeanRemoval = { new double[] { 1, 1, 1 }, new double[] { 1, 1, 1 }, new double[] { 1, 1, 1 } };
        public readonly static double[][] GaussianBlur = {
                                                           new double[] { 1, 2, 3, 2, 1 },
                                                           new double[] { 2, 4, 5, 4, 2 },
                                                           new double[] { 3, 5, 6, 5, 3 },
                                                           new double[] { 2, 4, 5, 4, 2 },
                                                           new double[] { 1, 2, 3, 2, 1 }};

        public readonly static double[][] KontrastCheck = {
                                                           new double[] {0	,0	,0	,0	,0},
                                                           new double[] {0,	0	,-1	,0	,0},
                                                           new double[] {0,	-1	,5	,-1,	0},
                                                           new double[] {0	,0	,-1,	0,	0},
                                                           new double[] {0,	0	,0	,0	,0}
                                                         };

        public readonly static double[][] Neon = { new double[] { -1, -20, -1 }, new double[] { 0, 0, 0 }, new double[] { 1, 20, 1 } };
        public readonly static double[][] Relief = { new double[] { -2, -1, 0 }, new double[] { -1, 1, 1 }, new double[] { 0, 1, 2 } };
        public readonly static double[][] Sharpen = { new double[] { 0, -2, 0 }, new double[] { -2, 11, -2 }, new double[] { 0, -2, 0 } };
    }
}