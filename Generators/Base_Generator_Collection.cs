using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    class Base_Generator_Collection: IGenerator
    {
        protected List<IBaseGenerator> generators;


        public Base_Generator_Collection()
        {
            generators = new List<IBaseGenerator>();
        }

        public void AddGenerator(IBaseGenerator generator)
        {
            generators.Add(generator);
        }

        public virtual object Generate(Type type)
        {
            foreach (var generator in generators)
            {
                if (generator.GeneratingType == type)
                    return generator.Generate();
            }

            return null;
        }
    }

}