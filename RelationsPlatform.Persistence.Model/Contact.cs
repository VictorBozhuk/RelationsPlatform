using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class Contact
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public string Discord { get; set; }

        public virtual User User { get; set; }
        public virtual Address Address { get; set; }

        public Contact()
        {
            Id = Guid.NewGuid();
        }
    }
}
