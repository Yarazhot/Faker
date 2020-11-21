using System;
using Generators;

namespace Base_Generators
{
    public abstract class Base_Generator : IBaseGenerator
    {
        protected Random random = new Random();
        public abstract Type GeneratingType { get; }

        public abstract object Generate();
    }

    public class Int_Generator : Base_Generator
    {
        public override Type GeneratingType
        {
            get { return typeof(int); }
        }

        public override object Generate()
        {
            return random.Next();
        }
    }

    public class LongGenerator : Base_Generator
    {
        public override Type GeneratingType
        {
            get { return typeof(long); }
        }

        public override object Generate()
        {
            return ((long)random.Next()) * random.Next();
        }
    }

    public class DoubleGenerator : Base_Generator
    {
        public override Type GeneratingType
        {
            get { return typeof(double); }
        }

        public override object Generate()
        {
            return random.NextDouble();
        }
    }

    public class FloatGenerator : Base_Generator
    {
        public override Type GeneratingType
        {
            get { return typeof(float); }
        }

        public override object Generate()
        {
            return (float)random.NextDouble();
        }
    }
}
