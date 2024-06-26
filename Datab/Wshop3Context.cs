﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using wshop3.Model;

namespace wshop3.Datab;

public partial class Wshop3Context : DbContext
{
    public Wshop3Context()
    {
    }

    public Wshop3Context(DbContextOptions<Wshop3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Felhasznalok> Felhasznaloks { get; set; }

    public virtual DbSet<Kategoriak> Kategoriaks { get; set; }

    public virtual DbSet<RendelesTermek> RendelesTermeks { get; set; }

    public virtual DbSet<Rendelesek> Rendeleseks { get; set; }

    public virtual DbSet<TermekKepek> TermekKepeks { get; set; }

    public virtual DbSet<Termekek> Termekeks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Felhasznalok>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("felhasznalok");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Jelszo)
                .HasMaxLength(100)
                .HasColumnName("jelszo");
            entity.Property(e => e.Nev)
                .HasMaxLength(50)
                .HasColumnName("nev");
        });

        modelBuilder.Entity<Kategoriak>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kategoriak");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nev)
                .HasMaxLength(50)
                .HasColumnName("nev");
        });

        modelBuilder.Entity<RendelesTermek>(entity =>
        {
            entity.HasKey(e => new { e.RendelesId, e.TermekId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("rendeles_termek");

            entity.HasIndex(e => e.TermekId, "termek_id");

            entity.Property(e => e.RendelesId)
                .HasColumnType("int(11)")
                .HasColumnName("rendeles_id");
            entity.Property(e => e.TermekId)
                .HasColumnType("int(11)")
                .HasColumnName("termek_id");
            entity.Property(e => e.Mennyiseg)
                .HasColumnType("int(11)")
                .HasColumnName("mennyiseg");

            entity.HasOne(d => d.Rendeles).WithMany(p => p.RendelesTermeks)
                .HasForeignKey(d => d.RendelesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rendeles_termek_ibfk_1");

            entity.HasOne(d => d.Termek).WithMany(p => p.RendelesTermeks)
                .HasForeignKey(d => d.TermekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("rendeles_termek_ibfk_2");
        });

        modelBuilder.Entity<Rendelesek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rendelesek");

            entity.HasIndex(e => e.FelhasznaloId, "felhasznalo_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.FelhasznaloId)
                .HasColumnName("felhasznalo_id")
                .UseCollation("utf8mb4_general_ci");
            entity.Property(e => e.RendelesIdeje)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("rendeles_ideje");
        });

        modelBuilder.Entity<TermekKepek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("termek_kepek");

            entity.HasIndex(e => e.TermekId, "termek_id2");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Kep).HasColumnName("kep");
            entity.Property(e => e.TermekId)
                .HasColumnType("int(11)")
                .HasColumnName("termek_id");
        });

        modelBuilder.Entity<Termekek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("termekek");

            entity.HasIndex(e => e.KategoriaId, "kategoria_id");

            entity.HasIndex(e => e.TermekKepId, "termek_kep_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Ar)
                .HasPrecision(10, 2)
                .HasColumnName("ar");
            entity.Property(e => e.KategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("kategoria_id");
            entity.Property(e => e.Leiras)
                .HasColumnType("text")
                .HasColumnName("leiras");
            entity.Property(e => e.Nev)
                .HasMaxLength(100)
                .HasColumnName("nev");
            entity.Property(e => e.TermekKepId)
                .HasColumnType("int(11)")
                .HasColumnName("termek_kep_id");

            entity.HasOne(d => d.Kategoria).WithMany(p => p.Termekeks)
                .HasForeignKey(d => d.KategoriaId)
                .HasConstraintName("termekek_ibfk_1");

            entity.HasOne(d => d.TermekKep).WithMany(p => p.Termekeks)
                .HasForeignKey(d => d.TermekKepId)
                .HasConstraintName("termekek_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
