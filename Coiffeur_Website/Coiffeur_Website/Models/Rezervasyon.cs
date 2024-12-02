namespace Coiffeur_Website.Models
{
    public class Rezervasyon
    {
        public int Id { get; set; }
        public int CalisanId { get; set; }
        public int IslemId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}
