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
            // Kullanıcı oturumu kontrol et
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Musteri")
            {
                TempData["msj"] = "Bu sayfaya erişmek için müşteri olarak giriş yapmalısınız.";
                return RedirectToAction("MusteriLogin", "Musteri");
            }

            string musteriName = TempData["musteriName"] as string;
            TempData.Keep("musteriName");
            ViewBag.MusteriName = musteriName;

            // Oturumdaki müşterinin randevularını getir
            var musteriMail = HttpContext.User.Identity.Name;
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Where(r => r.Musteri.MusteriMail == musteriMail)
                .ToList();

            return View(randevular);
        }


        [HttpGet]
        public IActionResult RandevuAl()
        {
            // Kuaför (Çalışan) verilerini doldur
            ViewBag.Calisanlar = _context.Calisanlar.Select(c => new SelectListItem
            {
                Value = c.CalisanId.ToString(),
                Text = c.CalisanAd
            }).ToList();

            // İşlem verilerini doldur
            ViewBag.Islemler = _context.Islemler.Select(i => new SelectListItem
            {
                Value = i.IslemId.ToString(),
                Text = $"{i.IslemAdi} - {i.Ucret}₺ ({i.Sure} dk)"
            }).ToList();

            return View();
        }


        [HttpPost]
        public IActionResult RandevuAl(int islemId, DateTime tarih, string saat, int calisanId)
        {
            var musteriId = HttpContext.Session.GetInt32("MusteriId");

            if (musteriId == null)
            {
                TempData["msj"] = "Oturum bilgileriniz bulunamadı. Lütfen tekrar giriş yapınız.";
                return RedirectToAction("MusteriLogin", "Musteri");
            }

            var mevcutRandevu = _context.Randevular
                .FirstOrDefault(r => r.RandevuTarihi == tarih && r.RandevuSaati == saat && r.CalisanId == calisanId);

            if (mevcutRandevu != null)
            {
                TempData["msj"] = "Seçilen saat diliminde başka bir randevu mevcut!";
                return RedirectToAction("RandevuAl");
            }

            var randevu = new Randevu
            {
                RandevuTarihi = tarih,
                RandevuSaati = saat,
                IslemId = islemId,
                CalisanId = calisanId,
                MusteriId = musteriId.Value,
                OnayDurumu = "Onaylanmadı" // Varsayılan değer olarak atanıyor
            };

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            TempData["msj"] = "Randevunuz başarıyla oluşturuldu!";
            return RedirectToAction("MusteriDashboard");
        }


        [HttpGet]
        public IActionResult GetCalisanMusaitSaatler(int calisanId, DateTime tarih, int islemId)
        {
            var calisan = _context.Calisanlar.FirstOrDefault(c => c.CalisanId == calisanId);
            if (calisan == null)
            {
                return Json(new { success = false, message = "Çalışan bulunamadı" });
            }

            var islem = _context.Islemler.FirstOrDefault(i => i.IslemId == islemId);
            if (islem == null)
            {
                return Json(new { success = false, message = "İşlem bulunamadı" });
            }

            // Çalışma aralıklarını hesapla
            var calismaAraliklari = calisan.GetCalismaAraliklari(islem.Sure);

            // Seçilen tarihte dolu olan saat aralıklarını kontrol et
            var doluSaatler = _context.Randevular
                .Where(r => r.CalisanId == calisanId && r.RandevuTarihi.Date == tarih.Date)
                .Select(r => new { r.RandevuSaati })
                .ToList();

            // Müsait saatleri hesapla
            var musaitSaatler = calismaAraliklari
                .Where(a => !doluSaatler.Any(d => d.RandevuSaati == a.Baslangic.ToString("HH:mm")))
                .Select(a => new { Baslangic = a.Baslangic.ToString("HH:mm"), Bitis = a.Bitis.ToString("HH:mm") })
                .ToList();

            return Json(new { success = true, saatler = musaitSaatler });
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
