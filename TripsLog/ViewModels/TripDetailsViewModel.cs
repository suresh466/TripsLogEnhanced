using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TripsLog.ViewModels
{
    // view model for trip details
    public class TripDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        [Display(Name = "Destination")]
        public int DestinationId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [ValidateEndDate(ErrorMessage = "End Date must be after Start Date.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Accommodation")]
        public int? AccommodationId { get; set; }

        public SelectList Destinations { get; set; }
        public SelectList Accommodations { get; set; }
    }

    // Custom validation attribute for End Date
    public class ValidateEndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (TripDetailsViewModel)validationContext.ObjectInstance;
            if (model.EndDate <= model.StartDate)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}