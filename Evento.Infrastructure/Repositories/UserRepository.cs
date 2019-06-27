using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.InfraStructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private static readonly ISet<User> _user = new HashSet<User>();

        public async Task<User> GetAsync(Guid id)
         => await Task.FromResult(_user.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
        => await Task.FromResult(_user.SingleOrDefault(x => x.Email == email));

        public async Task AddAsync(User user)
        {
            _user.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            _user.Add(user);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            _user.Remove(user);
            await Task.CompletedTask;
        }

        
    }
}
