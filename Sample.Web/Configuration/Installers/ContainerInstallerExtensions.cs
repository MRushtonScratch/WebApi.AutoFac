using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Sample.WebApi.Application;
using Sample.WebApi.Configuration.StartupTasks;
using System.Linq;
using System.Reflection;

namespace Sample.WebApi.Configuration.Installers
{
    public static class ContainerInstallerExtensions
    {
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterStartupTasks(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            return builder.RegisterAssemblyTypes(assemblies).Where(t => t.GetInterfaces().Contains(typeof(IStartupTask))).AsImplementedInterfaces();
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> RegisterRepositories(this ContainerBuilder builder, params Assembly[] controllerAssemblies)
        {
            return builder.RegisterAssemblyTypes(controllerAssemblies)
                .Where(t => typeof(IRepository).IsAssignableFrom(t)).AsImplementedInterfaces();
        }
    }
}