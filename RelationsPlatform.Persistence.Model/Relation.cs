using System;

namespace RelationsPlatform.Persistence.Model
{
    public class Relation
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Guid RelationPersonId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public virtual Person Person { get; set; }
        public virtual Person RelationPerson { get; set; }
    }
}
