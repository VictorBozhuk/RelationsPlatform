using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> People { get; set; }

        public Role()
        {
            People = new HashSet<User>();
        }
    }
}
