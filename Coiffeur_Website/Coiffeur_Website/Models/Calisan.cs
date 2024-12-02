namespace Coiffeur_Website.Models
{
    //Verimlilik hesabı yapılacak
    public class Calisan
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public List<CalisanSaat> CalismaSaatleri { get; set; }
        public List<int> IslemTurleri { get; set; } // Çalışanın yapabildiği işlem türlerinin ID'leri
        public double Verimlilik { get; set; }
        public string UzmanlikAlani{ get; set; }
        public int GunlukKazanc {  get; set; }
    }
}
