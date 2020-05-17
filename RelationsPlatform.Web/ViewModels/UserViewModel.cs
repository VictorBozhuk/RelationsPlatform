using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string DigitalName { get; set; }
        public byte[] Avatar { get; set; }

        public string UpdateTime { get; set; }
        public virtual Contact Contact { get; set; }
        public string Birthday { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }
    }
}
