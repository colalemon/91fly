using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nofly.Dependency
{
    public interface IIocResolver
    {
        T Resolve<T>();
        T Resolve<T>(Type type);
    }
}
