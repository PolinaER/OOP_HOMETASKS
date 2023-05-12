using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class RotateTransformer : ITransformer<RotationParameters>
    {
        Size oldSize { get; set; }
        double angleInRadians { get; set; }
        public Size ResultSize { get; private set; }

        public void Initialize(Size size, RotationParameters parameters)
        {
            oldSize = size;
            angleInRadians = parameters.AngleInDegrees * Math.PI / 180;
            ResultSize = new Size((int)(oldSize.Width * Math.Abs(Math.Cos(angleInRadians)) + oldSize.Height * Math.Abs(Math.Sin(angleInRadians))),
                     (int)(oldSize.Width * Math.Abs(Math.Sin(angleInRadians)) + oldSize.Height * Math.Abs(Math.Cos(angleInRadians))));
        }

        public Point? MapPoint(Point newPoint)
        {
            newPoint = new Point(newPoint.X - ResultSize.Width / 2, newPoint.Y - ResultSize.Height / 2);
            var x = (int)(newPoint.X * Math.Cos(angleInRadians) - newPoint.Y * Math.Sin(angleInRadians) + oldSize.Width / 2);
            var y = (int)(newPoint.X * Math.Sin(angleInRadians) + newPoint.Y * Math.Cos(angleInRadians) + oldSize.Height / 2);

            if (x < 0 || x >= oldSize.Width || y < 0 || y >= oldSize.Height)
                return null;

            return new Point(x, y);
        }
    }
}
