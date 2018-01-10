using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Nofly.Dependency;
using Nofly.Dependency.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nofly.Modules
{
    /// <summary>
    /// Nofly驱动引导类
    /// </summary>
    public class NoflyBootstrapper : IDisposable
    {

        /// <summary>
        /// 开始模块
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// 依赖注入容器
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// 日志
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// 模块管理者
        /// </summary>

        private NoflyModuleManager _moduleManager;

        #region  构造函数

        private NoflyBootstrapper(Type startupModule)
            : this(startupModule, Dependency.IocManager.Instance)
        {

        }

        private NoflyBootstrapper(Type startupModule, IIocManager iocManager)
        {
            if (!typeof(NoflyModule).IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(NoflyModule)}.");
            }
            StartupModule = startupModule;
            IocManager = iocManager;
            _logger = NullLogger.Instance;
        }

        #endregion

        public static NoflyBootstrapper Create<TStartupModule>()
           where TStartupModule : NoflyModule
        {
            return new NoflyBootstrapper(typeof(TStartupModule));
        }

        /// <summary>
        /// 初始化系统
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();
            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new NoflyCoreInstaller());


                _moduleManager = IocManager.Resolve<NoflyModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString(), ex);
                throw;
            }

        }

        /// <summary>
        /// 注册引导者
        /// </summary>
        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<NoflyBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<NoflyBootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// 注册日志
        /// </summary>
        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(NoflyBootstrapper));
            }
        }

        public void Dispose()
        {

        }
    }
}
