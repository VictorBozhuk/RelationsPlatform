using RelationsPlatform.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class AccountStorage : IAccountStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public AccountStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(string login, string password)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }

        public async Task<User> GetUser(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task<Role> GetRole(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
