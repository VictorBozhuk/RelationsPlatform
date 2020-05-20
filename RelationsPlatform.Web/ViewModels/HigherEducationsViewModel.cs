using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class HigherEducationsViewModel
    {
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Speciality { get; set; }
        public string Status { get; set; }
        public List<HigherEducation> higherEducations { get; set; }
    }
}
