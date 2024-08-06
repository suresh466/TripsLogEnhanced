using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TripsLog.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        // date field for start and end date are required
        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [ValidateEndDate(ErrorMessage = "End Date must be after Start Date.")]
        public DateTime EndDate { get; set; }

        // relationship with Accommodation
        public int? AccommodationId { get; set; }
        // relationship with Accommodation
        public Accommodation Accommodation { get; set; }

        public ICollection<TripActivity> TripActivities { get; set; }
    }

    // Custom validation attribute for End Date
    public class ValidateEndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var trip = (Trip)validationContext.ObjectInstance;
            if (trip.EndDate <= trip.StartDate)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}