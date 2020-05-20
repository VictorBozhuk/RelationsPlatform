using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class JobsViewModel
    {
        public string NameOfPosition { get; set; }
        public string NameOfCompany { get; set; }
        public string DescriptionOfWork { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
