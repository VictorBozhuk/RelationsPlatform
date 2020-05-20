using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class SchoolStorage : ISchoolStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public SchoolStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddSchool(SchoolArgs args)
        {
            var school = new School()
            {
                Name = args.Name,
                Description = args.Description,
                EducationId = args.EducationId,
            };
            await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSchool(string id)
        {
            var school = await GetSchool(id);
            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();
        }

        public async Task<School> GetSchool(string id)
        {
            return await _context.Schools.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
