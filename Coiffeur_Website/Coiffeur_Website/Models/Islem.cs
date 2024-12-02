namespace Coiffeur_Website.Models
{
    public class Islem
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int Sure { get; set; }  // Dakika cinsinden işlem süresi
        public decimal Ucret { get; set; }  // İşlem ücreti
        public int SalonId { get; set; }  // İlişkili salon
        public Salon Salon { get; set; }  // Salon ile ilişki
    }

}
