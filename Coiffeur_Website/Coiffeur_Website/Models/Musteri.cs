using System.ComponentModel.DataAnnotations;

namespace Coiffeur_Website.Models
{
    public class Musteri
    {

        public int MusteriId { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public string Sifre { get; set; }
        public string MusteriMail { get; set; }
        public string FotografAdres {  get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefon numarası geçerli bir formatta olmalıdır. Örnek: +90 (555) 123 4567")]
        [Display(Name = "Telefon Numarası")]
        public string TelNo { get; set; }

    }
}
