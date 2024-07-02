﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IND.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IND.klasser.Betyg", b =>
                {
                    b.Property<int>("BetygID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BetygID"));

                    b.Property<string>("BetygValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("ElevID")
                        .HasColumnType("int");

                    b.Property<int>("KursID")
                        .HasColumnType("int");

                    b.Property<int>("LarareID")
                        .HasColumnType("int");

                    b.HasKey("BetygID");

                    b.HasIndex("ElevID");

                    b.HasIndex("KursID");

                    b.HasIndex("LarareID");

                    b.ToTable("Betygs");
                });

            modelBuilder.Entity("IND.klasser.Elev", b =>
                {
                    b.Property<int>("ElevID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElevID"));

                    b.Property<string>("Klass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ElevID");

                    b.ToTable("Elevers");
                });

            modelBuilder.Entity("IND.klasser.ElevInfo", b =>
                {
                    b.Property<string>("BetygValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ElevID")
                        .HasColumnType("int");

                    b.Property<string>("Klass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KursNamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LarareNamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LonBelopp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("ElevInfos");
                });

            modelBuilder.Entity("IND.klasser.Kurs", b =>
                {
                    b.Property<int>("KursID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KursID"));

                    b.Property<bool>("Aktiv")
                        .HasColumnType("bit");

                    b.Property<string>("KursNamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KursID");

                    b.ToTable("Kursers");
                });

            modelBuilder.Entity("IND.klasser.Lon", b =>
                {
                    b.Property<int>("LonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LonID"));

                    b.Property<string>("Avdelning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LonBelopp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("LonID");

                    b.HasIndex("PersonID");

                    b.ToTable("Lons");
                });

            modelBuilder.Entity("IND.klasser.Personal", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<DateTime>("AnstallningsDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Befattning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("Personals");
                });

            modelBuilder.Entity("IND.klasser.Betyg", b =>
                {
                    b.HasOne("IND.klasser.Elev", "Elev")
                        .WithMany("Betyg")
                        .HasForeignKey("ElevID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IND.klasser.Kurs", "Kurs")
                        .WithMany("Betyg")
                        .HasForeignKey("KursID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IND.klasser.Personal", "Larare")
                        .WithMany("Betyg")
                        .HasForeignKey("LarareID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Elev");

                    b.Navigation("Kurs");

                    b.Navigation("Larare");
                });

            modelBuilder.Entity("IND.klasser.Lon", b =>
                {
                    b.HasOne("IND.klasser.Personal", "Personal")
                        .WithMany("Lons")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("IND.klasser.Elev", b =>
                {
                    b.Navigation("Betyg");
                });

            modelBuilder.Entity("IND.klasser.Kurs", b =>
                {
                    b.Navigation("Betyg");
                });

            modelBuilder.Entity("IND.klasser.Personal", b =>
                {
                    b.Navigation("Betyg");

                    b.Navigation("Lons");
                });
#pragma warning restore 612, 618
        }
    }
}
