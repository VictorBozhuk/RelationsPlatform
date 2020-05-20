using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class EducationStorage : IEducationStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public EducationStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
