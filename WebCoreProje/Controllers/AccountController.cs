﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebCoreProje.Models;

namespace WebCoreProje.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly IConfiguration _configuration;
        public AccountController(DatabaseContext db, IConfiguration configuration) //Dipendenct enjection
        {
            _db = db;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string sifre = StringHashed(model.Pasword);
                User user = _db.Users.FirstOrDefault(x=>x.Username  ==  model.Username && x.Pasword==sifre);
                if (user != null)
                {
                    if (!user.Aktivate)
                    {
                        ModelState.AddModelError("","Kullanıcı Aktif Değil!");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
                    claims.Add(new Claim("Name",user.Name?? string.Empty));//eğer null ise empty yaz
                    claims.Add(new Claim("UserName",user.Username));
                    //ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims,"Cookie"));
                    //HttpContext.SignInAsync("Cookie", principal);
                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                    return RedirectToAction("Index","Home");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string sifre = StringHashed(model.Pasword);
                User user = new() //new User() demeden de oluyor .net 6.0 ın özelliği
                {
                    Username = model.Username,
                    Email = model.Email,
                    Pasword = sifre,
                    CreateDate = DateTime.Now,
                    Aktivate = true


                };

                _db.Add(user);
                int sonuc = _db.SaveChanges(); //how many rows effected
                if (sonuc > 0) //if effected rows more than zero
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(""/*Buraya neyin hatası olduğu belirtilebilir.*/, "Kullanıcı Kaydedilemedi!!");
                }

            }
            return View(model);
        }

        private string StringHashed(string kullanıcısifre)
        {
            string salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string sifre = kullanıcısifre + salt;
            sifre = sifre.MD5();
            return sifre;
        }

        public IActionResult Profil()
        {
            return View();
        }

    }
}
