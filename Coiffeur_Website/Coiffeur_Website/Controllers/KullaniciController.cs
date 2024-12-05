using Coiffeur_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coiffeur_Website.Controllers
{
    public class KullaniciController : Controller
    {
        static List<Kullanici> Kullanicilar = new List<Kullanici>()
        {
            //id ad soyad  kullanici adi tel
            new Kullanici{Id=1,Ad="Ayşe",Soyad="Aktaş",TelNo="+905555555245",Sifre="123"},
            new Kullanici{Id=2,Ad="Büşra",Soyad="Sevinç",TelNo="+905555555215",Sifre="234"},
            new Kullanici{Id=3,Ad="Aslıhan",Soyad="Yıldırım",TelNo="+905555555247",Sifre="234"},
            new Kullanici{Id=4,Ad="Elif",Soyad="Toprak",TelNo="+905555575245",Sifre="234"}
        };
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SesKullanici") is null)
            {
                return View();
            }
            return View("AnaSayfa");
        }
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("SesKullanici") is not null)
            {
                TempData["msj"] = "Güvenli bir şekilde çıkış yaptınız";
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index");
        }
        public IActionResult KullaniciLogin(Kullanici k)
        {
            foreach (var kullanici in Kullanicilar)
            {
                if (k.Ad == kullanici.Ad && k.Sifre == kullanici.Sifre)
                {
                    //Login başarılı
                    HttpContext.Session.SetString("SesKullanici", kullanici.Ad);
                    var cookopt = new CookieOptions
                    {
                        Expires = DateTime.Now.AddSeconds(100),
                    };
                    TempData["msj"] = kullanici.Ad + " kullanıcısı login oldu";
                    return RedirectToAction("KullaniciIcerik");
                }
            }
            TempData["msj"] = "Kullanıcı Adı/Şifre Hatalı";
            return RedirectToAction("Index");
        }

        public IActionResult KullaniciSesSet()
        {
            Kullanici kullanici = new Kullanici();
            kullanici.Ad = "Hasan";
            kullanici.Sifre = "ztaa";
            string usrJson = JsonConvert.SerializeObject(kullanici);

            HttpContext.Session.SetString("SesObj", usrJson);

            return RedirectToAction("KullaniciIcerik");
        }
        public IActionResult KullaniciSesGet()
        {
            if (HttpContext.Session.GetString("SesObj") is not null)
            {
                var stJson = HttpContext.Session.GetString("SesObj");
                Kullanici u = JsonConvert.DeserializeObject<Kullanici>(stJson);
                return View(u);
            }
            TempData["msj"] = "Once Obje Session oluştur";
            return RedirectToAction("Index");
        }

        public IActionResult KullaniciIcerik()
        {
            if (HttpContext.Session.GetString("SesKullanici") is null)
            {
                TempData["msj"] = "Lütfen önce Login olun";
                return RedirectToAction("Index");
            }

            ViewData["Yorumlar"] = new List<string>
            {
                "Hizmet çok hızlı ve profesyoneldi!",
                "Çalışanlar güler yüzlü, kesinlikle tavsiye ederim.",
                "Fiyatlar uygun ve kaliteli bir salon."
            };
            return View();
        }



        //Kullanıcı kayıt yapabiliyor ancak veri tabanı bağlantısı henüz olmadığı için tekrar çalıştırılınca o kullanıcı siliniyor.
        public IActionResult KullaniciKayit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KullaniciKayit(Kullanici yeniKullanici)
        {
            if (ModelState.IsValid)
            {
                // ID'nin eşsiz olması için liste kontrolü
                if (Kullanicilar.Any(k => k.TelNo == yeniKullanici.TelNo))
                {
                    TempData["msj"] = "Bu telefon numarası zaten kayıtlı!";
                    return View();
                }

                yeniKullanici.Id = Kullanicilar.Count > 0 ? Kullanicilar.Max(k => k.Id) + 1 : 1;
                Kullanicilar.Add(yeniKullanici);
                TempData["msj"] = "Kullanıcı başarıyla kaydedildi!";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Lütfen tüm alanları doğru şekilde doldurun.";
            return View();
        }
        //-------------------------------------------------------------------------------------------
    }
}
