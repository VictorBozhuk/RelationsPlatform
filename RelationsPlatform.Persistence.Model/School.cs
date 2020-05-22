using System;

namespace RelationsPlatform.Persistence.Model
{
    public class School
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Education Education { get; set; }

        public School()
        {
            Id = Guid.NewGuid();
        }
    }
}
