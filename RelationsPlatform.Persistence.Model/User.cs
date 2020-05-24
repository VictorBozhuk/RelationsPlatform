using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string DigitalName { get; set; }
        public byte[] Avatar { get; set; }
        
        public virtual ICollection<UserTask> Tasks { get; set; }
        public virtual Role Role { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual DateTime? Birthday { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }
        public virtual ICollection<Relation> MainRelations { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public User()
        {
            //Id = Guid.NewGuid();
            Relations = new HashSet<Relation>();
            MainRelations = new HashSet<Relation>();
            Feedbacks = new HashSet<Feedback>();
            Tasks = new HashSet<UserTask>();
        }
    }
}
