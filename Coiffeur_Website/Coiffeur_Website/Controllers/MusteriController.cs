using Coiffeur_Website.Data;
using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coiffeur_Website.Controllers
{
    public class MusteriController : Controller
    {
        // Kayıt Formunu Göster
        private readonly CoiffeurDbContext _context;


        public MusteriController(CoiffeurDbContext context)
        {
            _context = context;
        }

        // Müşterileri Listeleme
        public IActionResult Index()
        {
            // Veritabanındaki tüm müşteri verilerini al
            var musteriler = _context.Musteriler.ToList();

            // View'a gönder
            return View(musteriler);
        }


        /*public IActionResult Index()
        {
            var musteriler = _context.Musteriler
                .OrderBy(m => m.MusteriId)  // ID'ye göre sıralama yap
                .ToList();

            return View(musteriler);  // Veriyi View'a gönder
        }*/
        public IActionResult KayitOl()
        {
            return View();
        }

        // Kayıt İşlemini Gerçekleştir
        [HttpPost]
        public IActionResult KayitOl(Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                // Veritabanına kaydetme işlemi burada yapılır.
                // Örneğin:
                // _context.Musteriler.Add(musteri);
                // _context.SaveChanges();
                try
                {
                    _context.Musteriler.Add(musteri);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Veri ekleme hatası: {ex.Message}");
                }
                TempData["Message"] = "Kayıt başarılı bir şekilde gerçekleşti.";
                return RedirectToAction("Index", "Home");
            }

            // Eğer model doğrulama başarısızsa form yeniden gösterilir.
            return View(musteri);
        }
    }
}
