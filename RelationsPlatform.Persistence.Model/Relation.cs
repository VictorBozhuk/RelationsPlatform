using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationsPlatform.Persistence.Model
{
    public class Relation
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid RelationUserId { get; set; }
        public string Status { get; set; }
        public virtual User User { get; set; }
        public virtual User RelationUser { get; set; }

        public Relation()
        {
            Id = Guid.NewGuid();
        }
    }
}
