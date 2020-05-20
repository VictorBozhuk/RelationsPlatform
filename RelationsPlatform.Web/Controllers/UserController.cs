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
using System.IO;
using Microsoft.AspNetCore.Http;

namespace RelationsPlatform.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserStorage _userStorage;

        public UserController(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var userViewModel = new UserViewModel()
            {
                Name = user.Name,
            };

            return View(userViewModel);
        }

        public async Task<IActionResult> EditUser()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var userViewModel = new UserViewModel()
            {
                Name = user.Name,
                Birthday = user.Birthday.HasValue == false ? string.Empty : user.Birthday.GetValueOrDefault().ToShortDateString(),
                Description = user.Description,
                Gender = user.Gender,
                Login = user.Login,
                Password = user.Password,
                DigitalName = user.DigitalName,
                Avatar = user.Avatar,
            };

            if(user.Contact != null)
            {
                userViewModel.Instagram = user.Contact.Instagram;
                userViewModel.Facebook = user.Contact.Facebook;
                userViewModel.Email = user.Contact.Email;
                userViewModel.Telegram = user.Contact.Telegram;
                userViewModel.Discord = user.Contact.Discord;

                if(user.Contact.Address != null)
                {
                    userViewModel.Country = user.Contact.Address.Country;
                    userViewModel.Region = user.Contact.Address.Region;
                    userViewModel.City = user.Contact.Address.City;
                    userViewModel.District = user.Contact.Address.District;
                    userViewModel.Street = user.Contact.Address.Street;
                    userViewModel.NumberOfHouse = user.Contact.Address.NumberOfHouse;
                }
            }

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            using (var ms = new MemoryStream())
            {
                model.File.CopyTo(ms);
                model.Avatar = ms.ToArray();
            }
            var address = new Address()
            {
                Country = model.Country,
                City = model.City,
                District = model.District,
                NumberOfHouse = model.NumberOfHouse,
                Region = model.Region,
                Street = model.Street,
            };

            var contact = new Contact()
            {
                Discord = model.Discord,
                Email = model.Email,
                Instagram = model.Instagram,
                Telegram = model.Telegram,
                Facebook = model.Facebook,
                Address = address,
            };

            var args = new UserArgs()
            {
                Avatar = model.Avatar,
                Birthday = model.Birthday,
                Description = model.Description,
                DigitalName = model.DigitalName,
                Gender = model.Gender,
                Login = model.Login,
                Name = model.Name,
                Password = model.Password,
                Contact = contact,
            };

            await _userStorage.EditUser(args);

            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Relations()
        {
            RelationsViewModel model = new RelationsViewModel()
            {
                Users = await _userStorage.GetUsers(),
            };

            return View(model);
        }

    }
}
