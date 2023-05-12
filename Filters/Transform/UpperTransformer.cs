using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class UpperTransformer : ITransformer<UpperParameters>
    {
        double coeff { get; set; }
        public Size ResultSize { get; private set; }

        public void Initialize(Size size, UpperParameters parameters)
        {
            ResultSize = size;
            coeff = (int)(parameters.CoeffUp / 100 * (size.Height - 1));
        }

        public Point? MapPoint(Point newPoint)
        {
            if (newPoint.Y + coeff <= ResultSize.Height - 1)
            {
                var c = new Point(newPoint.X, (int)(newPoint.Y + coeff));
                return c;
            }
            else
            {
                var c = new Point(newPoint.X, (int)(newPoint.Y + coeff - ResultSize.Height));
                return c;
            }
        }
    }
}
