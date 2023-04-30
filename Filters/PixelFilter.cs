using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class PixelFilter<TParameters> : ParametrisedFilter<TParameters>
                where TParameters : IParameters, new()
    {
        Func<Pixel, TParameters, Pixel> processor; 

        public PixelFilter(string name, Func<Pixel, TParameters, Pixel> processor)
        {
            this.name = name;
            this.processor = processor;
        }
        public override Photo Process(Photo original, TParameters parametrs)
        {
                var newPhoto = new Photo(original.Width, original.Height);

                for (var x = 0; x < original.Width; x++)
                    for (var y = 0; y < original.Height; y++)
                        newPhoto[x, y] = processor(original[x, y], parametrs);

                return newPhoto;
        }
    }
}
