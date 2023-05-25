using Microsoft.AspNetCore.Mvc;
using WebCoreProje.Models;

namespace WebCoreProje.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

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

            }
            return View(model);
        }


        public IActionResult Profil()
        {
            return View();
        }

    }
}
