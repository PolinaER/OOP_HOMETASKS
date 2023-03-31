using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public interface IFilter
    {
        Photo Process(Photo original, double[] parametrs);
        ParametrInfo[] GetParametrsInfo();

    }
}
