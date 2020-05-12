using System;
using System.Collections.Generic;

namespace RelationsPlatform.Persistence.Model
{
    public class Contacts
    {
        public Guid Id { get; set; }
        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public string Discord { get; set; }

        public virtual Person Person { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<string> NumberPhones { get; set; }

        public Contacts()
        {
            NumberPhones = new HashSet<string>();
        }
    }
}
