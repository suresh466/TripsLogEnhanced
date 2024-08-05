using System;
using System.ComponentModel.DataAnnotations;

namespace TripsLog.Models
{
    public class Trip
    {
        public int Id { get; set; }
        // following 3 fields are required
        [Required(ErrorMessage = "Destination is required.")]
        public string Destination { get; set; }
        // we have data annotions for validation here as a backup and at viewmodels primarily
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }
        // DateTime type for dates
        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }
        // The rest are optional fields
        public string Accommodation { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationEmail { get; set; }
        public string ThingToDo1 { get; set; }
        public string ThingToDo2 { get; set; }
        public string ThingToDo3 { get; set; }
    }
}
