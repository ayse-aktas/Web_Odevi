using System.Collections.Generic;

namespace Coiffeur_Website.Models
{
    public class Salon
    {
        public int SalonId { get; set; }//PK
        public int IslemId {  get; set; }//FK
        public string SalonAd { get; set; }
        public bool doluluk { get; set; }
        public int dolulukSuresi { get; set; }// (doluluk=1 olma sayısı)*işlemsüresi
    }

}
