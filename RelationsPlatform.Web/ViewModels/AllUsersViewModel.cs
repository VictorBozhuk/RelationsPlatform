using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class AllUsersViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string ProfrssionSkill { get; set; }
        public string Job { get; set; }
        public string Ability { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public List<User> Users { get; set; }
    }
}
