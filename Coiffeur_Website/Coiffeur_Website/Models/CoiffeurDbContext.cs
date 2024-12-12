using Coiffeur_Website.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Coiffeur_Website.Data
{
    public class CoiffeurDbContext : DbContext
    {
<<<<<<< HEAD
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Coiffeur2;Trusted_Connection=True;");
        }

=======
        public CoiffeurDbContext(DbContextOptions<CoiffeurDbContext> options)
        : base(options)
        {
        }
>>>>>>> bab913c3b6664fcaadc52ec8997307b95b6d2a85
        // Modelleri DbSet olarak ekleyin
        public DbSet<Admin> Adminler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Islem> Islemler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Salon> Salonlar { get; set; }
        //options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        //Veri tabanına bağlanma:
<<<<<<< HEAD
=======
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=Coiffeur2;Trusted_Connection=True;");
        }
>>>>>>> bab913c3b6664fcaadc52ec8997307b95b6d2a85
    }
}
