using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class EmpryParameters : IParameters
    {
        public ParametrInfo[] GetDiscription() => new ParametrInfo[0];

        public void SetValues(double[] values) {}
    }
}
