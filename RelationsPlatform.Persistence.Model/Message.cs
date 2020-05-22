using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Model
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid RelationUserId { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public DateTime Time { get; set; }
        public virtual User User { get; set; }
        public virtual User RelationUser { get; set; }

        public Message()
        {
            Id = Guid.NewGuid();
        }
    }
}
