using Microsoft.EntityFrameworkCore;
using System;

namespace TripsLog.Models
{
    // DbContext class for the Trips database
    public class TripContext : DbContext
    {
        // Constructor to pass default options to the base class
        public TripContext(DbContextOptions<TripContext> options)
            : base(options)
        { }

        // DbSet properties for the models so CRUD operations can be performed
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<TripActivity> TripActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Trip and Activity
            modelBuilder.Entity<TripActivity>()
                .HasKey(ta => new { ta.TripId, ta.ActivityId });

            modelBuilder.Entity<TripActivity>()
                .HasOne(ta => ta.Trip)
                .WithMany(t => t.TripActivities)
                .HasForeignKey(ta => ta.TripId);

            modelBuilder.Entity<TripActivity>()
                .HasOne(ta => ta.Activity)
                .WithMany(a => a.TripActivities)
                .HasForeignKey(ta => ta.ActivityId);

            // Configure cascading delete for Accommodation (set to null when deleted)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Accommodation)
                .WithMany(a => a.Trips)
                .HasForeignKey(t => t.AccommodationId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure cascading delete for Destination (restrict deletion if trips exist)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.Destination)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data for Destinations
            modelBuilder.Entity<Destination>().HasData(
                new Destination { Id = 1, Name = "India" },
                new Destination { Id = 2, Name = "Nepal" }
            );

            // Seed data for Accommodations
            modelBuilder.Entity<Accommodation>().HasData(
                new Accommodation { Id = 1, Name = "Hotel Taj", Phone = "123456789", Email = "contact@hoteltaj.com" },
                new Accommodation { Id = 2, Name = "Hotel Opera", Phone = "987654321", Email = "info@hotelopera.com" }
            );

            // Seed data for Activities
            modelBuilder.Entity<Activity>().HasData(
                new Activity { Id = 1, Name = "Visit Taj Mahal" },
                new Activity { Id = 2, Name = "Go To India Gate" },
                new Activity { Id = 3, Name = "Visit Pokhara" },
                new Activity { Id = 4, Name = "Climb Mt Everest" },
                new Activity { Id = 5, Name = "Go Trekking" }
            );

            // Seed data for Trips
            modelBuilder.Entity<Trip>().HasData(
                new Trip
                {
                    Id = 1,
                    DestinationId = 1,
                    StartDate = new DateTime(2024, 8, 4),
                    EndDate = new DateTime(2024, 8, 28),
                    AccommodationId = 1
                },
                new Trip
                {
                    Id = 2,
                    DestinationId = 2,
                    StartDate = new DateTime(2024, 8, 15),
                    EndDate = new DateTime(2024, 8, 30),
                    AccommodationId = 2
                },
                new Trip
                {
                    Id = 3,
                    DestinationId = 2,
                    StartDate = new DateTime(2024, 8, 20),
                    EndDate = new DateTime(2024, 9, 20)
                }
            );

            // Seed data for TripActivities (linking trips with activities)
            modelBuilder.Entity<TripActivity>().HasData(
                new TripActivity { TripId = 1, ActivityId = 1 },
                new TripActivity { TripId = 1, ActivityId = 2 },
                new TripActivity { TripId = 3, ActivityId = 3 },
                new TripActivity { TripId = 3, ActivityId = 4 },
                new TripActivity { TripId = 3, ActivityId = 5 }
            );
        }
    }
}