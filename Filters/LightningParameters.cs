using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class LightningParameters : IParameters
    {
        public double Coefficient { get; set; }
        public ParametrInfo[] GetDiscription()
        {
            return new[]
           {
                new ParametrInfo()
                {
                    Name="Коэффициент",
                    MinValue = 0,
                    MaxValue = 10,
                    DefaultValue = 1,
                    Increment = 0.05
                }
            };
        }

        public void SetValues(double[] values)
        {
            Coefficient = values[0];
        }
    }
}
