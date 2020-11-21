using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    class Date_Time_Generator : IBaseGenerator
    {
        public Type GeneratingType
        {
            get { return typeof(DateTime); }
        }
        public object Generate()
        {
            return DateTime.Now;
        }
    }
}
