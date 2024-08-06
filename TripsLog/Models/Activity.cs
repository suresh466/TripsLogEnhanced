using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TripsLog.Models
{
    public class Activity
    {
        public int Id { get; set; }

        // Activity name is required and cannot be longer than 100 characters
        [Required(ErrorMessage = "Activity name is required.")]
        [StringLength(100, ErrorMessage = "Activity name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<TripActivity> TripActivities { get; set; }
    }
}