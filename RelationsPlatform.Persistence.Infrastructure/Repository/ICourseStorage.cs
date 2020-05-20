using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface ICourseStorage
    {
        Task AddCourse(CourseArgs args);
        Task DeleteCourse(string id);
        Task<Course> GetCourse(string id);
    }

    public class CourseArgs
    {
        public Guid EducationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
