﻿// <auto-generated />
using System;
using Gestion_Municipalites.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gestion_Municipalites.Migrations
{
    [DbContext(typeof(MunicipaliteContext))]
    partial class MunicipaliteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gestion_Municipalites.Models.Election", b =>
                {
                    b.Property<int>("ElectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElectionId"));

                    b.Property<int>("CodeGeographique")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateElections")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MunicipaliteId")
                        .HasColumnType("int");

                    b.HasKey("ElectionId");

                    b.HasIndex("MunicipaliteId");

                    b.ToTable("Elections");
                });

            modelBuilder.Entity("Gestion_Municipalites.Models.Municipalite", b =>
                {
                    b.Property<int>("MunicipaliteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MunicipaliteId"));

                    b.Property<bool>("Actif")
                        .HasColumnType("bit");

                    b.Property<string>("AdresseCourriel")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AdresseWeb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateProchaineElection")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomMunicipalite")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MunicipaliteId");

                    b.ToTable("Municipalites");
                });

            modelBuilder.Entity("Gestion_Municipalites.Models.Election", b =>
                {
                    b.HasOne("Gestion_Municipalites.Models.Municipalite", null)
                        .WithMany("Elections")
                        .HasForeignKey("MunicipaliteId");
                });

            modelBuilder.Entity("Gestion_Municipalites.Models.Municipalite", b =>
                {
                    b.Navigation("Elections");
                });
#pragma warning restore 612, 618
        }
    }
}
