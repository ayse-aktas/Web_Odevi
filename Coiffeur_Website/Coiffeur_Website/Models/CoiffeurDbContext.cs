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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Randevu - Çalışan ilişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Calisan)
                .WithMany(c => c.Randevular)
                .HasForeignKey(r => r.CalisanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Randevu - İşlem ilişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Islem)
                .WithMany()
                .HasForeignKey(r => r.IslemId)
                .OnDelete(DeleteBehavior.Restrict);

            // Randevu - Salon ilişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Salon)
                .WithMany(s => s.Randevular)
                .HasForeignKey(r => r.SalonId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
