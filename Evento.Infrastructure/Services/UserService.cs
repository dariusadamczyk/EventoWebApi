using System;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.InfraStructure.DTO;
using Evento.InfraStructure.Extensions;

namespace Evento.InfraStructure.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private  IJwtHandler _jwtHandler { get; }

        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return _mapper.Map<AccountDto>(user);
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
