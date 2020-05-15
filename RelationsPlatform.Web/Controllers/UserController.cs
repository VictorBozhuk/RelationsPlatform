using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using RelationsPlatform.Persistence.Infrastructure;
using RelationsPlatform.Persistence.Infrastructure.Repository;
using RelationsPlatform.Persistence.Model;
using RelationsPlatform.Web.ViewModels;

namespace RelationsPlatform.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserStorage _userStorage;

        public UserController(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var userViewModel = new UserViewModel()
            {
                Name = user.Name,
            };

            return View(userViewModel);
        }
    }
}
