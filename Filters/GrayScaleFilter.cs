using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class GrayScaleFilter : PixelFilter<EmpryParameters>
    {
        public override string ToString()
        {
            return "Оттенки серого";
        }

        public override Pixel ProcessPixel(Pixel pixel, EmpryParameters parametrs)
        {
            var lightness = (0.3 * pixel.R + 0.6 * pixel.G + 0.1 * pixel.R) / 3;
            return new Pixel(lightness, lightness, lightness);
        }
    }
}
