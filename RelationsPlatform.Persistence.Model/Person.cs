using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public string DigitalName { get; set; }
        public byte[] Avatar { get; set; }

        public virtual DateTime? UpdateTime { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual DateTime? Birthday { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }

        public Person()
        {
            Relations = new HashSet<Relation>();
        }
    }
}
