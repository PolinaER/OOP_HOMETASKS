using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class UpperParameters : IParameters
    {
        [ParametrInfo(Name = "Процент сдвига",
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 0,
                    Increment = 5)]
        public double CoeffUp { get; set; }
    }
}
