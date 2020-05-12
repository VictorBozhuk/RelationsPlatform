using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class Skill
    {
        public Guid Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<ProfessionalSkill> ProfesionSkills { get; set; }
        public virtual ICollection<Ability> Abilities { get; set; }

        public Skill()
        {
            Jobs = new HashSet<Job>();
            ProfesionSkills = new HashSet<ProfessionalSkill>();
            Abilities = new HashSet<Ability>();
        }
    }
}
