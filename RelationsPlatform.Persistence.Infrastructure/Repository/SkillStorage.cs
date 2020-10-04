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

        public async Task Create(string userId)
        {
            _context.Skills.Add(new Skill() { UserId = Guid.Parse(userId) });
            await _context.SaveChangesAsync();
        }

        public async Task<Skill> GetSkill(string userId)
        {
            return await _context.Skills.FirstOrDefaultAsync(x => x.UserId.ToString() == userId);
        }

        public async Task EditSkill(string userId, string mainSkill)
        {
            var skill = await GetSkill(userId);
            skill.MainProfession = mainSkill;
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
        }
    }
}
