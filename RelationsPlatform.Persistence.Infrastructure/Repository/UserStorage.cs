using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class UserStorage : IUserStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public UserStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string login)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.Login == login);
        }
    }
}
