using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class LightningFilter : PixelFilter<LightningParameters>
    {
        public override string ToString()
        {
            return "Осветление/Затемнение";
        }

        public override Pixel ProcessPixel(Pixel pixel, LightningParameters parametrs)
        {
            return pixel * parametrs.Coefficient;
        }
    }
}
