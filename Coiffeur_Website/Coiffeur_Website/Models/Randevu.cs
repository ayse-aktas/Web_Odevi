using System.ComponentModel.DataAnnotations;

namespace Coiffeur_Website.Models
{
    public class Randevu
    {
        public int Id { get; set; }

        // İlgili kullanıcının randevu aldığı tarih
        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Randevu Tarihi")]
        public DateTime RandevuTarihi { get; set; }

        // İlgili çalışan
        [Required(ErrorMessage = "Çalışan seçilmelidir.")]
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }

        // İlgili işlem
        [Required(ErrorMessage = "İşlem seçilmelidir.")]
        public int IslemId { get; set; }
        public Islem Islem { get; set; }

        // Randevu onay durumu
        public bool OnayDurumu { get; set; }

        // Randevunun onaylanıp onaylanmadığını belirten bir özellik
        [Display(Name = "Randevu Onayı")]
        public string OnayDurumuText
        {
            get
            {
                return OnayDurumu ? "Onaylı" : "Onaysız";
            }
        }

        // Randevunun ücretini tutan özellik
        public int Ucret { get; set; }

        // Randevu süresi (dakika cinsinden)
        public int Sure { get; set; }
    }
}
