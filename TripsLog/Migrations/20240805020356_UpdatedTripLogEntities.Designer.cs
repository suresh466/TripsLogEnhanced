﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripsLog.Models;

namespace TripsLog.Migrations
{
    [DbContext(typeof(TripContext))]
    [Migration("20240805020356_UpdatedTripLogEntities")]
    partial class UpdatedTripLogEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripsLog.Models.Accommodation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accommodations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "contact@hotelparis.com",
                            Name = "Hotel Paris",
                            Phone = "123456789"
                        },
                        new
                        {
                            Id = 2,
                            Email = "info@tokyoinn.com",
                            Name = "Tokyo Inn",
                            Phone = "987654321"
                        });
                });

            modelBuilder.Entity("TripsLog.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Activities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Visit Eiffel Tower"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Explore Louvre Museum"
                        },
                        new
                        {
                            Id = 3,
                            Name = "See Statue of Liberty"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Visit Central Park"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Explore Times Square"
                        });
                });

            modelBuilder.Entity("TripsLog.Models.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Destinations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Paris"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tokyo"
                        },
                        new
                        {
                            Id = 3,
                            Name = "New York"
                        });
                });

            modelBuilder.Entity("TripsLog.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccommodationId")
                        .HasColumnType("int");

                    b.Property<int>("DestinationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Trips");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccommodationId = 1,
                            DestinationId = 1,
                            EndDate = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AccommodationId = 2,
                            DestinationId = 2,
                            EndDate = new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            DestinationId = 3,
                            EndDate = new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StartDate = new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("TripsLog.Models.TripActivity", b =>
                {
                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.HasKey("TripId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.ToTable("TripActivity");

                    b.HasData(
                        new
                        {
                            TripId = 1,
                            ActivityId = 1
                        },
                        new
                        {
                            TripId = 1,
                            ActivityId = 2
                        },
                        new
                        {
                            TripId = 3,
                            ActivityId = 3
                        },
                        new
                        {
                            TripId = 3,
                            ActivityId = 4
                        },
                        new
                        {
                            TripId = 3,
                            ActivityId = 5
                        });
                });

            modelBuilder.Entity("TripsLog.Models.Trip", b =>
                {
                    b.HasOne("TripsLog.Models.Accommodation", "Accommodation")
                        .WithMany("Trips")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TripsLog.Models.Destination", "Destination")
                        .WithMany("Trips")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TripsLog.Models.TripActivity", b =>
                {
                    b.HasOne("TripsLog.Models.Activity", "Activity")
                        .WithMany("TripActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripsLog.Models.Trip", "Trip")
                        .WithMany("TripActivities")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
