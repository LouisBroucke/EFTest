﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Repositories;

namespace Model.Migrations
{
    [DbContext(typeof(EFTestContext))]
    partial class EFTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Entities.Land", b =>
                {
                    b.Property<string>("ISOLandCode")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<int>("AantalInwoners")
                        .HasColumnType("int");

                    b.Property<string>("NISLandCode")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<float>("Oppervlakte")
                        .HasColumnType("real");

                    b.HasKey("ISOLandCode");

                    b.HasIndex("NISLandCode")
                        .IsUnique()
                        .HasFilter("[NISLandCode] IS NOT NULL");

                    b.ToTable("Landen");
                });

            modelBuilder.Entity("Model.Entities.LandTaal", b =>
                {
                    b.Property<string>("ISOLandCode")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("ISOTaalCode")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("ISOLandCode", "ISOTaalCode");

                    b.HasIndex("ISOTaalCode");

                    b.ToTable("LandsTalen");
                });

            modelBuilder.Entity("Model.Entities.Stad", b =>
                {
                    b.Property<int>("StadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ISOLandCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("StadId");

                    b.HasIndex("ISOLandCode");

                    b.ToTable("Steden");
                });

            modelBuilder.Entity("Model.Entities.Taal", b =>
                {
                    b.Property<string>("ISOTaalCode")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("NaamNL")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NaamTaal")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ISOTaalCode");

                    b.ToTable("Talen");
                });

            modelBuilder.Entity("Model.Entities.LandTaal", b =>
                {
                    b.HasOne("Model.Entities.Land", "Land")
                        .WithMany("LandsTalen")
                        .HasForeignKey("ISOLandCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Taal", "Taal")
                        .WithMany("TaalLanden")
                        .HasForeignKey("ISOTaalCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Entities.Stad", b =>
                {
                    b.HasOne("Model.Entities.Land", "Land")
                        .WithMany("Steden")
                        .HasForeignKey("ISOLandCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
