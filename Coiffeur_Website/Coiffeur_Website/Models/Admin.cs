using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Coiffeur_Website.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string? AdminAdi { get; set; }
        public string? AdminSoyadi { get; set; }


        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string AdminMail { get; set; }

    }
}
