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

        public async Task<IActionResult> AddCourse(CoursesViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var education = await _educationStorage.GetEducation(user.Id.ToString());
            var course = new CourseArgs()
            {
                Name = model.Name,
                Description = model.Description,
                EducationId = education.Id,
            };
            await _courseStorage.AddCourse(course);

            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> AddHigherEducation(HigherEducationsViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var education = await _educationStorage.GetEducation(user.Id.ToString());
            var higher = new HigherEducationArgs()
            {
                University = model.University,
                Faculty = model.Faculty,
                Specialty = model.Speciality,
                Status = model.Status,
                EducationId = education.Id,
            };
            await _higherEducationStorage.AddHigherEducation(higher);

            return RedirectToAction("HigherEducations");
        }

        public async Task<IActionResult> AddJob(JobsViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var skill = await _skillStorage.GetSkill(user.Id.ToString());
            var job = new JobArgs()
            {
                NamePosition = model.NameOfPosition,
                NameOfCompany = model.NameOfCompany,
                DescriptionOfWork = model.DescriptionOfWork,
                SkillId = skill.Id,
            };
            await _jobStorage.AddJob(job);

            return RedirectToAction("Jobs");
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

        public async Task<IActionResult> AddSchool(SchoolsViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var education = await _educationStorage.GetEducation(user.Id.ToString());
            var school = new SchoolArgs()
            {
                Name = model.Name,
                Description = model.Description,
                EducationId = education.Id,
            };
            await _schoolStorage.AddSchool(school);

            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> AllUsers(AllUsersViewModel model)
        {
            var users = await _userStorage.GetUsers();

            if (!string.IsNullOrEmpty(model.Name))
            {
                users = users.Intersect(users.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))).ToList();
            }

            if (model.Gender != null && model.Gender != "Everything")
            {
                users = users.Intersect(users.Where(p => p.Gender == model.Gender)).ToList();
            }

            if (!string.IsNullOrEmpty(model.City))
            {
                users = users.Intersect(users.Where(p => p.Contact.Address.City.ToLower().Contains(model.City.ToLower()))).ToList();
            }

            if (!string.IsNullOrEmpty(model.ProfrssionSkill))
            {
                users = users.Intersect(users.Where(p => p.Skill.ProfesionSkills.Where(x => x.Name.ToLower().Contains(model.ProfrssionSkill.ToLower())).Any())).ToList();
            }

            if (!string.IsNullOrEmpty(model.Ability))
            {
                users = users.Intersect(users.Where(p => p.Skill.Abilities.Where(x => x.Name.ToLower().Contains(model.Ability.ToLower())).Any())).ToList();
            }

            if (!string.IsNullOrEmpty(model.Job))
            {
                users = users.Intersect(users.Where(p => p.Skill.Jobs.Where(x => x.NamePosition.ToLower().Contains(model.Job.ToLower())).Any())).ToList();
            }

            if (!string.IsNullOrEmpty(model.University))
            {
                users = users.Intersect(users.Where(p => p.Education.HigherEducations.Where(x => x.University.ToLower().Contains(model.University.ToLower())).Any())).ToList();
            }

            if (!string.IsNullOrEmpty(model.Faculty))
            {
                users = users.Intersect(users.Where(p => p.Education.HigherEducations.Where(x => x.University.ToLower().Contains(model.Faculty.ToLower())).Any())).ToList();
            }

            AllUsersViewModel allUsers = new AllUsersViewModel()
            {
                Users = users,
            };

            return View(allUsers);
        }

        public async Task<IActionResult> Courses()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var courses = new CoursesViewModel()
            {
                Courses = user.Education.Courses.ToList(),
            };

            return View(courses);
        }

        public async Task<IActionResult> HigherEducations()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var higher = new HigherEducationsViewModel()
            {
                higherEducations = user.Education.HigherEducations.ToList(),
            };

            return View(higher);
        }

        public async Task<IActionResult> Jobs()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var jobs = new JobsViewModel()
            {
                Jobs = user.Skill.Jobs.ToList(),
            };

            return View(jobs);
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
                MainSkill = user.Skill.MainProfession,
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
            if(model.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    model.File.CopyTo(ms);
                    model.Avatar = ms.ToArray();
                }
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

            var user = await _userStorage.GetUser(User.Identity.Name);
            await _skillStorage.EditSkill(user.Id.ToString(), model.MainSkill);
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

        public async Task<IActionResult> DeleteCourse(string id)
        {
            await _courseStorage.DeleteCourse(id);

            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> DeleteHigherEducation(string id)
        {
            await _higherEducationStorage.DeleteHigherEducation(id);

            return RedirectToAction("HigherEducations");
        }

        public async Task<IActionResult> DeleteJob(string id)
        {
            await _jobStorage.DeleteJob(id);

            return RedirectToAction("Jobs");
        }

        public async Task<IActionResult> DeleteProfSkill(string id)
        {
            await _profSkillStorage.DeleteProfessionSkill(id);

            return RedirectToAction("ProfessionSkills");
        }

        public async Task<IActionResult> DeleteSchool(string id)
        {
            await _schoolStorage.DeleteSchool(id);

            return RedirectToAction("Schools");
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

        public async Task<IActionResult> Schools()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var schools = new SchoolsViewModel()
            {
                Schools = user.Education.Schools.ToList(),
            };

            return View(schools);
        }
    }
}
