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

        public async Task<Role> GetRole(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
