using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Model.Repositories
{
    public class EFTestContext : DbContext
    {
        public static IConfigurationRoot configuration;

        public DbSet<Land> Landen { get; set; }
        public DbSet<Stad> Steden { get; set; }
        public DbSet<Taal> Talen { get; set; }
        public DbSet<LandTaal> LandsTalen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var connectionString = configuration.GetConnectionString("eftest");

            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(
                    connectionString,
                    options => options.MaxBatchSize(150));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Stad
            modelBuilder.Entity<Stad>().ToTable("Steden");
            modelBuilder.Entity<Stad>().HasKey(s => s.StadId);
            modelBuilder.Entity<Stad>().Property(s => s.StadId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Stad>().Property(s => s.Naam)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Stad>().Property(s => s.ISOLandCode)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<Stad>()
                .HasOne(s => s.Land)
                .WithMany(l => l.Steden)
                .HasForeignKey(s => s.ISOLandCode);

            //Taal
            modelBuilder.Entity<Taal>().ToTable("Talen");
            modelBuilder.Entity<Taal>().HasKey(t => t.ISOTaalCode);
            modelBuilder.Entity<Taal>().Property(t => t.ISOTaalCode)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<Taal>().Property(t => t.NaamNL)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Taal>().Property(t => t.NaamTaal)
                .IsRequired()
                .HasMaxLength(50);
            //modelBuilder.Entity<Taal>()
            //    .HasMany(t => t.TaalLanden)
            //    .WithOne(t => t.Taal);

            //LandTaal
            modelBuilder.Entity<LandTaal>().ToTable("LandsTalen");
            modelBuilder.Entity<LandTaal>().HasKey(table => new
            {
                table.ISOLandCode,
                table.ISOTaalCode
            });
            modelBuilder.Entity<LandTaal>().Property(l => l.ISOLandCode)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<LandTaal>().Property(l => l.ISOTaalCode)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<LandTaal>()
                .HasOne(l => l.Land)
                .WithMany(l => l.LandsTalen);
            modelBuilder.Entity<LandTaal>()
                .HasOne(l => l.Taal)
                .WithMany(l => l.TaalLanden);

            //Land
            modelBuilder.Entity<Land>().ToTable("Landen");
            modelBuilder.Entity<Land>().HasKey(l => l.ISOLandCode);
            modelBuilder.Entity<Land>().Property(l => l.ISOLandCode)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<Land>().Property(l => l.NISLandCode)
                .HasMaxLength(3);
            modelBuilder.Entity<Land>().Property(l => l.Naam)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Land>().Property(l => l.AantalInwoners)
                .IsRequired();
            modelBuilder.Entity<Land>().Property(l => l.Oppervlakte)
                .IsRequired();
            modelBuilder.Entity<Land>()
                .HasIndex(l => l.NISLandCode)
                .IsUnique();
        }
    }
}
