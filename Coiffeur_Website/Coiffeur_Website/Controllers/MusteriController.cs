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

        [HttpGet]
        public IActionResult MusteriDashboard()
        {
            // Kullanıcının oturumda olup olmadığını ve rolünün "Musteri" olduğunu kontrol et
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Musteri")
            {
                // Yetkisiz kullanıcıyı giriş sayfasına yönlendir
                TempData["msj"] = "Bu sayfaya erişmek için önce müşteri olarak giriş yapmalısınız.";
                return RedirectToAction("MusteriLogin", "Musteri");
            }
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

            return View(_context.Randevular.Include(r => r.Calisan).Include(r => r.Islem).Include(r => r.Salon).ToList());
        }

        [HttpPost]
        public IActionResult RandevuAl(Randevu randevu)
        {
            if (!ModelState.IsValid) return RedirectToAction("MusteriDashboard");

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            TempData["msj"] = "Randevunuz başarıyla oluşturuldu!";
            return RedirectToAction("MusteriDashboard");
        }

        [HttpGet]
        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KayitOl(Musteri musteri)
        {
            if (!ModelState.IsValid)
            {
                TempData["msj"] = "Lütfen tüm alanları doğru bir şekilde doldurun!";
                return View(musteri);
            }

            // Email kontrolü - Zaten kayıtlıysa hata mesajı döndür
            if (_context.Musteriler.Any(m => m.MusteriMail == musteri.MusteriMail))
            {
                TempData["msj"] = "Bu e-posta adresi zaten kayıtlı!";
                return View(musteri);
            }

            // Yeni müşteri kaydı
            _context.Musteriler.Add(musteri);
            await _context.SaveChangesAsync();

            TempData["msj"] = "Kayıt başarılı! Şimdi giriş yapabilirsiniz.";
            return RedirectToAction("MusteriLogin");
        }


        [HttpGet]
        public IActionResult MusteriLogin()
        {
            return View();
        }

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
        public IActionResult GetCalisanlarBySalonId(int salonId)
        {
            var calisanlar = _context.Calisanlar
                .Where(c => c.Salon.SalonId == salonId)
                .Select(c => new { c.CalisanId, c.CalisanAd })
                .ToList();
            return Json(calisanlar);
        }

        [HttpGet]
        public IActionResult GetCalisanAvailability(int calisanId)
        {
            var availability = _context.Randevular
                .Where(r => r.Calisan.CalisanId == calisanId)
                .Select(r => new { r.RandevuTarihi })
                .ToList();
            return Json(availability);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Kullanıcının oturumunu sonlandır
            HttpContext.Session.Clear();
            return RedirectToAction("MusteriLogin");
        }

    }
}
