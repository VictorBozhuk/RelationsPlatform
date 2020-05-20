using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface IJobStorage
    {
    }

    public class JobArgs
    {
        public string NamePosition { get; set; }
        public string NameOfCompany { get; set; }
        public string DescriptionOfWork { get; set; }
    }
}
