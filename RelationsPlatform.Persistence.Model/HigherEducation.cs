using System;

namespace RelationsPlatform.Persistence.Model
{
    public class HigherEducation
    {
        public Guid Id { get; set; }
        public Guid EducationId { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Status { get; set; }
        public virtual Education Education { get; set; }

        public HigherEducation()
        {
            Id = Guid.NewGuid();
        }
    }
}
