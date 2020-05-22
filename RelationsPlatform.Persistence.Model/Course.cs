using System;

namespace RelationsPlatform.Persistence.Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Education Education { get; set; }

        public Course()
        {
            Id = Guid.NewGuid();
        }
    }
}
