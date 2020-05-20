using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class SchoolsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<School> Schools { get; set; }
    }
}
