﻿// <auto-generated />
using System;
using Coiffeur_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coiffeur_Website.Migrations
{
    [DbContext(typeof(CoiffeurDbContext))]
    [Migration("20241224153603_modelsUpdatedagain")]
    partial class modelsUpdatedagain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
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

                    b.HasIndex("SalonId");

                    b.ToTable("Calisanlar");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("AdminAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminSoyadi")
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

                    b.HasIndex("SalonId");

                    b.ToTable("Islemler");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Musteri", b =>
                {
                    b.Property<int>("MusteriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MusteriId"), 1L, 1);

                    b.Property<string>("MusteriAdi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MusteriMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MusteriSoyadi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNo")
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

                    b.Property<int>("MusteriId")
                        .HasColumnType("int");

                    b.Property<bool>("OnayDurumu")
                        .HasColumnType("bit");

                    b.Property<string>("RandevuSaati")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RandevuTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("SalonId")
                        .HasColumnType("int");

                    b.HasKey("RandevuId");

                    b.HasIndex("CalisanId");

                    b.HasIndex("IslemId");

                    b.HasIndex("MusteriId");

                    b.HasIndex("SalonId");

                    b.ToTable("Randevular");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Salon", b =>
                {
                    b.Property<int>("SalonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalonId"), 1L, 1);

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

            modelBuilder.Entity("Calisan", b =>
                {
                    b.HasOne("Coiffeur_Website.Models.Salon", "Salon")
                        .WithMany("Calisanlar")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Islem", b =>
                {
                    b.HasOne("Coiffeur_Website.Models.Salon", "Salon")
                        .WithMany("Islemler")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Randevu", b =>
                {
                    b.HasOne("Calisan", "Calisan")
                        .WithMany("Randevular")
                        .HasForeignKey("CalisanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Coiffeur_Website.Models.Islem", "Islem")
                        .WithMany()
                        .HasForeignKey("IslemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Coiffeur_Website.Models.Musteri", "Musteri")
                        .WithMany("Randevular")
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coiffeur_Website.Models.Salon", "Salon")
                        .WithMany("Randevular")
                        .HasForeignKey("SalonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Calisan");

                    b.Navigation("Islem");

                    b.Navigation("Musteri");

                    b.Navigation("Salon");
                });

            modelBuilder.Entity("Calisan", b =>
                {
                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Musteri", b =>
                {
                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("Coiffeur_Website.Models.Salon", b =>
                {
                    b.Navigation("Calisanlar");

                    b.Navigation("Islemler");

                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
