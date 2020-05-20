using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IProfessionSkillStorage
    {
        Task AddProfessionSkill(ProfessionSkillArgs args);
        Task DeleteProfessionSkill(string id);
        Task<ProfessionalSkill> GetProfessionSkill(string id);
    }

    public class ProfessionSkillArgs
    {
        public string Name { get; set; }
        public string description { get; set; }
        public int Level { get; set; }
        public Guid SkillId { get; set; }
    }

}
