namespace Coiffeur_Website.Models
{
    //Verimlilik hesabı yapılacak
    public class Calisan
    {
        public int CalisanId { get; set; }
        public double Verimlilik { get; set; }
        public string UzmanlikAlani{ get; set; }
        public bool MüsaitMi { get; set; }
        public int GunlukKazanc {  get; set; }
    }
}
