using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Model
{
    public class Feedback
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public int Note { get; set; }
        public string SenderId { get; set; }
        public virtual User User { get; set; }
    }
}
