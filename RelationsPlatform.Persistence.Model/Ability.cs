using System;

namespace RelationsPlatform.Persistence.Model
{
    public class Ability
    {
        public Guid Id { get; set; }
        public Guid SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
