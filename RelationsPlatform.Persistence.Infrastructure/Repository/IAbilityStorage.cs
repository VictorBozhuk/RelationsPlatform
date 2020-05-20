using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface  IAbilityStorage
    {
        Task AddAbility(AbilityArgs args);
        Task DeleteAbility(string id);
        Task<Ability> GetAbility(string id);
    }

    public class AbilityArgs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public Guid SkillId { get; set; }
    }
}
