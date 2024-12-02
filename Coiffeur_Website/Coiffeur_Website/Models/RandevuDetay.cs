namespace Coiffeur_Website.Models
{
    public class RandevuDetay
    {
        public int RandevuId { get; set; }  // İlişkili randevu
        public Randevu Randevu { get; set; }
        public int IslemId { get; set; }  // İlgili işlem
        public Islem Islem { get; set; }
        public decimal Ucret { get; set; }  // İşlem ücreti
        public int Sure { get; set; }  // İşlem süresi
    }

}
