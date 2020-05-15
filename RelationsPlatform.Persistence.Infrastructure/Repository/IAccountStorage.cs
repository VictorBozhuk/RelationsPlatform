using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IAccountStorage
    {
        Task<User> GetUser(string login);
        Task<User> GetUser(string login, string password);
        Task<Role> GetRole(string name);
        Task CreateUser(User user);
    }
}
