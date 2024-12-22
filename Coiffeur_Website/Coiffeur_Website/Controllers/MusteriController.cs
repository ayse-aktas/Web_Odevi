using Coiffeur_Website.Data;
using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Coiffeur_Website.Controllers
{
    public class MusteriController : Controller
    {
        private readonly CoiffeurDbContext _context;

        public MusteriController(CoiffeurDbContext context)
        {
            _context = context;
        }

        // Kayıt Formunu Göster
        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        // POST: Musteri/KayitOl
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KayitOl(Musteri musteri)
        {
            if (!ModelState.IsValid) return View(musteri);

            _context.Musteriler.Add(musteri);
            await _context.SaveChangesAsync();
            TempData["msj"] = "Kayıt başarılı!";
            return RedirectToAction("MusteriLogin");
        }

        // Musteri Login GET
        [HttpGet]
        public IActionResult MusteriLogin()
        {
            return View();
        }

        // Musteri Login POST
        [HttpPost]
        public IActionResult MusteriLogin(Musteri musteri)
        {
            if (!ModelState.IsValid) return View();

            var existingMusteri = _context.Musteriler.FirstOrDefault(m => m.MusteriMail == musteri.MusteriMail);

            if (existingMusteri == null || existingMusteri.Sifre != musteri.Sifre)
            {
                TempData["msj"] = "E-posta veya şifre hatalı!";
                return View();
            }

            HttpContext.Session.SetString("UserRole", "Musteri");
            TempData["musteriName"] = existingMusteri.MusteriAdi;
            return RedirectToAction("MusteriDashboard");
        }

        [HttpGet]
        public IActionResult MusteriDashboard()
        {
            string musteriName = TempData["musteriName"] as string;
            TempData.Keep("musteriName");
            ViewBag.MusteriName = musteriName;

            ViewBag.Calisanlar = _context.Calisanlar.Select(c => new SelectListItem
            {
                Value = c.CalisanId.ToString(),
                Text = c.CalisanAd
            }).ToList();

            ViewBag.Islemler = _context.Islemler.Select(i => new SelectListItem
            {
                Value = i.IslemId.ToString(),
                Text = $"{i.IslemAdi} - {i.Ucret}₺ ({i.Sure} dk)"
            }).ToList();

            ViewBag.Salonlar = _context.Salonlar.Select(s => new SelectListItem
            {
                Value = s.SalonId.ToString(),
                Text = s.SalonAd + (s.doluluk ? " (Dolu)" : " (Boş)")
            }).ToList();

            return View(_context.Randevular.ToList());
        }

        [HttpGet]
        public IActionResult GetCalisanlarBySalonId(int salonId)
        {
            var calisanlar = _context.Calisanlar
                .Where(c => c.SalonId == salonId)
                .Select(c => new { c.CalisanId, c.CalisanAd })
                .ToList();
            return Json(calisanlar);
        }

        [HttpGet]
        public IActionResult GetCalisanAvailability(int calisanId)
        {
            var availability = _context.Randevular
                .Where(r => r.CalisanId == calisanId)
                .Select(r => new { r.RandevuTarihi })
                .ToList();
            return Json(availability);
        }
    }
}
