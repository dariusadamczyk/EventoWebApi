using System;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.InfraStructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(Guid userId, string email, string name, string passowrd, string role = "user")
        {
            var user = await _userRepository.GetAsync(email);
            if (user!=null)
            {
                throw new Exception($"User with {email} already exist.");
            }

            user = new User(userId, role, name, email, passowrd);
            await _userRepository.AddAsync(user);
        }
    }
}
