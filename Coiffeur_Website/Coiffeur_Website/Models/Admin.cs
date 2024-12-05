using System.ComponentModel.DataAnnotations;

namespace Coiffeur_Website.Models
{
    public class Admin
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }

        // Randevuyu onaylama işlemi
        public void RandevuOnayla(Randevu randevu)
        {
            if (randevu != null)
            {
                randevu.OnayDurumu = true;
            }
        }

        // Randevuyu reddetme işlemi
        public void RandevuReddet(Randevu randevu)
        {
            if (randevu != null)
            {
                randevu.OnayDurumu = false;
            }
        }

        // Randevu durumu güncelleme metodu
        public void RandevuDurumGuncelle(Randevu randevu, bool durum)
        {
            if (randevu != null)
            {
                randevu.OnayDurumu = durum;
            }
        }
    }
}
