using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface ISchoolStorage
    {
        Task AddSchool(SchoolArgs args);
        Task DeleteSchool(string id);
        Task<School> GetSchool(string id);
    }

    public class SchoolArgs
    {
        public Guid EducationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
