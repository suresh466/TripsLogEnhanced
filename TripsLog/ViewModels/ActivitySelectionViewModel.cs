using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TripsLog.ViewModels
{
    // view model for activity selection
    public class ActivitySelectionViewModel
    {
        public int TripId { get; set; }
        public string Destination { get; set; }

        [Display(Name = "Activities")]
        public List<int> ActivityIds { get; set; }

        public MultiSelectList Activities { get; set; }
    }
}