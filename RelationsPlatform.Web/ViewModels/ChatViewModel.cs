using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class ChatViewModel
    {
        public string IdGetter { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Message Getters { get; set; }
    }
}
