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
        public static Pixel HSLToPixel(double hue, double saturation, double lightness) 
        {
            double a1;
            if (lightness < 0.5)
                a1 = lightness * (1 + saturation);
            else
                a1 = lightness + saturation - lightness * saturation;

            double a2 = 2 * lightness - a1;
            hue = hue / 360;
            double r = CountColor(CheckTColor(hue + 0.333), a1, a2);
            double g = CountColor(CheckTColor(hue), a1, a2);
            double b = CountColor(CheckTColor(hue - 0.333), a1, a2);

            return new Pixel(r, g, b);
        }

        static double CheckTColor(double c)
        {
            if (c < 0)
                return c + 1;
            else if (c > 1)
                return c - 1;
            else
                return c;
        }

        static double CountColor(double c, double a1, double a2)
        {
            if (6 * c < 1)
                return a2 + (a1 - a2) * 6 * c;
            else if (2 * c < 1)
                return a1;
            else if (3 * c < 2)
                return a2 + (a1 - a2) * (0.666 - c) * 6;
            else
                return a2;
        }
    }
}
