using System.ComponentModel.DataAnnotations;
using System;

namespace TripsLog.ViewModels
{
    public class TripDetailsViewModel
    {
        // all are required except for the accomodation
        [Required(ErrorMessage = "Destination is required.")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }
        public string Accommodation { get; set; }
    }
}
