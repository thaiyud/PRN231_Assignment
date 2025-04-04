﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PEPRN231_SU24_009909_Repo.Models;

public partial class EnglishPremierLeague2024DBContext : DbContext
{
    public EnglishPremierLeague2024DBContext(DbContextOptions<EnglishPremierLeague2024DBContext> options)
        : base(options)
    {
    }
    public EnglishPremierLeague2024DBContext()
    {
    }

    public virtual DbSet<FootballClub> FootballClubs { get; set; }

    public virtual DbSet<FootballPlayer> FootballPlayers { get; set; }

    public virtual DbSet<PremierLeagueAccount> PremierLeagueAccounts { get; set; }
    public static string? GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string? connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FootballClub>(entity =>
        {
            entity.HasKey(e => e.FootballClubId).HasName("PK__Football__91279504070DB488");

            entity.ToTable("FootballClub");

            entity.Property(e => e.FootballClubId)
                .HasMaxLength(30)
                .HasColumnName("FootballClubID");
            entity.Property(e => e.ClubName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ClubShortDescription)
                .IsRequired()
                .HasMaxLength(400);
            entity.Property(e => e.Mascos)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.SoccerPracticeField)
                .IsRequired()
                .HasMaxLength(250);
        });

        modelBuilder.Entity<FootballPlayer>(entity =>
        {
            entity.HasKey(e => e.FootballPlayerId).HasName("PK__Football__6D5466C314CCC9F8");

            entity.ToTable("FootballPlayer");

            entity.Property(e => e.FootballPlayerId)
                .HasMaxLength(30)
                .HasColumnName("FootballPlayerID");
            entity.Property(e => e.Achievements)
                .IsRequired()
                .HasMaxLength(400);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.FootballClubId)
                .HasMaxLength(30)
                .HasColumnName("FootballClubID");
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Nomination)
                .IsRequired()
                .HasMaxLength(400);
            entity.Property(e => e.PlayerExperiences)
                .IsRequired()
                .HasMaxLength(400);

            entity.HasOne(d => d.FootballClub).WithMany(p => p.FootballPlayers)
                .HasForeignKey(d => d.FootballClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__FootballP__Footb__29572725");
        });

        modelBuilder.Entity<PremierLeagueAccount>(entity =>
        {
            entity.HasKey(e => e.AccId).HasName("PK__PremierL__91CBC398F4B3F797");

            entity.ToTable("PremierLeagueAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__PremierL__49A1474085D050CB").IsUnique();

            entity.Property(e => e.AccId)
                .ValueGeneratedNever()
                .HasColumnName("AccID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(140);
            entity.Property(e => e.EmailAddress).HasMaxLength(90);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(90);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}