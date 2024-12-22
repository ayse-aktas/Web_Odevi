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

        // Admin Login GET
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        // Admin Login POST
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

            // Kullanıcının rolünü session'da saklıyoruz
            HttpContext.Session.SetString("UserRole", "Admin");
            TempData["adminName"] = existingAdmin.AdminAdi;
            return RedirectToAction("Dashboard");
        }

        // Admin Dashboard GET
        [HttpGet]
        public IActionResult Dashboard()
        {
            // Randevuları ilgili tablolarla birlikte yüklüyoruz
            var randevular = _context.Randevular
                .Include(r => r.IslemId)  // İşlem bilgilerini yükle
                .Include(r => r.CalisanId) // Çalışan bilgilerini yükle
                .ToList();

            // ViewBag ile randevuları aktar
            ViewBag.Randevular = randevular.Select(r => new
            {
                r.RandevuId,
                r.RandevuTarihi,
                r.OnayDurumu,
                IslemAdi = _context.Islemler.FirstOrDefault(i => i.IslemId == r.IslemId)?.IslemAdi,
                CalisanAdi = _context.Calisanlar.FirstOrDefault(c => c.CalisanId == r.CalisanId)?.CalisanAd
            }).ToList();

            return View(randevular);
        }

        // Randevuyu Onayla
        [HttpPost]
        public IActionResult Onayla(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.OnayDurumu = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        // Randevuyu Reddet
        [HttpPost]
        public IActionResult Reddet(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                randevu.OnayDurumu = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
    }
}
