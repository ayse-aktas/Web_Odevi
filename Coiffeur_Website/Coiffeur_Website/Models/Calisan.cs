// Verimlilik hesabı yapılacak
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

    // Günlük Verim Hesabı
    public float GunlukVerim { get; private set; }

    // Hesaplama Metodu
    public void HesaplaGunlukVerim(Salon salon, Islem islem)
    {
        if (islem.Ucret > 0 && islem.Sure > 0)
        {
            GunlukKazanc = salon.dolulukSuresi * islem.Ucret;

            // Çalışma süresini saat cinsinden hesapla
            int calismaSuresi = (BitisSaati.Hour * 60 + BitisSaati.Minute) -
                                (BaslangicSaati.Hour * 60 + BaslangicSaati.Minute);

            if (calismaSuresi > 0)
            {
                float toplamIslemSayisi = (float)calismaSuresi / islem.Sure;

                GunlukVerim = (GunlukKazanc * 100) / (toplamIslemSayisi * islem.Ucret);
            }
            else
            {
                GunlukVerim = 0; // Çalışma süresi negatifse verim hesaplanmaz
            }
        }
    }
}
