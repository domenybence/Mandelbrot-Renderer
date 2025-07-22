using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Drawing.Processing;

namespace Mandelbrot {
    internal class FractalRenderer {
        private const double GLOBAL_WIDTH = 3;
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public Complex Center { get; set; }
        public double Zoom { get; set; }
        public int Iterations { get; set; }

        public FractalRenderer(int imagewidth, int imageheight, Complex center, double zoom, int iterations) {
            ImageWidth = imagewidth;
            ImageHeight = imageheight;
            Center = center;
            Zoom = zoom;
            Iterations = iterations;
        }

        public Image<Rgb24> Render() {
            Rgb24[] pixels = new Rgb24[ImageWidth * ImageHeight];

            for (int y = 0; y < ImageHeight; y++) {
                for (int x = 0; x < ImageWidth; x++) {
                    int pixelId = y * ImageWidth + x;

                    double u = (double)x * 2 / ImageWidth - 1;
                    double v = (double)y * 2 / ImageHeight - 1;

                    u *= (double)ImageWidth / ImageHeight;

                    Complex uv = new Complex(u,v);
                    Complex c = Center + uv / Zoom;

                    double i = MandelbrotValue(c) / Iterations;

                    byte r = (byte)Math.Clamp(i * 255, 0, 255);
                    byte g = (byte)Math.Clamp(i * 255, 0, 255);
                    byte b = (byte)Math.Clamp(i * 255, 0, 255);

                    pixels[pixelId] = new Rgb24(r, g, b);
                }
            }
            return Image.LoadPixelData<Rgb24>(pixels, ImageWidth, ImageHeight);
        }

        private double MandelbrotValue(Complex c) {
            Complex z = Complex.Zero;
            int i = 0;
            for ( ; i < Iterations; i++) {
                z = z * z + c;
                if (z.Real * z.Real + z.Imaginary * z.Imaginary > 100) break;
            }
            return i - Math.Log2(Math.Log(z.Magnitude)/Math.Log(10));
        }
    }
}
