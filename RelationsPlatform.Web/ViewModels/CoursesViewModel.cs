using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class CoursesViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Course> Courses { get; set; }
    }
}
