using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nofly.Modules
{
    public class DependsOnAttribute : Attribute
    {
        public Type[] DependedModuleTypes { get; private set; }
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
