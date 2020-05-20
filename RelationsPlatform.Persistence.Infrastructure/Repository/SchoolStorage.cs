using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class SchoolStorage : ISchoolStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public SchoolStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
