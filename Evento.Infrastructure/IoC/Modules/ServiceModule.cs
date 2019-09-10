using System;
using System.Reflection;
using Autofac;
using Evento.InfraStructure.Services;
namespace Evento.InfraStructure.IoC.Modules
{
        public class ServiceModule : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                var assembly = typeof(ServiceModule)
                    .GetTypeInfo()
                    .Assembly;

                builder.RegisterAssemblyTypes(assembly)
                    .Where(x => x.IsAssignableTo<IService>())
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            }
        }
    
}
