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
            new Kullanici{Id=1,Ad="Ayşe",Soyad="Aktaş",TelNo="555 555 5245",Sifre="123"},
            new Kullanici{Id=1,Ad="Büşra",Soyad="Sevinç",TelNo="555 555 5245",Sifre="234"}
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
            return View();
        }
    }
}
