using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Infrastructure.Repository
{
    public interface  IAbilityStorage
    {
    }

    public class AbilitiArgs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }
}
