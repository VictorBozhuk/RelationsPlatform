using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class ProfessionSkillStorage : IProfessionSkillStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public ProfessionSkillStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddProfessionSkill(ProfessionSkillArgs args)
        {
            var skill = new ProfessionalSkill()
            {
                Name = args.Name,
                Level = args.Level,
                Description = args.description,
                SkillId = args.SkillId,
            };
            await _context.ProfessionSkills.AddAsync(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfessionSkill(string id)
        {
            var profSkill = await GetProfessionSkill(id);
            _context.ProfessionSkills.Remove(profSkill);
            await _context.SaveChangesAsync();
        }

        public async Task<ProfessionalSkill> GetProfessionSkill(string id)
        {
            return await _context.ProfessionSkills.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
