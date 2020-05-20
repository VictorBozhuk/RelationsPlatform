using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public class CourseStorage : ICourseStorage
    {
        private RelationsPlatformDataBaseContext _context;

        public CourseStorage(RelationsPlatformDataBaseContext context)
        {
            _context = context;
        }
    }
}
