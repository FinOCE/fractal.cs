using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fractal
{
    class Program
    {
        public static void Main(string[] args)
        {
            int iterations = 100;
            int width = 256;
            int height = 256;
            double x = -0.518d;
            double y = -0.5215d;
            int zoom = 1;
            var color = Color.Red;

            RenderProperties renderProperties = new RenderProperties((width, height), (x, y), zoom, iterations, color);
            Canvas canvas = new Canvas(renderProperties);
            canvas.Render(Formula.Mandelbrot);
            canvas.SaveToFile("output");
        }
    }
}
