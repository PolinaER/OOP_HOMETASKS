using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class GammaCorrectionParameters : IParameters
    {
        public double Gamma { get; set; }
        public ParametrInfo[] GetDiscription()
        {
            return new[]
           {
                new ParametrInfo()
                {
                    Name="Гамма",
                    MinValue = 0.2,
                    MaxValue = 5,
                    DefaultValue = 1,
                    Increment = 0.01
                }
            };
        }

        public void SetValues(double[] values)
        {
            Gamma = values[0];
        }
    }
}
