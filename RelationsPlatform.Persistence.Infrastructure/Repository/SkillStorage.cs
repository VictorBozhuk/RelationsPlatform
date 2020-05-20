using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class SkillStorage : ISkillStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public SkillStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Skill> GetSkill(string userId)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.UserId.ToString() == userId);
        }


    }
}
