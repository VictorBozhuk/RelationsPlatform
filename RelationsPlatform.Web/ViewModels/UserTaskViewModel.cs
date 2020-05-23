using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class UserTaskViewModel
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }
        public List<string> Subjects { get; set; }
    }
}
