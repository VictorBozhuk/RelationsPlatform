using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class EducationStorage : IEducationStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public EducationStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task<Education> GetEducation(string userid)
        {
            return await _context.Educations.FirstOrDefaultAsync(x => x.UserId.ToString() == userid);
        }
    }
}
