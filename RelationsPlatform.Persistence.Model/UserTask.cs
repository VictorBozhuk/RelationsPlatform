using System;
using System.Collections.Generic;
using System.Text;

namespace RelationsPlatform.Persistence.Model
{
    public class UserTask
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }

        public UserTask()
        {
            //Id = Guid.NewGuid();
        }
    }
}
