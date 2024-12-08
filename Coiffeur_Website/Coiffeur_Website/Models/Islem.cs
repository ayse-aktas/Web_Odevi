namespace Coiffeur_Website.Models
{
    public class Islem
    {
        public int IslemId { get; set; }
        public int SalonId { get; set; }  // İlişkili salon
        public string IslemAdi { get; set; }
        public int Sure { get; set; }  // Dakika cinsinden işlem süresi
        public int Ucret { get; set; }  // İşlem ücreti

    }

}
