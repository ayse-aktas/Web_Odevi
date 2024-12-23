using System.ComponentModel.DataAnnotations;

namespace Coiffeur_Website.Models
{
    public class Musteri
    {
        public int MusteriId { get; set; }
        public string? MusteriAdi { get; set; }
        public string? MusteriSoyadi { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string MusteriMail { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefon numarası geçerli bir formatta olmalıdır. Örnek: +90 (555) 123 4567")]
        public string? TelNo { get; set; }

        // Navigasyon özelliklerini ekle
        public ICollection<Randevu>? Randevular { get; set; }
    }
}
