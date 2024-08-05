using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TripsLog.Models
{
    public class Accommodation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Accommodation name is required.")]
        [StringLength(100, ErrorMessage = "Accommodation name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}