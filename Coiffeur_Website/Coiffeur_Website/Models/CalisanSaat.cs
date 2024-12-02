namespace Coiffeur_Website.Models
{
    public class CalisanSaat
    {
        public int Id { get; set; }
        public int CalisanId { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
        public bool Doluluk { get; set; } // Saat dolu mu kontrolü
    }
}
