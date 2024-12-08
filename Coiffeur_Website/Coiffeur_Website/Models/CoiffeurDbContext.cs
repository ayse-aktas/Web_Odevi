using Coiffeur_Website.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Coiffeur_Website.Data
{
    public class CoiffeurDbContext : DbContext
    {

        // Modelleri DbSet olarak ekleyin
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Salon> Salonlar { get; set; }

        //Veri tabanına bağlanma:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Coiffeur;Trusted_Connection=True;");
        }
    }
}
