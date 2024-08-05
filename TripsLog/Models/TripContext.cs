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

        // Override the OnModelCreating method to configure relationships and seed data
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
                new Destination { Id = 1, Name = "Paris" },
                new Destination { Id = 2, Name = "Tokyo" },
                new Destination { Id = 3, Name = "New York" }
            );

            // Seed data for Accommodations
            modelBuilder.Entity<Accommodation>().HasData(
                new Accommodation { Id = 1, Name = "Hotel Paris", Phone = "123456789", Email = "contact@hotelparis.com" },
                new Accommodation { Id = 2, Name = "Tokyo Inn", Phone = "987654321", Email = "info@tokyoinn.com" }
            );

            // Seed data for Activities
            modelBuilder.Entity<Activity>().HasData(
                new Activity { Id = 1, Name = "Visit Eiffel Tower" },
                new Activity { Id = 2, Name = "Explore Louvre Museum" },
                new Activity { Id = 3, Name = "See Statue of Liberty" },
                new Activity { Id = 4, Name = "Visit Central Park" },
                new Activity { Id = 5, Name = "Explore Times Square" }
            );

            // Seed data for Trips
            modelBuilder.Entity<Trip>().HasData(
                new Trip
                {
                    Id = 1,
                    DestinationId = 1,
                    StartDate = new DateTime(2024, 1, 1),
                    EndDate = new DateTime(2024, 1, 10),
                    AccommodationId = 1
                },
                new Trip
                {
                    Id = 2,
                    DestinationId = 2,
                    StartDate = new DateTime(2024, 2, 15),
                    EndDate = new DateTime(2024, 2, 25),
                    AccommodationId = 2
                },
                new Trip
                {
                    Id = 3,
                    DestinationId = 3,
                    StartDate = new DateTime(2024, 3, 10),
                    EndDate = new DateTime(2024, 3, 20)
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