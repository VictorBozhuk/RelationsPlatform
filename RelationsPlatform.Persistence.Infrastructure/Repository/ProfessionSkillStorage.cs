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
    }
}
