using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class SimpleParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        public TParameters CreateParameters(double[] values)
        {
            var parameters = new TParameters();

            var properties = parameters
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes<ParametrInfo>().Count() > 0)
                .ToArray();

            if (properties.Length != values.Length)
                throw new ArgumentException();

            for (var i = 0; i < properties.Length; i++)
                properties[i].SetValue(parameters, values[i]);

            return parameters;
        }
        public ParametrInfo[] GetDescriptions() =>
            typeof(TParameters)
                .GetProperties()
                .Select(p => p.GetCustomAttributes<ParametrInfo>())
                .Where(a => a.Count() > 0)
                .SelectMany(a => a)
                .Cast<ParametrInfo>()
                .ToArray();
    }
}
