using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class Education
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<HigherEducation> HigherEducations { get; set; }
        public virtual ICollection<School> Schools { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

        public Education()
        {
            HigherEducations = new HashSet<HigherEducation>();
            Schools = new HashSet<School>();
            Courses = new HashSet<Course>();
            Id = Guid.NewGuid();
        }
    }
}
