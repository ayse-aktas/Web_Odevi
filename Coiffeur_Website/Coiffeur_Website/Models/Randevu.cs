using System.ComponentModel.DataAnnotations;

namespace Coiffeur_Website.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public bool OnayDurumu { get; set; }

        // Foreign Key alanları
        public int CalisanId { get; set; }
        public int IslemId { get; set; }
        public int SalonId { get; set; }

        // Navigasyon özellikleri
        public Calisan Calisan { get; set; }
        public Islem Islem { get; set; }
        public Salon Salon { get; set; }

        // Randevunun onaylanıp onaylanmadığını belirten bir özellik
        [Display(Name = "Randevu Onayı")]
        public string OnayDurumuText
        {
            get
            {
                return OnayDurumu ? "Onaylı" : "Onaysız";
            }
        }
    }
}
