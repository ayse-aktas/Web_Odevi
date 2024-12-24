using Coiffeur_Website.Models;

namespace Coiffeur_Website.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string RandevuSaati { get; set; } = string.Empty;
        public string OnayDurumu { get; set; } = "Onaylanmadı"; // Varsayılan değer
        public int IslemId { get; set; }
        public int CalisanId { get; set; }
        public int MusteriId { get; set; }
        public int SalonId { get; set; }

        // Navigasyon Özellikleri
        public Islem Islem { get; set; } = null!;
        public Calisan Calisan { get; set; } = null!;
        public Musteri Musteri { get; set; } = null!;
        public Salon Salon { get; set; } = null!;
    }
}
