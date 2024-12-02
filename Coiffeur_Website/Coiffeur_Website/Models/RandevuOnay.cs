namespace Coiffeur_Website.Models
{
    public class RandevuOnay
    {
        public int RandevuId { get; set; }  // İlişkili randevu
        public Randevu Randevu { get; set; }  // Randevu bilgisi
        public bool OnayDurumu { get; set; }  // Randevunun onay durumu (onaylı/onaysız)
        public string Aciklama { get; set; }  // Onaylama süreciyle ilgili açıklama
    }

}
