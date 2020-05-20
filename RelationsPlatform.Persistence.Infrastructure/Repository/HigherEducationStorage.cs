using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class HigherEducationStorage : IHigherEducationStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public HigherEducationStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddHigherEducation(HigherEducationArgs args)
        {
            var higher = new HigherEducation()
            {
                University = args.University,
                Faculty = args.Faculty,
                Specialty = args.Specialty,
                Status = args.Status,
                EducationId = args.EducationId,
            };
            await _context.HigherEducation.AddAsync(higher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHigherEducation(string id)
        {
            var higher = await GetHigherEducation(id);
            _context.HigherEducation.Remove(higher);
            await _context.SaveChangesAsync();
        }

        public async Task<HigherEducation> GetHigherEducation(string id)
        {
            return await _context.HigherEducation.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
