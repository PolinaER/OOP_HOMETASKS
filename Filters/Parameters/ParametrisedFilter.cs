using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoRed
{
    public abstract class ParametrisedFilter<TParameters> : IFilter
        where TParameters : IParameters, new() 
    {
        protected string name;

        IParametersHandler<TParameters> handler = new StaticParametersHandler<TParameters>();
        
        public ParametrInfo[] GetParametrsInfo() => handler.GetDescriptions();

        public Photo Process(Photo original, double[] values)
        {
            var parameters = handler.CreateParameters(values);
            return Process(original, parameters);
        }

        public abstract Photo Process(Photo original, TParameters parameters);

        public override string ToString()
        {
            return name;
        }
    }
}
