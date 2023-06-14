using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public class StaticParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        static PropertyInfo[] properties;
        static ParametrInfo[] descriptions;

        static StaticParametersHandler()
        {
            properties = typeof(TParameters)
                .GetProperties()
                .Where(p => p.GetCustomAttributes<ParametrInfo>().Count() > 0)
                .ToArray();

            descriptions = typeof(TParameters)
                .GetProperties()
                .Select(p => p.GetCustomAttributes<ParametrInfo>())
                .Where(a => a.Count() > 0)
                .SelectMany(a => a)
                .Cast<ParametrInfo>()
                .ToArray();
        }

        public TParameters CreateParameters(double[] values)
        {
            var parameters = new TParameters();

            if (properties.Length != values.Length)
                throw new ArgumentException();

            for (var i = 0; i < properties.Length; i++)
                properties[i].SetValue(parameters, values[i]);

            return parameters;
        }
        public ParametrInfo[] GetDescriptions() => descriptions;
    }
}
