using System;
using System.Threading.Tasks;

namespace Evento.InfraStructure.Services
{
    public interface IUserService
    {
        Task RegisterAsync(Guid userId, string email, string name, string passowrd, string role="user");
    }
}

