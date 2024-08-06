using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripsLog.Models;

namespace TripsLog.ViewModels
{
    // view model for manager
    public class ManagerViewModel
    {
        [StringLength(100, ErrorMessage = "Destination name cannot be longer than 100 characters.")]
        [Display(Name = "New Destination")]
        public string NewDestination { get; set; }

        [StringLength(100, ErrorMessage = "Accommodation name cannot be longer than 100 characters.")]
        [Display(Name = "New Accommodation")]
        public string NewAccommodation { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter a valid phone number like (123) 456-7890 or 123-456-7890 or 1234567890. .")]
        [Display(Name = "Accommodation Phone")]
        public string NewAccommodationPhone { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Accommodation Email")]
        public string NewAccommodationEmail { get; set; }

        [StringLength(100, ErrorMessage = "Activity name cannot be longer than 100 characters.")]
        [Display(Name = "New Activity")]
        public string NewActivity { get; set; }

        public List<Destination> Destinations { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public List<Activity> Activities { get; set; }
    }
}