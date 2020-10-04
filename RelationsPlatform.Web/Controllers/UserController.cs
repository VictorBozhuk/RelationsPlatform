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
        private readonly IUserTaskStorage _userTaskStorage;

        public UserController(IUserStorage userStorage, IAbilityStorage abilityStorage, IProfessionSkillStorage profSkillStorage, 
            IJobStorage jobStorage, IEducationStorage educationStorage, ISchoolStorage schoolStorage, ICourseStorage courseStorage, 
            IHigherEducationStorage higherEducationStorage, IRelationStorage relationStorage, ISkillStorage skillStorage, IUserTaskStorage userTaskStorage)
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
            _userTaskStorage = userTaskStorage;
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

        public async Task<IActionResult> AddFeedback(RelationProfileViewModel model)
        {
            await _userStorage.AddFeedback(User.Identity.Name, model.Id, model.Note);

            return RedirectToAction(nameof(RelationProfile), new { id = $"{model.Id}" });
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

        public async Task<IActionResult> AddFriend(string id)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            await _userStorage.AddFriend(user.Id, id);

            return RedirectToAction(nameof(AllUsers));
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

            return RedirectToAction("Schools");
        }

        public async Task<IActionResult> AllUsers(AllUsersViewModel model)
        {
            var userMe = await _userStorage.GetUser(User.Identity.Name);
            var users = (await _userStorage.GetUsers()).Where(x => x != userMe).ToList();

            if (!string.IsNullOrEmpty(model.Name))
            {
                users = users.Intersect(users.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))).ToList();
            }

            if (model.Gender != null && model.Gender != "Всі")
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

        public IActionResult CreateUserTask()
        {
            Subjects.subjects.Sort();
            var task = new UserTaskViewModel()
            {
                Subjects = Subjects.subjects,
            };
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserTask(UserTaskViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var task = new UserTaskArgs()
            {
                UserId = user.Id.ToString(),
                Date = DateTime.Now,
                LongDescription = model.LongDescription,
                ShortDescription = model.ShortDescription,
                Subject = model.Subject,
                Status = "Немає відповіді",
                
            };

            await _userTaskStorage.AddUserTask(task);
            return RedirectToAction(nameof(UserTasks));
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = new UserArgs()
            {
                Login = User.Identity.Name,
                Password = model.NewPassword,
            };

            await _userStorage.ChangePassword(user);
            return RedirectToAction(nameof(Profile));
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

        public async Task<IActionResult> FriendsOfFriend(string id)
        {
            var user = await _userStorage.GetUser(id);

            AllUsersViewModel allUsers = new AllUsersViewModel()
            {
                Users = user.Relations.Select(x => x.RelationUser).ToList(),
            };

            return RedirectToAction("AllUsers", new { allUsers });
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

        public async Task<IActionResult> EditUserTask(string id)
        {
            var task = await _userTaskStorage.GetUserTask(id);
            Subjects.subjects.Sort();
            var model = new UserTaskViewModel()
            {
                Subjects = Subjects.subjects,
                LongDescription = task.LongDescription,
                ShortDescription = task.ShortDescription,
                Status = task.Status,
                Subject = task.Subject,
                TaskId = task.Id.ToString(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserTask(UserTaskViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var task = new UserTaskArgs()
            {
                Id = model.TaskId,
                Date = DateTime.Now,
                LongDescription = model.LongDescription,
                ShortDescription = model.ShortDescription,
                Subject = model.Subject,
                Status = model.Status,
            };

            await _userTaskStorage.EditUserTask(task);
            return RedirectToAction(nameof(MyUserTask), new { id = model.TaskId });
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

        public async Task<IActionResult> DeleteRelation(string id)
        {
            await _schoolStorage.DeleteSchool(id);

            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> MyUserTask(string id)
        {
            var task = await _userTaskStorage.GetUserTask(id);
            var model = new UserTaskViewModel()
            {
                TaskId = id,
                Subject = task.Subject,
                LongDescription = task.LongDescription,
                ShortDescription = task.ShortDescription,
                Date = task.Date,
                Status = task.Status,
            };

            return View(model);
        }

        public async Task<IActionResult> MyUserTasks()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var task = new UserTasksViewModel()
            {
                Tasks = user.Tasks.ToList(),
            };

            return View(task);
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

        public async Task<IActionResult> UserTasks()
        {
            var task = new UserTasksViewModel()
            {
                Tasks = await _userTaskStorage.GetAllUserTasks(),
            };

            return View(task);
        }

        public async Task<IActionResult> UserTask(string id)
        {
            var task = await _userTaskStorage.GetUserTask(id);
            var model = new UserTaskViewModel()
            {
                Subject = task.Subject,
                LongDescription = task.LongDescription,
                ShortDescription = task.ShortDescription,
                Date = task.Date,
                UserId = task.UserId.ToString(),
                Status = task.Status,
            };

            return View(model);
        }

        public async Task<IActionResult> Relations(RelationsViewModel model)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var idRelations = user.Relations.Select(x => x.RelationUser.Id).ToList();
            var users = idRelations.Select(x => _userStorage.GetUserById(x.ToString()).Result).ToList();
            if(model.AbstractLevel == "2")
            {
                var list = new List<User>();
                foreach(var item in users)
                {
                    foreach (var item2 in item.Relations)
                    {
                        list.Add(item2.RelationUser);
                    }
                }
                users = list.Distinct().ToList();
            }

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

            RelationsViewModel modelR = new RelationsViewModel()
            {
                Users = users,
            };

            return View(modelR);
        }


        public async Task<IActionResult> Profile()
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            string raiting;
            if (user.Feedbacks.Count == 0)
            {
                raiting = "0.0";
            }
            else
            {
                raiting = (user.Feedbacks.Select(x => x.Note).Sum() / user.Feedbacks.Count / 10.0).ToString();
            }
            var userViewModel = new ProfileViewModel()
            {
                Raiting = raiting,
                User = user,
            };

            return View(userViewModel);
        }

        public async Task<IActionResult> RelationProfile(string id)
        {
            var user = await _userStorage.GetUser(User.Identity.Name);
            var relationUser = await _userStorage.GetUserById(id);
            string status = null;
            if (user.Relations.Where(x => x.RelationUser == relationUser).Any())
            {
                status = "Friend";
            }
            int note = 0;
            var feedback = relationUser.Feedbacks.FirstOrDefault(x => x.SenderId == user.Id.ToString());
            if (feedback != null)
            {
                note = feedback.Note;
            }
            string raiting;
            if (relationUser.Feedbacks.Count == 0)
            {
                raiting = "0.0";
            }
            else
            {
                raiting = $"{(relationUser.Feedbacks.Select(x => x.Note).Sum() / relationUser.Feedbacks.Count / 10.0).ToString()}/10";
            }
            var userViewModel = new RelationProfileViewModel()
            {
                Id = relationUser.Id.ToString(),
                Note = note,
                Status = status,
                User = relationUser,
                Raiting = raiting,
            };

            return View(userViewModel);
        }
    }
}
