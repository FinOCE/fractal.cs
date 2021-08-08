using System;
using System.IO;
using Cudafy;

namespace Fractal
{
    class Program
    {
        public static void Main(string[] args)
        {
            int iterations = 100;
            int width = 8;
            int height = 8;
            double x = -0.518d;
            double y = -0.5215d;
            int zoom = 1;
            var color = (255, 0, 0);

            RenderData RenderData = new RenderData(iterations, (width, height), (x, y), zoom, color);
            ImageData ImageData = Render(RenderData, Formula.Mandelbrot);
            SaveRenderToImage(ImageData);
        }

        [Cudafy]
        public static ImageData Render(RenderData RenderData, Func<int, (double, double), (int, int, int), byte[]> callback)
        {
            byte[] imageBuffer = new byte[RenderData.Width * RenderData.Height * 4];

            int index = 0;

            double halfAdjustedHeight = (double)(RenderData.Height)/(double)(RenderData.Zoom)/2d;
            double halfAdjustedWidth = (double)(RenderData.Width)/(double)(RenderData.Zoom)/2d;
            double zoomFactor = 1d/(double)(RenderData.Zoom);

            for (double j = RenderData.Y - halfAdjustedHeight; j < RenderData.Y + halfAdjustedHeight; j += zoomFactor)
            {
                for (double i = RenderData.X - halfAdjustedWidth; i < RenderData.X + halfAdjustedWidth; i += zoomFactor)
                {
                    byte[] pixel = callback(RenderData.Iterations, (i, j), RenderData.Color);
                    imageBuffer[index] = 255;
                    imageBuffer[index + 1] = pixel[0];
                    imageBuffer[index + 2] = pixel[1];
                    imageBuffer[index + 3] = pixel[2];
                    index += 4;
                }
            }

            return new ImageData(RenderData, imageBuffer);
        }

        public static void SaveRenderToImage(ImageData ImageData)
        {
            string base64Data = Convert.ToBase64String(ImageData.ImageBuffer);
            string img = string.Format("data:image/jpg;base64,{0}", base64Data);

            File.WriteAllText($"output/Mandelbrot {{{ImageData.Iterations}}} [{ImageData.Width}x{ImageData.Height}] ({ImageData.X},{ImageData.Y}i) {ImageData.Zoom}x.png", img);
        }
    }
}
