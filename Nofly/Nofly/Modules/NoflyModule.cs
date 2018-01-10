using Castle.Core.Logging;
using Nofly.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Nofly.Modules
{
    /// <summary>
    /// 模块抽象基类
    /// </summary>
    public abstract class NoflyModule
    {
        /// <summary>
        /// IocManager
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// 日志助手
        /// </summary>
        public ILogger Logger { get; set; }

        protected NoflyModule()
        {
            Logger = NullLogger.Instance;
        }

        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 初始化（用来注册相关文件）
        /// </summary>
        public virtual void Initialize()
        {

        }
        public virtual void PostInitialize()
        {

        }

        public virtual void Shutdown()
        {

        }

        /// <summary>
        ///查找所有的的模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FillAllModules(Type moduleType)
        {
            var list = new List<Type>();
            WSModules(list, moduleType);
            return list;
        }

        /// <summary>
        /// 当前模块的的依赖项
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependencyModules(Type moduleType)
        {
            List<Type> list = new List<Type>();
            if (moduleType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 广搜查找算法
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="module"></param>
        public static void WSModules(List<Type> modules, Type module)
        {
            if (modules.Contains(module)) { return; }
            List<Type> deModules = FindDependencyModules(module);
            foreach (var deModule in deModules)
            {
                WSModules(modules, deModule);
            }
        }


    }
}
