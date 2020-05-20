using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class ProfessionSkillsViewModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public List<KeyValuePair<int, string>> Levels { get; set; }
        public List<ProfessionalSkill> Skills { get; set; }
    }
}
