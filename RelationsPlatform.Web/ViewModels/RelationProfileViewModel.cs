using Microsoft.AspNetCore.Http;
using RelationsPlatform.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationsPlatform.Web.ViewModels
{
    public class RelationProfileViewModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public int Note { get; set; }
        public string Raiting { get; set; }
        public User User { get; set; }
    }
}
