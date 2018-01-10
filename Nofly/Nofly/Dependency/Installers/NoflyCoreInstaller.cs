using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Nofly.Modules;

namespace Nofly.Dependency.Installers
{
    public class NoflyCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                 Component.For<INoflyModuleManager, NoflyModuleManager>().ImplementedBy<NoflyModuleManager>().LifestyleSingleton()
                );
        }
    }
}
