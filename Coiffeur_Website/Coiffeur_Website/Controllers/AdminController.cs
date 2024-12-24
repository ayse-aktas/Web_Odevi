using Coiffeur_Website.Data;
using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (!ModelState.IsValid)
                return View();

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

            // Dashboard verilerini yükle
            var randevular = _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Include(r => r.Islem.Salon)
                .ToList();

            ViewBag.Randevular = randevular.Select(r => new
            {
                r.RandevuId,
                r.RandevuTarihi,
                r.OnayDurumu,
                r.Islem.IslemAdi,
                r.Calisan?.CalisanAd
            }).ToList();

            return View(randevular);
        }

        [HttpPost]
        public IActionResult Onayla(int id)
        {
            // Admin kontrolü
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                TempData["msj"] = "Bu işlemi gerçekleştirmek için admin olarak giriş yapmalısınız.";
                return RedirectToAction("AdminLogin");
            }

            // Randevuyu bul ve durumunu güncelle
            var randevu = _context.Randevular.FirstOrDefault(r => r.RandevuId == id);
            if (randevu != null)
            {
                randevu.OnayDurumu = "Onaylandı";
                _context.SaveChanges();
                TempData["msj"] = "Randevu başarıyla onaylandı!";
            }
            else
            {
                TempData["msj"] = "Randevu bulunamadı!";
            }

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult Reddet(int id)
        {
            // Admin kontrolü
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                TempData["msj"] = "Bu işlemi gerçekleştirmek için admin olarak giriş yapmalısınız.";
                return RedirectToAction("AdminLogin");
            }

            // Randevuyu bul ve durumunu güncelle
            var randevu = _context.Randevular.FirstOrDefault(r => r.RandevuId == id);
            if (randevu != null)
            {
                randevu.OnayDurumu = "Onaylanmadı";
                _context.SaveChanges();
                TempData["msj"] = "Randevu reddedildi.";
            }
            else
            {
                TempData["msj"] = "Randevu bulunamadı!";
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

        // Çalışan Listeleme
        [HttpGet]
        public IActionResult CalisanList()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        // Çalışan Ekleme Sayfası
        [HttpGet]
        public IActionResult AddCalisan()
        {
            // Yeni bir çalışan nesnesi oluştur ve görünümle birlikte gönder
            var calisan = new Calisan();
            ViewData["Action"] = "AddCalisan"; // Görünümde kullanılacak
            return View(calisan);
        }
        [HttpPost]
        public IActionResult AddCalisan(Calisan calisan)
        {

            if (!ModelState.IsValid)
            {
                TempData["msj"] = "Lütfen tüm alanları doldurun.";
                return View(calisan);
            }

            // Debug ile gelen verileri kontrol edin
            Console.WriteLine($"Ad: {calisan.CalisanAd}, Soyad: {calisan.CalisanSoyad}, Uzmanlık: {calisan.UzmanlikAlani}");

            _context.Calisanlar.Add(calisan);
            _context.SaveChanges();

            TempData["msj"] = "Çalışan başarıyla eklendi!";
            return RedirectToAction("CalisanList");
        }


        // Çalışan Güncelleme Sayfası
        [HttpGet]
        public IActionResult EditCalisan(int id)
        {
            var calisan = _context.Calisanlar.Include(c => c.Salon).FirstOrDefault(c => c.CalisanId == id);
            if (calisan == null)
            {
                TempData["msj"] = "Çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            // Salon listesini ViewBag ile gönderiyoruz
            ViewBag.Salonlar = _context.Salonlar.Select(s => new SelectListItem
            {
                Value = s.SalonId.ToString(),
                Text = s.SalonAd
            }).ToList();

            return View(calisan);
        }

        // Çalışan Güncelleme İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCalisan(Calisan calisan)
        {
            if (!ModelState.IsValid)
            {
                // Hata durumunda salon listesini tekrar yükle
                ViewBag.Salonlar = _context.Salonlar.Select(s => new SelectListItem
                {
                    Value = s.SalonId.ToString(),
                    Text = s.SalonAd
                }).ToList();
                TempData["msj"] = "Lütfen tüm alanları doldurunuz.";
                return View(calisan);
            }

            var existingCalisan = _context.Calisanlar.FirstOrDefault(c => c.CalisanId == calisan.CalisanId);
            if (existingCalisan == null)
            {
                TempData["msj"] = "Çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            // Mevcut çalışan bilgilerini güncelle
            existingCalisan.CalisanAd = calisan.CalisanAd;
            existingCalisan.CalisanSoyad = calisan.CalisanSoyad;
            existingCalisan.UzmanlikAlani = calisan.UzmanlikAlani;
            existingCalisan.BaslangicSaati = calisan.BaslangicSaati;
            existingCalisan.BitisSaati = calisan.BitisSaati;
            existingCalisan.SalonId = calisan.SalonId;

            _context.SaveChanges();

            TempData["msj"] = "Çalışan bilgileri başarıyla güncellendi!";
            return RedirectToAction("CalisanList");
        }


        // Çalışan Silme
        [HttpPost]
        public IActionResult DeleteCalisan(int id)
        {
            var calisan = _context.Calisanlar.FirstOrDefault(c => c.CalisanId == id);
            if (calisan == null)
            {
                TempData["msj"] = "Çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            _context.Calisanlar.Remove(calisan);
            _context.SaveChanges();

            TempData["msj"] = "Çalışan başarıyla silindi!";
            return RedirectToAction("CalisanList");
        }

        [HttpGet]
        public IActionResult DeleteConfirmed(int id)
        {
            var calisan = _context.Calisanlar.FirstOrDefault(c => c.CalisanId == id);
            if (calisan == null)
            {
                TempData["msj"] = "Çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            return View(calisan); // Onay sayfasına çalışan detaylarını gönder
        }

        [HttpPost]
        public IActionResult DeleteConfirmedPost(int id)
        {
            var calisan = _context.Calisanlar.FirstOrDefault(c => c.CalisanId == id);
            if (calisan == null)
            {
                TempData["msj"] = "Çalışan bulunamadı.";
                return RedirectToAction("CalisanList");
            }

            _context.Calisanlar.Remove(calisan);
            _context.SaveChanges();

            TempData["msj"] = "Çalışan başarıyla silindi!";
            return RedirectToAction("CalisanList");
        }
    }
}

