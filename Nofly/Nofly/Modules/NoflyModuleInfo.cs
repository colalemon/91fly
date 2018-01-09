using System;
using System.Collections.Generic;
using System.Reflection;


namespace Nofly.Modules
{
    public class NoflyModuleInfo
    {
        public Assembly Assembly { get; }
        /// <summary>
        /// 模块类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 实例
        /// </summary>
        public NoflyModule Instance { get; }

        /// <summary>
        /// 依赖
        /// </summary>
        public List<NoflyModuleInfo> Dependencies { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NoflyModuleInfo(Type type,NoflyModule instance)
        {            

            Type = type;
            Instance = instance;
            Assembly = Type.Assembly;
            Dependencies = new List<NoflyModuleInfo>();
        }

    }
}
