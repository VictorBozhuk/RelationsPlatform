using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class AbilityStorage : IAbilityStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public AbilityStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddAbility(AbilityArgs args)
        {
            var ability = new Ability()
            {
                Name = args.Name,
                Level = args.Level,
                Description = args.Description,
                SkillId = args.SkillId,
            };
            await _context.Abilities.AddAsync(ability);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAbility(string id)
        {
            var ability = await GetAbility(id);
            _context.Abilities.Remove(ability);
            await _context.SaveChangesAsync();
        }

        public async Task<Ability> GetAbility(string id)
        {
            return await _context.Abilities.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
