using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IJobStorage
    {
        Task AddJob(JobArgs args);
        Task DeleteJob(string id);
        Task<Job> GetJob(string id);
    }

    public class JobArgs
    {
        public string NamePosition { get; set; }
        public string NameOfCompany { get; set; }
        public string DescriptionOfWork { get; set; }
        public Guid SkillId { get; set; }
    }
}
