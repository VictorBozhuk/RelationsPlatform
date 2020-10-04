using Microsoft.EntityFrameworkCore;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface ISkillStorage
    {
        Task<Skill> GetSkill(string userId);
        Task EditSkill(string userId, string mainSkill);
        Task Create(string userId);
    }

    public class SkillArgs
    {

    }

}
