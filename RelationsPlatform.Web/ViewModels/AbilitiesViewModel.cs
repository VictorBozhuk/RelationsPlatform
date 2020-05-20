using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class AbilitiesViewModel
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public List<KeyValuePair<int, string>> Levels { get; set; }
        public List<Ability> Abilities { get; set; }
    }
}
