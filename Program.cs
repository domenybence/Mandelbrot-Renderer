using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using System.Numerics;

namespace Mandelbrot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //center of cam - complex
            //camera width - int
            //camera height - int
            //camera zoom - double
            //camera width(3) in coordinate system

            Complex center = new Complex(0, 0);
            int width = 800;
            int height = 600;
            double zoom = 1;
            int iterations = 50;

            FractalRenderer renderer = new FractalRenderer(width, height, center, zoom, iterations);
            Image<Rgb24> image = renderer.Render();

            image.Save("kep.png");

            Process.Start(new ProcessStartInfo("kep.png") { UseShellExecute = true });
        }
    }
}