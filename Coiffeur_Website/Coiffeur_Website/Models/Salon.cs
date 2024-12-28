using System.Collections.Generic;

namespace Coiffeur_Website.Models
{
    public class Salon
    {
        public int SalonId { get; set; } = 4; // PK
        public string SalonAd { get; set; }
        public bool doluluk { get; set; }
        public int? dolulukSuresi { get; set; }

        // Navigasyon özellikleri
        public ICollection<Calisan>? Calisanlar { get; set; }
        public ICollection<Randevu>? Randevular { get; set; }
        public ICollection<Islem>? Islemler { get; set; }
    }
}
