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
        public string Sex { get; set; }
        public string Description { get; set; }
        public string DigitalName { get; set; }
        public byte[] Avatar { get; set; }

        public virtual DateTime? UpdateTime { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual DateTime? Birthday { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }
    }
}
