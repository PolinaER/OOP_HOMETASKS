using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public interface IParameters
    {
        ParametrInfo[] GetDiscription();

        void SetValues(double[] values);
    }
}
