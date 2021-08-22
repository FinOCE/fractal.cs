using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fractal
{
    class Program
    {
        public static void Main(string[] args)
        {
            int iterations = 2000;
            int width = 2048;
            int height = 2048;
            double x = -0.518;
            double y = -0.5215;
            int zoom = 1048576;
            var color = Color.Red;

            RenderProperties renderProperties = new RenderProperties((width, height), (x, y), zoom, iterations, color);
            Canvas canvas = new Canvas(renderProperties);
            canvas.Render(Formula.Mandelbrot);
            canvas.SaveToFile("output");
        }
    }
}
