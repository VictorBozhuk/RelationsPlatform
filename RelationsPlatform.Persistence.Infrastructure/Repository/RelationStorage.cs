using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class RelationStorage : IRelationStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public RelationStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
