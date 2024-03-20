﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using wshop3.Datab;
using wshop3.Model;

#nullable disable

namespace wshop3.Migrations
{
    [DbContext(typeof(Wshop3Context))]
    [Migration("20240304145352_teszt1")]
    partial class teszt1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_hungarian_ci")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("wshop3.Model.Felhasznalok", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Jelszo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("jelszo");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nev");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("felhasznalok", (string)null);
                });

            modelBuilder.Entity("wshop3.Model.Kategoriak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nev");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.ToTable("kategoriak", (string)null);
                });

            modelBuilder.Entity("wshop3.Model.Kosar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FelhasznaloId")
                        .HasColumnType("int(11)")
                        .HasColumnName("felhasznalo_id");

                    b.Property<int?>("Mennyiseg")
                        .HasColumnType("int(11)")
                        .HasColumnName("mennyiseg");

                    b.Property<int?>("TermekId")
                        .HasColumnType("int(11)")
                        .HasColumnName("termek_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "FelhasznaloId" }, "felhasznalo_id");

                    b.HasIndex(new[] { "TermekId" }, "termek_id");

                    b.ToTable("kosar", (string)null);
                });

            modelBuilder.Entity("wshop3.Model.Rendelesek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FelhasznaloId")
                        .HasColumnType("int(11)")
                        .HasColumnName("felhasznalo_id");

                    b.Property<int?>("Mennyiseg")
                        .HasColumnType("int(11)")
                        .HasColumnName("mennyiseg");

                    b.Property<int?>("TermekId")
                        .HasColumnType("int(11)")
                        .HasColumnName("termek_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "FelhasznaloId" }, "felhasznalo_id")
                        .HasDatabaseName("felhasznalo_id1");

                    b.HasIndex(new[] { "TermekId" }, "termek_id")
                        .HasDatabaseName("termek_id1");

                    b.ToTable("rendelesek", (string)null);
                });

            modelBuilder.Entity("wshop3.Model.TermekKepek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Kep")
                        .HasColumnType("longblob")
                        .HasColumnName("kep");

                    b.Property<int?>("TermekId")
                        .HasColumnType("int(11)")
                        .HasColumnName("termek_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "TermekId" }, "termek_id")
                        .HasDatabaseName("termek_id2");

                    b.ToTable("termek_kepek", (string)null);
                });

            modelBuilder.Entity("wshop3.Model.Termekek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Ar")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("ar");

                    b.Property<int?>("KategoriaId")
                        .HasColumnType("int(11)")
                        .HasColumnName("kategoria_id");

                    b.Property<string>("Leiras")
                        .HasColumnType("text")
                        .HasColumnName("leiras");

                    b.Property<string>("Nev")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nev");

                    b.Property<int?>("TermekKepId")
                        .HasColumnType("int(11)")
                        .HasColumnName("termek_kep_id");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "KategoriaId" }, "kategoria_id");

                    b.HasIndex(new[] { "TermekKepId" }, "termek_kep_id");

                    b.ToTable("termekek", (string)null);
                });

            modelBuilder.Entity("wshop3.Model.Kosar", b =>
                {
                    b.HasOne("wshop3.Model.Felhasznalok", "Felhasznalo")
                        .WithMany("Kosars")
                        .HasForeignKey("FelhasznaloId")
                        .HasConstraintName("kosar_ibfk_1");

                    b.HasOne("wshop3.Model.Termekek", "Termek")
                        .WithMany("Kosars")
                        .HasForeignKey("TermekId")
                        .HasConstraintName("kosar_ibfk_2");

                    b.Navigation("Felhasznalo");

                    b.Navigation("Termek");
                });

            modelBuilder.Entity("wshop3.Model.Rendelesek", b =>
                {
                    b.HasOne("wshop3.Model.Felhasznalok", "Felhasznalo")
                        .WithMany("Rendeleseks")
                        .HasForeignKey("FelhasznaloId")
                        .HasConstraintName("rendelesek_ibfk_1");

                    b.HasOne("wshop3.Model.Termekek", "Termek")
                        .WithMany("Rendeleseks")
                        .HasForeignKey("TermekId")
                        .HasConstraintName("rendelesek_ibfk_2");

                    b.Navigation("Felhasznalo");

                    b.Navigation("Termek");
                });

            modelBuilder.Entity("wshop3.Model.Termekek", b =>
                {
                    b.HasOne("wshop3.Model.Kategoriak", "Kategoria")
                        .WithMany("Termekeks")
                        .HasForeignKey("KategoriaId")
                        .HasConstraintName("termekek_ibfk_1");

                    b.HasOne("wshop3.Model.TermekKepek", "TermekKep")
                        .WithMany("Termekeks")
                        .HasForeignKey("TermekKepId")
                        .HasConstraintName("termekek_ibfk_2");

                    b.Navigation("Kategoria");

                    b.Navigation("TermekKep");
                });

            modelBuilder.Entity("wshop3.Model.Felhasznalok", b =>
                {
                    b.Navigation("Kosars");

                    b.Navigation("Rendeleseks");
                });

            modelBuilder.Entity("wshop3.Model.Kategoriak", b =>
                {
                    b.Navigation("Termekeks");
                });

            modelBuilder.Entity("wshop3.Model.TermekKepek", b =>
                {
                    b.Navigation("Termekeks");
                });

            modelBuilder.Entity("wshop3.Model.Termekek", b =>
                {
                    b.Navigation("Kosars");

                    b.Navigation("Rendeleseks");
                });
#pragma warning restore 612, 618
        }
    }
}
