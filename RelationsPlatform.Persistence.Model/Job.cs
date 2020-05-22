using System;

namespace RelationsPlatform.Persistence.Model
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid SkillId { get; set; }
        public string NamePosition { get; set; }
        public string NameCompany { get; set; }
        public string DescriptionOfWork { get; set; }
        public virtual Skill Skill { get; set; }

        public Job()
        {
            Id = Guid.NewGuid();
        }
    }
}
