using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public interface IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        ParametrInfo[] GetDescriptions();
        TParameters CreateParameters(double[] values);
    }
}
