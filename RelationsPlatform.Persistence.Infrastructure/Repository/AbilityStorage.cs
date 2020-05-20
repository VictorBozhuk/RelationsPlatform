using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class AbilityStorage : IAbilityStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public AbilityStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
