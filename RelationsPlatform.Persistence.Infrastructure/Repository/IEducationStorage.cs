using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IEducationStorage
    {
        Task<Education> GetEducation(string userid);
        Task Create(string userId);
    }

    public class EducationArgs
    {

    }
}
