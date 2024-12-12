using Coiffeur_Website.Data;
using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // GET: Musteri/KayitOl
        public IActionResult KayitOl()
        {
            return View();
        }

        // POST: Musteri/KayitOl
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KayitOl([Bind("MusteriAdi,MusteriSoyadi,Sifre,MusteriMail,TelNo")] Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musteri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Kayıt başarılı olduktan sonra listeleme sayfasına yönlendir
            }
            return View(musteri); // Eğer model geçerli değilse, aynı formu tekrar göster
        }

        // Index sayfası (Müşterileri listeleme)
        public IActionResult Index()
        {
            var musteriler = _context.Musteriler.ToList(); // Veritabanındaki tüm müşteriler
            return View(musteriler);
        }
    }
}
