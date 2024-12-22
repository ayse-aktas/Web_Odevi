//Günlük verim tablosu olcak
using Coiffeur_Website.Models;

public class Calisan
{
    public int CalisanId { get; set; } // PK
    public int SalonId { get; set; } // FK
    public string CalisanAd { get; set; }
    public string CalisanSoyad { get; set; }
    public string UzmanlikAlani { get; set; }
    public DateTime BaslangicSaati { get; set; }
    public DateTime BitisSaati { get; set; } // TimeOnly olarak düzenlendi
    public int Maas { get; set; } 
    public int TopCalismaSaati { get; set; }

    // Günlük Kazanç (Salon'daki doluluk oranını kullanarak hesaplanır)
    public float GunlukKazanc { get; set; }
}
