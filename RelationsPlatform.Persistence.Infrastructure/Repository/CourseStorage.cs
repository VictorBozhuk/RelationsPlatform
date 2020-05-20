using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class CourseStorage : ICourseStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public CourseStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddCourse(CourseArgs args)
        {
            var course = new Course()
            {
                Name = args.Name,
                Description = args.Description,
                EducationId = args.EducationId,
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(string id)
        {
            var course = await GetCourse(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task<Course> GetCourse(string id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
