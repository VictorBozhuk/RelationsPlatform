using Microsoft.AspNetCore.Http;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class ProfileViewModel
    {
        public string Raiting { get; set; }
        public User User { get; set; }
    }
}
