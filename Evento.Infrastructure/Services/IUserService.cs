using System;
using System.Threading.Tasks;
using Evento.InfraStructure.DTO;

namespace Evento.InfraStructure.Services
{
    public interface IUserService : IService
    {
        Task RegisterAsync(Guid userId, string email, string name, string passowrd, string role="user");
        Task<TokenDto> LoginAsync(string email, string passowrd);
        Task<AccountDto> GetAccountAsync(Guid userId);
    }
}

