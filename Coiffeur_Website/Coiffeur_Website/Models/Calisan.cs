using Coiffeur_Website.Models;

public class Calisan
{
    public int CalisanId { get; set; } // PK
    public string CalisanAd { get; set; }
    public string CalisanSoyad { get; set; }
    public string UzmanlikAlani { get; set; }
    public DateTime BaslangicSaati { get; set; }
    public DateTime BitisSaati { get; set; }
    public int Maas { get; set; }
    public int TopCalismaSaati { get; set; }

    // Günlük Kazanç (Salon'daki doluluk oranını kullanarak hesaplanır)
    public float GunlukKazanc { get; set; }

    // Navigasyon özelliği
    public Salon Salon { get; set; }
    public ICollection<Randevu> Randevular { get; set; }
}
