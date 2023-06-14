using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public static class Convertors
    {
        public static Photo BitmapToPhoto(Bitmap bmp) 
        {
            var photo = new Photo(bmp.Width, bmp.Height);

            for (var x = 0; x < bmp.Width; x++)
                for (var y = 0; y < bmp.Height; y++) 
                {
                    var p = bmp.GetPixel(x, y);

                    photo[x, y] = GetPixelWithHSL(p.R, p.B, p.G);
                }
            return photo;
        }

        public static Bitmap PhotoToBitmap(Photo photo) 
        {
            var bmp = new Bitmap(photo.Width, photo.Height);
            for (var x = 0; x < photo.Width; x++)
                for (var y = 0; y < photo.Height; y++) 
                    bmp.SetPixel(x, y, Color.FromArgb(
                        (int)Math.Round(photo[x, y].R * 255), 
                        (int)Math.Round(photo[x, y].G * 255), 
                        (int)Math.Round(photo[x, y].B * 255)));
            return bmp;
        }
        public static Pixel GetPixelWithHSL(double red,double blue, double green )
        {
            var r = red / 255;
            var g = green / 255;
            var b = blue / 255;
            double h;
            double s;
            var max = Math.Max(Math.Max(r, g), b);
            var min = Math.Min(Math.Min(r, g), b);
            double l = 0.5 * (max + min);

            if (max == min)
                h = 0;
            else if (max == r && g >= b)
                h = 60 * ((g - b) / (max - min)) + 0;
            else if (max == r && g < b)
                h = 60 * ((g - b) / (max - min)) + 360;
            else if (max == g)
                h = 60 * ((b - r) / (max - min)) + 120;
            else if (max == b)
                h = 60 * ((r - g) / (max - min)) + 240;
            else
                throw new NotImplementedException("Присворение H");

            if (l == 0 || max == min)
                s = 0;
            else if (l > 0 && l <= 0.5)
                s = (max - min) / (max + min);
            else if (l > 0.5 && l < 1)
                s = (max - min) / (2 - (max + min));
            else
                s = (max - min) / (1 - Math.Abs(1 - (max + min)));

            return new Pixel(r, g, b, h, s, l);
        }
        public static Pixel HSLToPixelGamma(Pixel p, double gamma)
        {
            var c = (1 - Math.Abs((2 * gamma) - 1)) * p.S;
            var x = c * (1 - Math.Abs((p.H / 60 % 2) - 1));
            var m = gamma - (c / 2);
            double r;
            double g;
            double b;
            if (p.H >= 0 && p.H < 60)
            {
                r = c + m;
                g = x + m;
                b = 0 + m;
            }
            else if (p.H >= 60 && p.H < 120)
            {
                r = x + m;
                g = c + m;
                b = 0 + m;
            }
            else if (p.H >= 120 && p.H < 180)
            {
                r = 0 + m;
                g = c + m;
                b = x + m;
            }
            else if (p.H >= 180 && p.H < 240)
            {
                r = 0 + m;
                g = x + m;
                b = c + m;
            }
            else if (p.H >= 240 && p.H < 300)
            {
                r = x + m;
                g = 0 + m;
                b = c + m;
            }
            else if (p.H >= 300 && p.H < 360)
            {
                r = c + m;
                g = 0 + m;
                b = x + m;
            }
            else
                throw new ArgumentException("ToRGBGamma wrong");

            return new Pixel(r, g, b);
        }
    }
}
