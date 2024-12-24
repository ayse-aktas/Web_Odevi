using Coiffeur_Website.Models;

namespace Coiffeur_Website.Models
{
    public class Calisan
    {
        public int CalisanId { get; set; } // PK
        public string CalisanAd { get; set; } = string.Empty; // Non-nullable default value
        public string CalisanSoyad { get; set; } = string.Empty; // Non-nullable default value
        public string UzmanlikAlani { get; set; } = string.Empty; // Non-nullable default value
        public DateTime BaslangicSaati { get; set; }
        public DateTime BitisSaati { get; set; }
        public int Maas { get; set; }
        public int TopCalismaSaati { get; set; }

        // Günlük Kazanç (Salon'daki doluluk oranını kullanarak hesaplanır)
        public float GunlukKazanc { get; set; }

        // Navigasyon özelliği
        public Salon? Salon { get; set; }
        public int? SalonId { get; set; } // SalonId nullable yapıldı
        public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>(); // Non-nullable default collection

        // Çalışma aralıklarını işlem süresine göre hesaplayan metot
        public List<(DateTime Baslangic, DateTime Bitis)> GetCalismaAraliklari(int islemSure)
        {
            var araliklar = new List<(DateTime Baslangic, DateTime Bitis)>();
            var currentTime = BaslangicSaati;

            while (currentTime < BitisSaati)
            {
                var endTime = currentTime.AddMinutes(islemSure);

                if (endTime > BitisSaati)
                    break;

                araliklar.Add((currentTime, endTime));
                currentTime = endTime;
            }

            return araliklar;
        }
    }
}