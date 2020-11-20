﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generators
{
    public interface IBaseGenerator
    {
        Type GeneratingType { get; }
        object Generate();
    }
}
