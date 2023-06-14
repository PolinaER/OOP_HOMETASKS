using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public interface ITransformer<TParameters>
        where TParameters : IParameters, new()
    {
        void Initialize(Size size, TParameters parameters);
        Size ResultSize { get; }
        Point? MapPoint(Point newPoint);
    }
}
