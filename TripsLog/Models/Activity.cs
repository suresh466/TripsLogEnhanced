using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TripsLog.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Activity name is required.")]
        [StringLength(100, ErrorMessage = "Activity name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<TripActivity> TripActivities { get; set; }
    }
}