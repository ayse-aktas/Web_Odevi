using System.Collections.Generic;

namespace Coiffeur_Website.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public string SalonAdi { get; set; }
        public List<Calisan> Calisanlar { get; set; }  // Salonla ilişkili çalışanlar
        public List<Islem> Islemler { get; set; }  // Sunulan işlemler
        public string CalismaSaatleri { get; set; }  // Salon çalışma saatleri
    }

}
