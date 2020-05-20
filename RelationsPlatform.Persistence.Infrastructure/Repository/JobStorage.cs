using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class JobStorage : IJobStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public JobStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }

        public async Task AddJob(JobArgs args)
        {
            var job = new Job()
            {
                DescriptionOfWork = args.DescriptionOfWork,
                NameCompany = args.NameOfCompany,
                NamePosition = args.NamePosition,
                SkillId = args.SkillId,
            };
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJob(string id)
        {
            var job = await GetJob(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }

        public async Task<Job> GetJob(string id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
