using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using WebCoreProje.Models;
using WebCoreProje.Models.Entities;

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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost,AllowAnonymous]
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
                    //claims.Add(new Claim(ClaimTypes.Role,user.Role ));
                    claims.Add(new Claim("UserName",user.Username));

                    //ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims,"Cookie"));
                    //HttpContext.SignInAsync("Cookie", principal);
                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                    return RedirectToAction("Index","Home");
                }

                else
                {
                    ModelState.AddModelError("","Kullanıcı veya şifre hatalı ");
                }
            }
            ModelState.AddModelError("", "Kullanıcı Adı veya şifre hatalı");
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost,AllowAnonymous]
        public IActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User kullaniciname = _db.Users.FirstOrDefault(x=>x.Username == model.Username);

                User kullanicimail = _db.Users.FirstOrDefault(x => x.Email == model.Email);

                if (kullaniciname!=null)
                {
                   ModelState.AddModelError("Username","Bu kullanıcı adı zaten kayıtlı");                    
                }

                if (kullanicimail.Email != null)
                {
                   ModelState.AddModelError("Email","Bu email zaten kayıtlı");                 
                }

                if (kullaniciname != null || kullanicimail !=null)
                {
                    return View(model);
                }

                string sifre = StringHashed(model.Pasword);
                User user = new() //new User() demeden de oluyor .net 6.0 ın özelliği
                {
                    Username = model.Username,
                    Email = model.Email,
                    Pasword = sifre,
                    CreateDate = DateTime.Now,
                    Aktivate = true,
                    //Role ="user"//default role verdik.
                    


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

        private string StringHashed(string kullanıcısifre)//kullanıcı şifresini şifreleme
        {
            string salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string sifre = kullanıcısifre + salt;
            sifre = sifre.MD5();
            return sifre;
        }

        public User UserFind()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));//FindFirs ilk claimname i getir yukarıda iki tane değer atmıştık eğer claim boş gelirse null attık onu getirmesin diye.

            return _db.Users.Find(userid);
        }

        public IActionResult Profil()
        {
            
            User user = UserFind();
            ViewData["ad"] = user.Name;
            ViewData["soyad"]= user.Surname;
            ViewData["kullanıcı"] = user.Username;
            ViewData["email"] = user.Email;
            //ViewData["şifre"] = user.Pasword; şifreyi geri çözdürmek lazım şifre şifreli sonra bakarız.

            return View();
        }

        [HttpPost]
        public IActionResult AdKaydet(string ad) //butonun naminden bu değer geliyor
        {
            User user = UserFind();
            user.Name = ad;
            _db.SaveChanges();
            return RedirectToAction("Profil"); //Direk return View dersek ViewData lar boş kalıyor. onun yerine RedirectToAction kullandık. Action u yeniden yüklüyor doldurarak.
        }

        [HttpPost]
        public IActionResult SoyadKaydet(string soyad)
        {
            User user = UserFind();
            user.Surname = soyad;
            _db.SaveChanges();
            return RedirectToAction("Profil");
        }

        [HttpPost]
        public IActionResult UserNameKaydet(string kulAdi)
        {
            User user = UserFind();
            User kullaniciname = _db.Users.FirstOrDefault(x => x.Username == kulAdi && x.Id != user.Id);//ve bulduğun ben değilsem..        

            if (kullaniciname != null)
            {
                ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kayıtlı");
                 return RedirectToAction("Profil");
            }

            user.Username = kulAdi;
            _db.SaveChanges();
            return RedirectToAction("Profil");
        }

        [HttpPost]
        public IActionResult EmailKaydet()
        {
            User user = UserFind();

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


    }
}
