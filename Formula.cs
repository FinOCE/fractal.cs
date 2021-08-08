using System;
using Cudafy;

namespace Fractal
{
    class Formula
    {
        [Cudafy]
        public static byte[] Mandelbrot(int iterations, (double, double) position, (int, int, int) color)
        {
            var (r, g, b) = color;

            var (i, j) = position;
            double real = i;
            double imag = j;

            int nt = iterations;
            while (nt > 0)
            {
                double temp = Math.Pow(real, 2) - Math.Pow(imag, 2) + i;
                imag = 2 * real * imag + j;
                real = temp;

                nt--;

                if (real * imag > 5) break;
            }

            double dis = (iterations - nt)/iterations;

            return new byte[3]
            {
                (byte)(Math.Round(dis * r)),
                (byte)(Math.Round(dis * g)),
                (byte)(Math.Round(dis * b))
            };
        }
    }
}
