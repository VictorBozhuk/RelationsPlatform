using Microsoft.AspNetCore.Http;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        public string DigitalName { get; set; }
        public string Birthday { get; set; }
        public IFormFile File { get; set; }
        public byte[] Avatar { get; set; }

        public string Telegram { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Email { get; set; }
        public string Discord { get; set; }

        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string NumberOfHouse { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Education Education { get; set; }
        public virtual ICollection<Relation> Relations { get; set; }
    }
}
