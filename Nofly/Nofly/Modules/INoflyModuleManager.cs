using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nofly.Modules
{
    /// <summary>
    /// 模块管理者
    /// </summary>
    public interface INoflyModuleManager
    {
        NoflyModuleInfo StartupModule { get; }

        IReadOnlyList<NoflyModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}
