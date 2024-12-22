using Coiffeur_Website.Models;
using Microsoft.EntityFrameworkCore;

namespace Coiffeur_Website.Data
{
    public class CoiffeurDbContext : DbContext
    {
        public CoiffeurDbContext(DbContextOptions<CoiffeurDbContext> options)
            : base(options)
        {
        }

        // DbSet tanımları
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Salon> Salonlar { get; set; }
    }
}
