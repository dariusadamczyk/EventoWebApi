using System;
using System.Collections.Generic;

namespace Evento.Core.Domain
{
    public class User : Entity
    {
        private static List<string> _roles = new List<String>
        {
            "admin", "user"
        };
        public string Role { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }

        public DateTime CreatedAd { get; set; }

        protected User()
        {
            
        }

        public User(Guid id, string role, string name,string email, string password)
        {
            Id=id;
            SetRole(role);
            SetName(name);
            SetEmail(email);
            SetPassword(password);
            CreatedAd = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new Exception($"User with id '{Id}' can not have emtpy name ");
            }

            Name = name;
           
        }

        public void SetEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                throw new Exception($"User with id '{Id}' can not have emtpy email ");
            }

            Email = email;

        }

        public void SetRole(string role)
        {
            if (String.IsNullOrWhiteSpace(role))
            {
                throw new Exception($"User with id '{Id}' can not have emtpy role ");
            }

            role = role.ToLowerInvariant();
            if (!_roles.Contains(role))
            {
                throw new Exception($"User can not have role '{role}'");
            }
            Role = role;

        }

        public void SetPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"User with id '{Id}' can not have emtpy email ");
            }

            Password = password;

        }
    }
}