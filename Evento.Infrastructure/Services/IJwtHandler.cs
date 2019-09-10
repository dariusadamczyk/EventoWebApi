using System;
using Evento.InfraStructure.DTO;

namespace Evento.InfraStructure.Services
{
    public interface IJwtHandler : IService
    {
        JwtDto Create(Guid userId, string role);
    }
}
