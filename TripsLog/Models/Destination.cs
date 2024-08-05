using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TripsLog.Models
{
    public class Destination
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Destination name is required.")]
        [StringLength(100, ErrorMessage = "Destination name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}