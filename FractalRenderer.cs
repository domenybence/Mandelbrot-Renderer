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
        public int Width { get; set; }
        public int Height { get; set; }
        public Complex Center { get; set; }
        public double Zoom { get; set; }
        public int Iterations { get; set; }

        public FractalRenderer(int width, int height, Complex center, double zoom, int iterations) {
            Width = width;
            Height = height;
            Center = center;
            Zoom = zoom;
            Iterations = iterations;
        }

        public Image<Rgb24> Render() {
            Rgb24[] pixels = new Rgb24[Width * Height];

            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    int pixelId = y * Width + x;

                    double u = (double)x * 2 / Width - 1;
                    double v = (double)y * 2 / Height - 1;

                    byte r = (byte)Math.Clamp(u * 255,0,255);
                    byte g = (byte)Math.Clamp(v * 255,0,255);
                    byte b = 0;

                    pixels[pixelId] = new Rgb24(r, g, b);
                }
            }
            return Image.LoadPixelData<Rgb24>(pixels, Width, Height);
        }

        private int MandelbrotValue(Complex c) {
            Complex z = Complex.Zero;
            int i = 0;
            for ( ; i < Iterations; i++) {
                z = z * z + c;
                if (z.Real * z.Real + z.Imaginary * z.Imaginary > 4) break;
            }
            return i;
        }
    }
}
