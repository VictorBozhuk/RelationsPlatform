using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IProfessionSkillStorage
    {
    }

    public class ProfessionSkillArgs
    {
        public string Name { get; set; }
        public string description { get; set; }
        public int Level { get; set; }
        public Guid SkillId { get; set; }
    }

}
