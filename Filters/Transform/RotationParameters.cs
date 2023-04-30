using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class RotationParameters : IParameters
    {
        public double AngleInDegrees { get; set; }
        public ParametrInfo[] GetDiscription()
        {
            return new[]
           {
                new ParametrInfo()
                {
                    Name="Угол в градусах",
                    MinValue = -360,
                    MaxValue = 360,
                    DefaultValue = 0, 
                    Increment = 5
                }
            };
        }

        public void SetValues(double[] values)
        {
            AngleInDegrees = values[0];
        }
    }
}
