using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nofly.Dependency
{
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,

        /// <summary>
        /// 临时
        /// </summary>
        Transient
    }
}
