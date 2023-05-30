using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.ComponentModel.DataAnnotations;
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
                    claims.Add(new Claim(ClaimTypes.Role,user.Role ));
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

                if (kullanicimail != null)
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
                    Role ="user",//default role verdik.
                    ProfilImagefileName = "~/resim/No_Image_Available"


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

        private void ProfilBilgileri()
        {
            User user = UserFind();
            ViewData["ad"] = user.Name;
            ViewData["soyad"] = user.Surname;
            ViewData["kullanıcı"] = user.Username;
            ViewData["email"] = user.Email;
            ViewData["sifre"] = user.Pasword;
            ViewData["resim"] = user.ProfilImagefileName;
            
        }

        public IActionResult Profil()
        {
            ProfilBilgileri();
            return View();
        }

        [HttpPost]
        public IActionResult AdKaydet(string ad) //butonun naminden bu değer geliyor
        {
            User user = UserFind();
            user.Name = ad;
            _db.SaveChanges();
            ViewData["mesaj"] = "Ad Kaydedildi";
            ProfilBilgileri();// Aşağıdaki açıklamadan sonra bu eklendi. direk view e yönlendiriyoruz ama önce viewbag leri tekrar dollduruyoruz yoksa boş gider..
            return View("Profil"); //Direk return View dersek ViewData lar boş kalıyor. onun yerine RedirectToAction kullandık. Action u yeniden yüklüyor doldurarak. 
        }

        [HttpPost]
        public IActionResult SoyadKaydet(string soyad)
        {
            User user = UserFind();
            user.Surname = soyad;
            _db.SaveChanges();
            ViewData["mesaj"] = "Soyad Kaydedildi";
            ProfilBilgileri();
            return View("Profil");
        }

        [HttpPost]
        public IActionResult UserNameKaydet(string kulAdi)
        {
            if (ModelState.IsValid)
            {

                User user = UserFind();
                User kullaniciname = _db.Users.FirstOrDefault(x => x.Username == kulAdi && x.Id != user.Id);//ve bulduğun ben değilsem..        
                if (kullaniciname != null)
                {
                    ModelState.AddModelError("Username", "Bu kullanıcı adı zaten kayıtlı");
                    ProfilBilgileri();
                     return View("Profil"); // Hata! Eğer RedirectToAction yaparsak validation hataları çıkmıyor. View yaparsak da Textboxlar boş kalıyor.
                }

                user.Username = kulAdi;
                _db.SaveChanges();
                ViewData["mesaj"] = "Kullanıcı adı Kaydedildi";
                ProfilBilgileri();
                return View("Profil");
            }

            return View(nameof(Profil));
        }

        [HttpPost]
        public IActionResult EmailKaydet(string Email)
        {
            if (ModelState.IsValid)
            {
                User user = UserFind();
                User kullaniciMail = _db.Users.FirstOrDefault(x => x.Email == Email && x.Id != user.Id);

                if (kullaniciMail != null)
                {
                    ModelState.AddModelError("Email","Bu Email zaten kayıtlı" );
                    ProfilBilgileri();
                    return View("Profil");
                }
                user.Email = Email;
                _db.SaveChanges();
                ProfilBilgileri();
                ViewData["mesaj"] = "Email Kaydedildi";
            }
            return View("Profil");
        }

        [HttpPost]
        public IActionResult SifreKaydet([MaxLength(16),MinLength(6)]string Sfre)
        {
            if (ModelState.IsValid)
            {
                User user = UserFind();
                if (_db.Users.Any(x=>x.Pasword != Sfre && x.Id == user.Id))
                {
                    user.Pasword = StringHashed(Sfre);
                    _db.SaveChanges();

                }
                    ViewData["mesaj"]="Şifre Kaydedildi";
            }
            ProfilBilgileri();
            return View(nameof(Profil));
        }

        public IActionResult ProfilResimKaydet(IFormFile resim)
        {
            if (ModelState.IsValid)
            {
                User user = UserFind();
                string dosyaadi = user.Id+".jpg";
                Stream dosya = new FileStream("wwwroot/resim/"+dosyaadi,FileMode.OpenOrCreate);
                resim.CopyTo(dosya);
                dosya.Close();//stream ı kapatmak lazım
                dosya.Dispose();//stream ı iş bitince sil 
                if (System.IO.File.Exists("wwwroot/resim/" + dosyaadi))
                {
                    user.ProfilImagefileName = dosyaadi;
                    _db.SaveChanges();

                }
                user.ProfilImagefileName = dosyaadi;
                _db.SaveChanges();
            }
            ProfilBilgileri();
            return View(nameof(Profil));
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login");
        }


    }
}
