using Microsoft.AspNetCore.Mvc;
using Coiffeur_Website.Models;

namespace Coiffeur_Website.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin Login
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        // POST: Admin Login
        [HttpPost]
        public IActionResult AdminLogin(Admin admin)
        {
            if (ModelState.IsValid)
            {
                // Admin doğrulama işlemleri
                bool isValidAdmin = admin.KullaniciAdi == "admin" && admin.Sifre == "admin123";
                if (isValidAdmin)
                {
                    TempData["msj"] = "Başarılı admin girişi.";
                    return RedirectToAction("Dashboard", "Admin");
                }
                TempData["msj"] = "Hatalı kullanıcı adı veya şifre!";
            }
            return View();
        }

        // Admin Dashboard sayfası
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
