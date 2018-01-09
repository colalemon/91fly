using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nofly.Dependency
{
    public interface IIocRegistrar
    {
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
          where T : class;

        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        /// <summary>
        /// 是否注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        bool IsRegistered<T>();
    }
}
