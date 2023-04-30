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
        public ParametrInfo[] GetParametrsInfo() => (new TParameters()).GetDiscription();

        public Photo Process(Photo original, double[] values)
        {
            var parameters = new TParameters();
            parameters.SetValues(values);
            return Process(original, parameters);
        }

        public abstract Photo Process(Photo original, TParameters parameters);

        public override string ToString()
        {
            return name;
        }
    }
}
