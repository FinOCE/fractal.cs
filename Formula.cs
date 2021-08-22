using System;
using System.Drawing;

namespace Fractal
{
    class Formula
    {
        public static Color Mandelbrot(int iterations, (double, double) position, Color color)
        {
            var (i, j) = position;
            double real = i;
            double imag = j;

            int nt = iterations;
            while (nt > 0)
            {
                double temp = Math.Pow(real, 2) - Math.Pow(imag, 2) + i;
                imag = (double)2 * real * imag + j;
                real = temp;

                nt--;

                if (real * imag > 5) break;
            }

            double dis = (iterations - nt)/iterations;

            return Color.FromArgb(
                (byte)(dis * color.R),
                (byte)(dis * color.G),
                (byte)(dis * color.B)
            );
        }
    }
}
