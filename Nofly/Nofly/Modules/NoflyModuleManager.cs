using Castle.Core.Logging;
using Nofly.Dependency;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace Nofly.Modules
{
    public class NoflyModuleManager : INoflyModuleManager
    {
        public NoflyModuleInfo StartupModule { get; private set; }
        private Type _startupModuleType;
        private readonly IIocManager _iocManager;
        public IReadOnlyList<NoflyModuleInfo> Modules => _modules.ToImmutableList();       
        private readonly NoflyModuleCollection _modules;
        public ILogger Logger { get; set; }

        public NoflyModuleManager(IIocManager iocManager)
        {
            _iocManager = iocManager;
            _modules = new NoflyModuleCollection();
            Logger= NullLogger.Instance;

        }

        public void Initialize(Type startupModule)
        {
            _startupModuleType = startupModule;
        }

        public void ShutdownModules()
        {
           
        }

        public void StartModules()
        {
           
        }

        private void LoadAllModules()
        {
            Logger.Debug("Loading Abp modules...");

            var moduleTypes = FindAllModules().Distinct().ToList();

            Logger.Debug("Found " + moduleTypes.Count + " ABP modules in total.");

            RegisterModules(moduleTypes);
            //CreateModules(moduleTypes);

            //AbpModuleCollection.EnsureKernelModuleToBeFirst(_modules);

            //SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private List<Type> FindAllModules()
        {
            var modules = NoflyModule.FillAllModules(_startupModuleType);   

            return modules;
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }
      
    }
}
