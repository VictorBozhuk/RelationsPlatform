using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IHigherEducationStorage
    {
        Task AddHigherEducation(HigherEducationArgs args);
        Task DeleteHigherEducation(string id);
        Task<HigherEducation> GetHigherEducation(string id);
    }

    public class HigherEducationArgs
    {
        public Guid EducationId { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string Status { get; set; }
    }
}
