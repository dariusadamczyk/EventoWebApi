using Autofac;
using Microsoft.Extensions.Configuration;
using Evento.InfraStructure.Mappers;
using Evento.InfraStructure.IoC.Modules;

namespace Evento.InfraStructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
            builder.RegisterModule(new SettingsModule(_configuration));
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
        }
     }
}
