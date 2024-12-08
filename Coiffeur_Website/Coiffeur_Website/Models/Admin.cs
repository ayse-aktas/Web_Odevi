using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Coiffeur_Website.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string AdminAdi { get; set; }
        public string AdminSoyadi { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
        public string AdminMail { get; set; }


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
