using Microsoft.EntityFrameworkCore;
using System;

namespace TripsLog.Models
{
    // DbContext class for the FAQs database
    public class TripContext : DbContext
    {
        // Constructor to pass default options to the base class
        public TripContext(DbContextOptions<TripContext> options)
            : base(options)
        { }
        // DbSet properties for the Trip models so CRUD operations can be performed
        public DbSet<Trip> Trips { get; set; }

        // Override the OnModelCreating method to seed data for the Categories, Topics, and FAQs tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Categories
            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 1, Destination = "Paris", StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 10), Accommodation = "Hotel Paris", AccommodationPhone = "123456789", AccommodationEmail = "contact@hotelparis.com", ThingToDo1 = "Visit Eiffel Tower", ThingToDo2 = "Explore Louvre Museum" },
                new Trip { Id = 2, Destination = "Tokyo", StartDate = new DateTime(2024, 2, 15), EndDate = new DateTime(2024, 2, 25), Accommodation = "Tokyo Inn", AccommodationPhone = "987654321", AccommodationEmail = "info@tokyoinn.com" },
                new Trip { Id = 3, Destination = "New York", StartDate = new DateTime(2024, 3, 10), EndDate = new DateTime(2024, 3, 20), ThingToDo1 = "See Statue of Liberty", ThingToDo2 = "Visit Central Park", ThingToDo3 = "Explore Times Square" }
            );
        }
    }

}
