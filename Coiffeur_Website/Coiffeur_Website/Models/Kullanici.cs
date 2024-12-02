using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Coiffeur_Website.Models
{
    //musteri
    //Uyelik işlemi yapabilir.
    //Giriş çıkış yapabilir.
    //randevu oluşturabilir.

    public class Kullanici
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad maksimum 50 karakter olmalıdır.")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Ad sadece harflerden oluşmalıdır.")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad maksimum 50 karakter olmalıdır.")]
        [RegularExpression(@"^[a-zA-ZğüşöçıİĞÜŞÖÇ]+$", ErrorMessage = "Soyad sadece harflerden oluşmalıdır.")]
        [Display(Name = "Soyad")]
        public string  Soyad { get; set; }

        // Kullanıcı adı (Dinamik olarak Ad ve Soyad birleşiminden oluşturulur)
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi => $"{Ad} {Soyad}"; // Getter ile dinamik olarak oluşturulur.

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefon numarası geçerli bir formatta olmalıdır. Örnek: +90 (555) 123 4567")]
        [Display(Name = "Telefon Numarası")]
        public string TelNo { get; set; }
        public string Sifre { get; set; }

 
    }
}
