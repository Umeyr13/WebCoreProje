using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebCoreProje.Models;
using WebCoreProje.Models.Entities;
using WebCoreProje.Models.ViewModel;

namespace WebCoreProje.Controllers
{
    [Authorize(Roles = "admin, başka rol, bir başka rol")]
    public class UserController : Controller
    {
        private readonly DatabaseContext _db;//bunu yazınca alttaki otomatik gelir.. Elle yazmaya gerek yok
        private readonly IMapper _mapper;

        public UserController(DatabaseContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<User> liste = _db.Users.ToList();

            List<UserModel> yeniliste =liste.Select(x =>_mapper.Map<UserModel>(x)).ToList();

            return View(yeniliste);

            #region AutoMapper Olmazsa Yapmamız Gerekenler
            ////1,Yol 
            //foreach (User user in liste)
            //{
            //    yeniliste.Add(new UserModel() {
                    
            //        Name = user.Name,
            //        Email = user.Email //gibi atanabilir..
                    
            //        });
            //}

            ////2.Yol
            //yeniliste = _db.Users.Select(x=> new UserModel() { Name = x.Name, Email = x.Email}).ToList();// gibi atanabilir..

            #endregion

            //Katmanlar arası veri taşımak için Dto kullanılır.
            //View için kullanılıcaksa adına ViewModel deriz. Araştır..          
        }

        public IActionResult Create() //zorunlu alanlardan dolayı direk entitiler kullanılmaz. Aslında herşey için ayrı model olsa daha iyi. View model ekliyoruz..CreateUserModel
        {



            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserModel model)//AutoMapper a Map ekliyoruz bunun için
        {
            if (ModelState.IsValid)
            {
                bool usernamecheck = _db.Users.Any(x => x.Username == model.Username);
                bool emailcheck = _db.Users.Any(x => x.Email == model.Email);

                if (usernamecheck)
                {
                    ModelState.AddModelError("UserName","Bu kullanıcı zaten kayıtlı");
                }
                if (emailcheck)
                {
                    ModelState.AddModelError("Email","Bu mail kayıtlı");
                }
                if (emailcheck || usernamecheck) { return View(model);}

                User user = _mapper.Map<User>(model);
                user.CreateDate = DateTime.Now;
                user.ProfilImagefileName= "No_Image_Available";
                _db.Users.Add(user);
                _db.SaveChanges();
                return   RedirectToAction("Index");

            }
            return View(model);    
        }
        public IActionResult Edit( Guid? Id) 
        {
            if (Id==null)
            {
                return NotFound();
            }
            User user = _db.Users.Find(Id);
            EditUserModel model = _mapper.Map<EditUserModel>(user);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid Id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                bool usernamecheck = _db.Users.Any(x => x.Username == model.Username && x.Id!=model.Id);
                bool emailcheck = _db.Users.Any(x => x.Email == model.Email && x.Id != model.Id);

                if (usernamecheck)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı zaten kayıtlı");
                }
                if (emailcheck)
                {
                    ModelState.AddModelError("Email", "Bu mail kayıtlı");
                }
                if (emailcheck || usernamecheck) { return View(model); }

                User user = _db.Users.Find(Id);
                _mapper.Map(model, user);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        
        public IActionResult Delete(Guid Id)
        {
            User user = _db.Users.Find(Id);
            if (user!=null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
                return RedirectToAction("Index");
        }

    }
}
