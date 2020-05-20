using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class JobStorage : IJobStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public JobStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
