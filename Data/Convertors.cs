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
                    photo[x, y] = new Pixel(p.R / 255.0, p.G / 255.0, p.B / 255.0);
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

        public static Pixel PixelToHSL(Pixel pixel)
        {
            var r = pixel.R;
            var g = pixel.G;
            var b = pixel.B;
            double h;
            double s;

            var max = Math.Max(Math.Max(r, g),b);
            var min= Math.Min(Math.Min(r, g), b);
            double l = 0.5 * (max + min);

            if (max == r && g >= b)
                h = 60 * ((g - b) / (max - min)) + 0;
            else if (max == r && g < b)
                h = 60 * ((g - b) / (max - min)) + 360;
            else if (max == g)
                h = 60 * ((b - r) / (max - min)) + 120;
            else if (max == b)
                h = 60 * ((r - g) / (max - min)) + 240;
            else
                throw new NotImplementedException();

            if (l == 0 || max == min)
                s = 0;
            else if (l > 0 && l <= 0.5)
                s = (max - min) / (max + min);
            else if (l > 0.5 && l < 1)
                s = (max - min) / (2 - (max + min));
            else
                s = (max - min) / (1 - Math.Abs(1 - (max + min)));

            pixel.H = h;
            pixel.L = l;
            pixel.S = s;

            return pixel;
        }

        public static Pixel HSLToPixel(Pixel pixel, double l)
        {
            double q;
            if (l < 0.5)
                q = l * (1.0 + pixel.S);
            else
                q = l + pixel.S - (l * pixel.S);

            var p = 2.0 * l - q;

            var h = pixel.H / 360;
            var tr = h + (1 / 3);
            var tg = h;
            var tb = h - (1 / 3);

            tr = CheckTColor(tr);
            tg = CheckTColor(tg);
            tb = CheckTColor(tb);

            pixel.R = CountColor(tr, p, q);
            pixel.G = CountColor(tg, p, q);
            pixel.B = CountColor(tb, p, q);

            return pixel;
        }

        static double CheckTColor(double c)
        {
            if (c < 0)
                return c + 1.0;
            else if (c > 1)
                return c - 1;
            else
                return c;
        }

        static double CountColor(double c, double p, double q)
        {
            if (c < (1 / 6))
                return p + ((q - p) * 6.0 * c);
            else if (c >= (1 / 6) && c < 0.5)
                return q;
            else if (c >= 0.5 && c < (2 / 3))
                return p + ((q - p) * ((2 / 3) - c) * 6.0);
            else
                return p;
        }
    }
}
