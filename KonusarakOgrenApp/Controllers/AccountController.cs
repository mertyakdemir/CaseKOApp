using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using KonusarakOgrenApp.Models;
using System.Security.Claims;
using KonusarakOgrenApp.Data;

namespace KonusarakOgrenApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AccountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _appDbContext.Users.Where(x => x.Username == userModel.Username).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("Username", "Kullanıcı mevcut değil");
                return View(user);
            }

            if (user!=null && user.Password != userModel.Password)
            {
                ModelState.AddModelError("Password", "Şifre yanlış");
                return View(user);
            }

            ClaimsIdentity identity = null;
            bool isAuth = false;

            if(userModel.Username == "admin" && userModel.Password == "password")
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuth = true;
            }

            if (userModel.Username == "user" && userModel.Password == "password")
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, "User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuth = true;
            }

            if (isAuth)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
