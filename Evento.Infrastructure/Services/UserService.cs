using System;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.InfraStructure.DTO;

namespace Evento.InfraStructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private  IJwtHandler _jwtHandler { get; }

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

       

        public async Task<TokenDto> LoginAsync(string email, string passowrd)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"Invalid credentials.");
            }
            if (user.Password != passowrd)
            {
                throw new Exception($"Invalid credentials.");
            }

            var jwt = _jwtHandler.Create(user.Id, user.Role);
            return new TokenDto {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role= user.Role
            };

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
