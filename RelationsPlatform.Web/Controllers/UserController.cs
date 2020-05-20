using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using RelationsPlatform.Persistence.Infrastructure;
using RelationsPlatform.Persistence.Infrastructure.Models;
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
        private readonly IAbilityStorage _abilityStorage;
        private readonly IProfessionSkillStorage _profSkillStorage;
        private readonly IJobStorage _jobStorage;
        private readonly IEducationStorage _educationStorage;
        private readonly ISchoolStorage _schoolStorage;
        private readonly ICourseStorage _courseStorage;
        private readonly IHigherEducationStorage _higherEducationStorage;
        private readonly IRelationStorage _relationStorage;
        private readonly ISkillStorage _skillStorage;

        public UserController(IUserStorage userStorage, IAbilityStorage abilityStorage, IProfessionSkillStorage profSkillStorage, 
            IJobStorage jobStorage, IEducationStorage educationStorage, ISchoolStorage schoolStorage, ICourseStorage courseStorage, 
            IHigherEducationStorage higherEducationStorage, IRelationStorage relationStorage, ISkillStorage skillStorage)
        {
            _userStorage = userStorage;
            _abilityStorage = abilityStorage;
            _profSkillStorage = profSkillStorage;
            _jobStorage = jobStorage;
            _educationStorage = educationStorage;
            _schoolStorage = schoolStorage;
            _courseStorage = courseStorage;
            _higherEducationStorage = higherEducationStorage;
            _relationStorage = relationStorage;
            _skillStorage = skillStorage;
        }

        public async Task<IActionResult> Abilities()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var abilities = new AbilitiesViewModel()
            {
                Levels = Level.Levels,
                Abilities = user.Skill.Abilities.ToList(),
            };

            return View(abilities);
        }

        public async Task<IActionResult> AddAbility(AbilitiesViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var skill = await _skillStorage.GetSkill(user.Id.ToString());
            var ability = new AbilityArgs()
            {
                Name = model.Name,
                Level = model.Level,
                Description = Level.Levels.First(x => x.Key == model.Level).Value,
                SkillId = skill.Id,
            };
            await _abilityStorage.AddAbility(ability);

            return RedirectToAction("Abilities");
        }

        public async Task<IActionResult> AddProfSkill(ProfessionSkillsViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var skill = await _skillStorage.GetSkill(user.Id.ToString());
            var profSkill = new ProfessionSkillArgs()
            {
                Name = model.Name,
                Level = model.Level,
                description = Level.Levels.First(x => x.Key == model.Level).Value,
                SkillId = skill.Id,
            };
            await _profSkillStorage.AddProfessionSkill(profSkill);

            return RedirectToAction("ProfessionSkills");
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

        public async Task<IActionResult> DeleteAbility(string id)
        {
            await _abilityStorage.DeleteAbility(id);

            return RedirectToAction("Abilities");
        }

        public async Task<IActionResult> DeleteProfSkill(string id)
        {
            await _profSkillStorage.DeleteProfessionSkill(id);

            return RedirectToAction("ProfessionSkills");
        }

        public async Task<IActionResult> ProfessionSkills()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var skills = new ProfessionSkillsViewModel()
            {
                Levels = Level.Levels,
                Skills = user.Skill.ProfesionSkills.ToList(),
            };

            return View(skills);
        }
    }
}
