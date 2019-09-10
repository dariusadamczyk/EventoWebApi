using System;
using Autofac;
using Evento.InfraStructure.Extensions;
using Evento.InfraStructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Evento.InfraStructure.IoC.Modules
{
    public class SettingsModule: Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<AppSettings>())
                .SingleInstance();
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                .SingleInstance();
            
        }
    }
}
