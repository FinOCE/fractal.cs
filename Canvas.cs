using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fractal
{
    interface IRenderProperties
    {
        int Width {get; set;}
        int Height {get; set;}
        double X {get; set;}
        double Y {get; set;}
        int Zoom {get; set;}
        int Iterations {get; set;}
        Color Color {get; set;}
    }

    class RenderProperties : IRenderProperties
    {
        public int Width {get; set;}
        public int Height {get; set;}
        public double X {get; set;}
        public double Y {get; set;}
        public int Zoom {get; set;}
        public int Iterations {get; set;}
        public Color Color {get; set;}

        public RenderProperties((int, int) size, (double, double) position, int zoom, int iterations, Color color)
        {
            var (width, height) = size;
            Width = width;
            Height = height;

            var (x, y) = position;
            X = x;
            Y = y;

            Zoom = zoom;
            Iterations = iterations;
            Color = color;
        }
    }

    class Canvas
    {
        public int Width {get; set;}
        public int Height {get; set;}
        public double X {get; set;}
        public double Y {get; set;}
        public int Zoom {get; set;}
        public int Iterations {get; set;}
        public Color Color {get; set;}
        public Bitmap Image {get; set;}

        public Canvas(IRenderProperties RenderProperties)
        {
            Width = RenderProperties.Width;
            Height = RenderProperties.Height;
            X = RenderProperties.X;
            Y = RenderProperties.Y;
            Zoom = RenderProperties.Zoom;
            Iterations = RenderProperties.Iterations;
            Color = RenderProperties.Color;
        }

        public void Render(Func<int, (double, double), Color, Color> callback)
        {
            Bitmap image = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            double halfRelHeight = (double)Height/(double)Zoom/2d;
            double halfRelWidth = (double)Width/(double)Zoom/2d;
            double zoomFactor = 1d/(double)Zoom;

            for (int y = 0; y < Height; y++)
            {
                double j = Y - halfRelHeight + zoomFactor*(double)y;

                for (int x = 0; x < Width; x++)
                {
                    double i = X - halfRelWidth + zoomFactor*(double)x;

                    Color pixel = callback(Iterations, (i, j), Color);
                    image.SetPixel(x, y, pixel);
                }
            }

            Image = image;
        }

        public void SaveToFile(string location)
        {
            Image.Save(location + $"/Mandelbrot {{{Iterations}}} [{Width}x{Height}] ({X},{Y}i) {Zoom}x.jpg", ImageFormat.Jpeg);
        }
    }
}