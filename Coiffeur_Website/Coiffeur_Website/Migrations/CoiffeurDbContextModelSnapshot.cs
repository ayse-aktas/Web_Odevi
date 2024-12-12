﻿// <auto-generated />
using System;
using Coiffeur_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coiffeur_Website.Migrations
{
    [DbContext(typeof(CoiffeurDbContext))]
    partial class CoiffeurDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Calisan", b =>
                {
                    b.Property<int>("CalisanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalisanId"), 1L, 1);

                    b.Property<DateTime>("BaslangicSaati")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("BitisSaati")
                        .HasColumnType("datetime2");

                    b.Property<string>("CalisanAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CalisanSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GunlukKazanc")
                        .HasColumnType("real");

                    b.Property<float>("GunlukVerim")
                        .HasColumnType("real");

                    b.Property<int>("Maas")
                        .HasColumnType("int");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<int>("TopCalismaSaati")
                        .HasColumnType("int");

                    b.Property<string>("UzmanlikAlani")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CalisanId");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("AdminAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminSoyadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Adminler");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Islem", b =>
                {
                    b.Property<int>("IslemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IslemId"), 1L, 1);

                    b.Property<string>("IslemAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.Property<int>("Sure")
                        .HasColumnType("int");

                    b.Property<int>("Ucret")
                        .HasColumnType("int");

                    b.HasKey("IslemId");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Musteri", b =>
                {
                    b.Property<int>("MusteriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MusteriId"), 1L, 1);

                    b.Property<string>("FotografAdres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MusteriAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MusteriMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MusteriSoyadi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MusteriId");

                    b.ToTable("Musteriler");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuId"), 1L, 1);

                    b.Property<int>("CalisanId")
                        .HasColumnType("int");

                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RandevuTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("RandevuId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Salon", b =>
                {
                    b.Property<int>("SalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalonId"), 1L, 1);

                    b.Property<int>("IslemId")
                        .HasColumnType("int");

                    b.Property<string>("SalonAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("doluluk")
                        .HasColumnType("bit");

                    b.Property<int>("dolulukSuresi")
                        .HasColumnType("int");

                    b.HasKey("SalonId");

                    b.ToTable("Salonlar");
                });
#pragma warning restore 612, 618
        }
    }
}
