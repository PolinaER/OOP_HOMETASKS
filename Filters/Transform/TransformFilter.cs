using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PhotoRed
{
    public class TransformFilter: TransformFilter<EmpryParameters>
    {
        public TransformFilter(string name,
            Func<Size, Size> sizeTransform, Func<Point, Size, Point> pointTransform) : 
            base(name, new SimpleTransformer(sizeTransform, pointTransform)) { }
    }
}
