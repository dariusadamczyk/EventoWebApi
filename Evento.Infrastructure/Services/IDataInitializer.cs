using System;
using System.Threading.Tasks;

namespace Evento.InfraStructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}
