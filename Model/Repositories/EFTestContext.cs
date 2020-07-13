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

            //Seeding Table Landen
            var oostenrijk = new Land
            {
                ISOLandCode = "AT",
                NISLandCode = "105",
                Naam = "Oostenrijk",
                AantalInwoners = 8754413,
                Oppervlakte = 83871
            };

            var belgie = new Land
            {
                ISOLandCode = "BE",
                NISLandCode = "150",
                Naam = "Belgie",
                AantalInwoners = 11500000,
                Oppervlakte = 30869
            };

            var zwitserland = new Land
            {
                ISOLandCode = "CH",
                NISLandCode = "127",
                Naam = "Zwitserland",
                AantalInwoners = 8236303,
                Oppervlakte = 41285
            };

            var duitsland = new Land
            {
                ISOLandCode = "DE",
                NISLandCode = "103",
                Naam = "Duitsland",
                AantalInwoners = 80594017,
                Oppervlakte = 357022
            };

            var denemarken = new Land
            {
                ISOLandCode = "DK",
                NISLandCode = "108",
                Naam = "Denemarken",
                AantalInwoners = 5605948,
                Oppervlakte = 43094
            };

            var spanje = new Land
            {
                ISOLandCode = "SP",
                NISLandCode = "109",
                Naam = "Spanje",
                AantalInwoners = 48958159,
                Oppervlakte = 505992
            };

            var frankrijk = new Land
            {
                ISOLandCode = "FR",
                NISLandCode = "111",
                Naam = "Frankrijk",
                AantalInwoners = 62814233,
                Oppervlakte = 674843
            };

            var verenigdKoninkrijk = new Land
            {
                ISOLandCode = "GB",
                NISLandCode = "112",
                Naam = "Verenigd Koninkrijk",
                AantalInwoners = 64769452,
                Oppervlakte = 242495
            };

            var italie = new Land
            {
                ISOLandCode = "IT",
                NISLandCode = "128",
                Naam = "Italie",
                AantalInwoners = 62137802,
                Oppervlakte = 300000
            };

            var luxemburg = new Land
            {
                ISOLandCode = "LU",
                NISLandCode = "113",
                Naam = "Luxemburg",
                AantalInwoners = 594130,
                Oppervlakte = 2586
            };

            var nederland = new Land
            {
                ISOLandCode = "NL",
                NISLandCode = "129",
                Naam = "Nederland",
                AantalInwoners = 17424978,
                Oppervlakte = 41873
            };

            var noorwegen = new Land
            {
                ISOLandCode = "NO",
                NISLandCode = "121",
                Naam = "Noorwegen",
                AantalInwoners = 5367580,
                Oppervlakte = 385207
            };

            var polen = new Land
            {
                ISOLandCode = "PL",
                NISLandCode = "139",
                Naam = "Polen",
                AantalInwoners = 38476269,
                Oppervlakte = 311888
            };

            var portugal = new Land
            {
                ISOLandCode = "PT",
                NISLandCode = "123",
                Naam = "Portugal",
                AantalInwoners = 10839541,
                Oppervlakte = 92212
            };

            var zweden = new Land
            {
                ISOLandCode = "SE",
                NISLandCode = "126",
                Naam = "Zweden",
                AantalInwoners = 9960487,
                Oppervlakte = 450295
            };

            var verenigdeStaten = new Land
            {
                ISOLandCode = "US",
                NISLandCode = "402",
                Naam = "Verenigde Staten",
                AantalInwoners = 326625791,
                Oppervlakte = 9826675
            };

            modelBuilder.Entity<Land>().HasData(
                oostenrijk, belgie, zwitserland, duitsland, denemarken, spanje,
                frankrijk, verenigdKoninkrijk, italie, luxemburg, nederland,
                noorwegen, polen, portugal, zweden, verenigdeStaten);

            //Seeding Table Talen
            var bg = new Taal
            {
                ISOTaalCode = "bg",
                NaamNL = "Bulgaars",
                NaamTaal = "български"
            };

            var cs = new Taal
            {
                ISOTaalCode = "cs",
                NaamNL = "Tsjechisch",
                NaamTaal = "čeština"
            };

            var da = new Taal
            {
                ISOTaalCode = "da",
                NaamNL = "Deens",
                NaamTaal = "dansk"
            };

            var de = new Taal
            {
                ISOTaalCode = "de",
                NaamNL = "Duits",
                NaamTaal = "Deutsch"
            };

            var el = new Taal
            {
                ISOTaalCode = "el",
                NaamNL = "Grieks",
                NaamTaal = "ελληνικά"
            };

            var en = new Taal
            {
                ISOTaalCode = "en",
                NaamNL = "Engels",
                NaamTaal = "English"
            };

            var es = new Taal
            {
                ISOTaalCode = "es",
                NaamNL = "Spaans",
                NaamTaal = "español"
            };

            var et = new Taal
            {
                ISOTaalCode = "et",
                NaamNL = "Ests",
                NaamTaal = "eesti keel"
            };

            var fi = new Taal
            {
                ISOTaalCode = "fi",
                NaamNL = "Fins",
                NaamTaal = "suomi"
            };

            var fr = new Taal
            {
                ISOTaalCode = "fr",
                NaamNL = "Frans",
                NaamTaal = "français"
            };

            var ga = new Taal
            {
                ISOTaalCode = "ga",
                NaamNL = "Iers",
                NaamTaal = "Gaeilge"
            };

            var hu = new Taal
            {
                ISOTaalCode = "hu",
                NaamNL = "Hongaars",
                NaamTaal = "magyar"
            };

            var it = new Taal
            {
                ISOTaalCode = "it",
                NaamNL = "Italiaans",
                NaamTaal = "italiano"
            };

            var lt = new Taal
            {
                ISOTaalCode = "lt",
                NaamNL = "Litouws",
                NaamTaal = "lietuvių kalba"
            };

            var lv = new Taal
            {
                ISOTaalCode = "lv",
                NaamNL = "Lets",
                NaamTaal = "latviešu valoda"
            };

            var mt = new Taal
            {
                ISOTaalCode = "mt",
                NaamNL = "Maltees",
                NaamTaal = "malti"
            };

            var nl = new Taal
            {
                ISOTaalCode = "nl",
                NaamNL = "Nederlands",
                NaamTaal = "Nederlands"
            };

            var pl = new Taal
            {
                ISOTaalCode = "pl",
                NaamNL = "Pools",
                NaamTaal = "polski"
            };

            var pt = new Taal
            {
                ISOTaalCode = "pt",
                NaamNL = "Portugees",
                NaamTaal = "português"
            };

            var ro = new Taal
            {
                ISOTaalCode = "ro",
                NaamNL = "Roemeens",
                NaamTaal = "română"
            };

            var sk = new Taal
            {
                ISOTaalCode = "sk",
                NaamNL = "Slovaaks",
                NaamTaal = "slovenčina"
            };

            var sl = new Taal
            {
                ISOTaalCode = "sl",
                NaamNL = "Sloveens",
                NaamTaal = "slovenščina"
            };

            var sv = new Taal
            {
                ISOTaalCode = "sv",
                NaamNL = "Zweeds",
                NaamTaal = "svenska"
            };

            modelBuilder.Entity<Taal>().HasData(
                bg, cs, da, de, el, en, es, et, fi, fr, ga, hu, it, lt, lv,
                mt, nl, pl, pt, ro, sk, sl, sv);

            //Seeding Table Steden
            modelBuilder.Entity<Stad>().HasData(
                new 
                {
                    StadId = 1,
                    Naam = "Brussel",
                    ISOLandCode = "BE"
                },
                new
                {
                    StadId = 2,
                    Naam = "Antwerpen",
                    ISOLandCode = "BE"
                },
                new
                {
                    StadId = 3,
                    Naam = "Luik",
                    ISOLandCode = "BE"
                },
                new
                {
                    StadId = 4,
                    Naam = "Amsterdam",
                    ISOLandCode = "NL"
                },
                new
                {
                    StadId = 5,
                    Naam = "Den Haag",
                    ISOLandCode = "NL"
                },
                new
                {
                    StadId = 6,
                    Naam = "Rotterdam",
                    ISOLandCode = "NL"
                },
                new
                {
                    StadId = 7,
                    Naam = "Berlijn",
                    ISOLandCode = "DE"
                },
                new
                {
                    StadId = 8,
                    Naam = "Hamburg",
                    ISOLandCode = "DE"
                },
                new
                {
                    StadId = 9,
                    Naam = "Munchen",
                    ISOLandCode = "DE"
                },
                new
                {
                    StadId = 10,
                    Naam = "Luxemburg",
                    ISOLandCode = "LU"
                },
                new
                {
                    StadId = 11,
                    Naam = "Parijs",
                    ISOLandCode = "FR"
                },
                new
                {
                    StadId = 12,
                    Naam = "Marseille",
                    ISOLandCode = "FR"
                },
                new
                {
                    StadId = 13,
                    Naam = "Lyon",
                    ISOLandCode = "FR"
                });

            //Seeding Table LandsTalen
            modelBuilder.Entity<LandTaal>().HasData(
                new
                {
                    ISOLandCode = "BE",
                    ISOTaalCode = "de"
                },
                new
                {
                    ISOLandCode = "BE",
                    ISOTaalCode = "nl"
                },
                new
                {
                    ISOLandCode = "BE",
                    ISOTaalCode = "fr"
                },
                new
                {
                    ISOLandCode = "DE",
                    ISOTaalCode = "de"
                },
                new
                {
                    ISOLandCode = "LU",
                    ISOTaalCode = "de"
                },
                new
                {
                    ISOLandCode = "FR",
                    ISOTaalCode = "fr"
                },
                new
                {
                    ISOLandCode = "LU",
                    ISOTaalCode = "fr"
                },
                new
                {
                    ISOLandCode = "NL",
                    ISOTaalCode = "nl"
                });
        }
    }
}
