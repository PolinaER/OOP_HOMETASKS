using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class GammaCorrectionFilter : PixelFilter<GammaCorrectionParameters>
    {
        public override string ToString()
        {
            return "Гамма-коррекция";
        }

        public override Pixel ProcessPixel(Pixel pixel, GammaCorrectionParameters parametrs)
        {
            return Convertors.HSLToPixel(pixel.H, pixel.S, Math.Pow(pixel.L, 1/parametrs.Gamma));
        }
    }
}
