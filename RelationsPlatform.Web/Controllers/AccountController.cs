using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using RelationsPlatform.Persistence.Infrastructure;
using RelationsPlatform.Persistence.Infrastructure.Repository;
using RelationsPlatform.Persistence.Model;
using RelationsPlatform.Web.ViewModels;

namespace RelationsPlatform.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountStorage _accountStorage;
        private readonly IUserStorage _userStorage;
        private readonly ISkillStorage _skillStorage;
        private readonly IEducationStorage _educationStorage;

        public AccountController(IAccountStorage accountStorage, IUserStorage userStorage, ISkillStorage skillStorage, IEducationStorage educationStorage)
        {
            _userStorage = userStorage;
            _accountStorage = accountStorage;
            _skillStorage = skillStorage;
            _educationStorage = educationStorage;
        }

        public IActionResult Index(string returnUrl = null)
        {
            if ((bool)this.User?.Identity?.IsAuthenticated)
            {
                return this.RedirectToAction("Profile", "User");
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if ((bool)this.User?.Identity?.IsAuthenticated)
            {
                return this.RedirectToAction("Profile", "User");
            }

            return this.View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = await _userStorage.GetUser(model.Login, model.Password);
                if (user != null)
                {
                    await this.Authenticate(user);

                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    {
                        return this.Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return this.RedirectToAction("Profile", "User");
                    }
                }

                this.ModelState.AddModelError(string.Empty, "Wrong login or password!");
            }

            return RedirectToAction("Index", "Account", new { model });
        }


        [HttpGet]
        public IActionResult Registration() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                User user = await _userStorage.GetUser(model.Login);
                if (user == null)
                {
                    if (model.Password == model.PasswordConfirm)
                    {
                        var userArgs = new UserArgs { Login = model.Login, Password = model.Password, Name = model.Name };

                        await _userStorage.CreateUser(userArgs);
                        var createdUser = await _userStorage.GetUser(model.Login);
                        await _skillStorage.Create(createdUser.Id.ToString());
                        await _educationStorage.Create(createdUser.Id.ToString());
                        await this.Authenticate(createdUser);                        
                        return this.RedirectToAction("Profile", "User");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Паролі не співпадають");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Цей логін вже використовується");
                }
            }


            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync();
            return this.RedirectToAction("Index", "Account");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                    };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
