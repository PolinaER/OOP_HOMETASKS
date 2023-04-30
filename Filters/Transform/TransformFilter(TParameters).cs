using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoRed
{
    public class TransformFilter<TParameters>: ParametrisedFilter<TParameters>
        where TParameters:IParameters, new()
    {
        ITransformer<TParameters> transformer;

        public TransformFilter(string name, ITransformer<TParameters> transformer) 
        {
            this.name = name;
            this.transformer = transformer;
        }
        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.Width, original.Height);
            transformer.Initialize(oldSize, parameters);
            var result = new Photo(transformer.ResultSize.Width, transformer.ResultSize.Height);

            for(var x = 0; x < transformer.ResultSize.Width; x++)
            {
                for(var y = 0; y < transformer.ResultSize.Height; y++)
                {
                    var oldPoint = transformer.MapPoint(new Point(x, y));

                    if(oldPoint.HasValue)
                        result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y]; 
                }
            }
            return result;
        }
    }  
}
