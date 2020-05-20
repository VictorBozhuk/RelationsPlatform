using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class HigherEducationStorage : IHigherEducationStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public HigherEducationStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
