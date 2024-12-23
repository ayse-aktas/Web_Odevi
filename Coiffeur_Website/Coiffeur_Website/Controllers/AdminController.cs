using Coiffeur_Website.Data;
using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coiffeur_Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly CoiffeurDbContext _context;

        public AdminController(CoiffeurDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(Admin admin)
        {
            if (!ModelState.IsValid) return View();

            var existingAdmin = _context.Adminler.FirstOrDefault(a => a.AdminMail == admin.AdminMail);

            if (existingAdmin == null || existingAdmin.Sifre != admin.Sifre)
            {
                TempData["msj"] = "E-posta veya şifre hatalı!";
                return View();
            }

            // Admin olarak giriş yapan kullanıcıyı session'a kaydet
            HttpContext.Session.SetString("UserRole", "Admin");
            TempData["adminName"] = existingAdmin.AdminAdi;
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            // Kullanıcının admin olup olmadığını kontrol et
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                TempData["msj"] = "Bu sayfaya erişmek için admin olarak giriş yapmalısınız.";
                return RedirectToAction("AdminLogin");
            }

            // Eğer adminse dashboard verilerini yükle
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Include(r => r.Salon)
                .ToList();

            ViewBag.Randevular = randevular.Select(r => new
            {
                r.RandevuId,
                r.RandevuTarihi,
                r.OnayDurumu,
                IslemAdi = r.Islem?.IslemAdi,
                CalisanAdi = r.Calisan?.CalisanAd
            }).ToList();

            return View(randevular);
        }

        [HttpPost]
        public IActionResult Onayla(int id)
        {
            // Kullanıcının admin olup olmadığını kontrol et
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                TempData["msj"] = "Bu işlemi gerçekleştirmek için admin olarak giriş yapmalısınız.";
                return RedirectToAction("AdminLogin");
            }

            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.OnayDurumu = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult Reddet(int id)
        {
            // Kullanıcının admin olup olmadığını kontrol et
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                TempData["msj"] = "Bu işlemi gerçekleştirmek için admin olarak giriş yapmalısınız.";
                return RedirectToAction("AdminLogin");
            }

            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.OnayDurumu = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Admin oturumunu sonlandır
            HttpContext.Session.Clear(); // Tüm session bilgilerini temizler
            TempData["msj"] = "Başarıyla çıkış yaptınız.";
            return RedirectToAction("AdminLogin"); // Admin giriş sayfasına yönlendir
        }
    }
}
