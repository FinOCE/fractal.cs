namespace Fractal
{
    class RenderData
    {
        public int Iterations {get; set;}
        public int Width {get; set;}
        public int Height {get; set;}
        public double X {get; set;}
        public double Y {get; set;}
        public int Zoom {get; set;}
        public (int, int, int) Color {get; set;}

        public RenderData(RenderData RenderData)
        {
            Iterations = RenderData.Iterations;
            Width = RenderData.Width;
            Height = RenderData.Height;
            X = RenderData.X;
            Y = RenderData.Y;
            Zoom = RenderData.Zoom;
        }

        public RenderData(int iterations, (int, int) screen, (double, double) position, int zoom, (int, int, int) color)
        {
            var (w, h) = screen;
            var (x, y) = position;

            Iterations = iterations;
            Width = w;
            Height = h;
            X = x;
            Y = y;
            Zoom = zoom;
            Color = color;
        }
    }

    class ImageData : RenderData
    {
        public byte[] ImageBuffer {get; set;}

        public ImageData(RenderData RenderData, byte[] imageBuffer) : base(RenderData)
        {
            Iterations = RenderData.Iterations;
            Width = RenderData.Width;
            Height = RenderData.Height;
            X = RenderData.X;
            Y = RenderData.Y;
            Zoom = RenderData.Zoom;
            ImageBuffer = imageBuffer;
        }
    }
}
