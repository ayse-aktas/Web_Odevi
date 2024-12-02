using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;

public class RandevuController : Controller
{
    // Örnek çalışanlar
    static List<Calisan> Calisanlar = new List<Calisan>
    {
        new Calisan
        {
            Id = 1, Ad = "Ali",
            IslemTurleri = new List<int> { 1, 2 }, // Saç Kesimi ve Fön Çekimi yapabilir
            CalismaSaatleri = new List<CalisanSaat>
            {
                new CalisanSaat { Id = 1, BaslangicSaati = new TimeSpan(9, 0, 0), BitisSaati = new TimeSpan(10, 0, 0), Doluluk = false },
                new CalisanSaat { Id = 2, BaslangicSaati = new TimeSpan(10, 0, 0), BitisSaati = new TimeSpan(11, 0, 0), Doluluk = false }
            }
        },
        new Calisan
        {
            Id = 2, Ad = "Veli",
            IslemTurleri = new List<int> { 2 }, // Sadece Fön Çekimi yapabilir
            CalismaSaatleri = new List<CalisanSaat>
            {
                new CalisanSaat { Id = 3, BaslangicSaati = new TimeSpan(9, 0, 0), BitisSaati = new TimeSpan(10, 0, 0), Doluluk = false },
                new CalisanSaat { Id = 4, BaslangicSaati = new TimeSpan(11, 0, 0), BitisSaati = new TimeSpan(12, 0, 0), Doluluk = true }
            }
        }
    };

    static List<Islem> Islemler = new List<Islem>
    {
        new Islem { Id = 1, Ad = "Saç Kesimi", Sure = 30 },
        new Islem { Id = 2, Ad = "Fön Çekimi", Sure = 20 }
    };

    // İşlem Seçimi Sayfası
    public IActionResult IslemSecim()
    {
        return View(Islemler); // İşlemleri listele
    }

    // Çalışan Seçim Sayfası
    [HttpPost]
    public IActionResult CalisanSec(int islemId)
    {
        var uygunCalisanlar = Calisanlar
            .Where(c => c.IslemTurleri.Contains(islemId)) // İşlemi yapabilen çalışanları filtrele
            .ToList();

        var islem = Islemler.FirstOrDefault(i => i.Id == islemId);
        if (islem == null) return NotFound("İşlem bulunamadı");

        ViewBag.Islem = islem;
        return View(uygunCalisanlar); // Uygun çalışanları listele
    }

    // Saat Seçim Sayfası
    [HttpPost]
    public IActionResult SaatSec(int calisanId, int islemId)
    {
        var calisan = Calisanlar.FirstOrDefault(c => c.Id == calisanId);
        var islem = Islemler.FirstOrDefault(i => i.Id == islemId);

        if (calisan == null || islem == null) return NotFound("Çalışan veya işlem bulunamadı");

        // Çalışanın uygun saatlerini al
        var uygunSaatler = calisan.CalismaSaatleri
            .Where(cs => !cs.Doluluk) // Doluluk durumunu kontrol et
            .ToList();

        ViewBag.Islem = islem;
        ViewBag.Calisan = calisan;
        return View(uygunSaatler); // Uygun saatleri göster
    }

    // Randevu Oluşturma
    [HttpPost]
    public IActionResult RandevuOlustur(int calisanId, int islemId, int saatId)
    {
        var calisan = Calisanlar.FirstOrDefault(c => c.Id == calisanId);
        var islem = Islemler.FirstOrDefault(i => i.Id == islemId);
        var saat = calisan?.CalismaSaatleri.FirstOrDefault(cs => cs.Id == saatId);

        if (calisan == null || islem == null || saat == null) return NotFound("Geçersiz işlem veya saat");

        // Saat dolu değilse randevuyu oluştur
        if (!saat.Doluluk)
        {
            saat.Doluluk = true; // Saati dolu olarak işaretle

            var randevu = new Rezervasyon
            {
                CalisanId = calisanId,
                IslemId = islemId,
                BaslangicTarihi = DateTime.Today.Add(saat.BaslangicSaati),
                BitisTarihi = DateTime.Today.Add(saat.BaslangicSaati).AddMinutes(islem.Sure)
            };

            // Randevuyu veritabanına kaydetme işlemi eklenebilir
            TempData["msj"] = "Randevu başarıyla oluşturuldu!";
            return RedirectToAction("Index", "Home");
        }

        TempData["msj"] = "Seçilen saat dolu, lütfen başka bir saat seçin.";
        return RedirectToAction("SaatSec", new { calisanId, islemId });
    }
}
