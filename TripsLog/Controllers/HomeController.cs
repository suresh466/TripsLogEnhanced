using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripsLog.Models;
using TripsLog.ViewModels;

namespace TripsLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TripContext context;

        public HomeController(ILogger<HomeController> logger, TripContext ctx)
        {
            _logger = logger;
            // initialize the context
            context = ctx;
        }

        // show the list of trips
        public IActionResult Index()
        {
            var trips = context.Trips
                .Include(t => t.Destination)
                .Include(t => t.Accommodation)
                .Include(t => t.TripActivities)
                    .ThenInclude(ta => ta.Activity)
                .ToList();

            return View(trips);
        }

        // add a new trip
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Subhead"] = "Add Trip Details";
            var viewModel = new TripDetailsViewModel
            {
                // populate the dropdown lists
                Destinations = new SelectList(context.Destinations, "Id", "Name"),
                Accommodations = new SelectList(context.Accommodations, "Id", "Name")
            };
            return View(viewModel);
        }

        // this handles the form submission for new trip
        [HttpPost]
        public IActionResult Add(TripDetailsViewModel model)
        {
            // check if the model is valid
            if (ModelState.IsValid)
            {
                // create a new trip object
                var trip = new Trip
                {
                    DestinationId = model.DestinationId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    AccommodationId = model.AccommodationId
                };

                // save the trip to the database and redirect to the next step
                context.Trips.Add(trip);
                context.SaveChanges();

                return RedirectToAction("AddActivities", new { tripId = trip.Id });
            }

            // if the model is not valid, show the form again
            model.Destinations = new SelectList(context.Destinations, "Id", "Name");
            model.Accommodations = new SelectList(context.Accommodations, "Id", "Name");
            return View(model);
        }

        // this method shows the list of activities to select from
        [HttpGet]
        public IActionResult AddActivities(int tripId)
        {
            var trip = context.Trips
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                return NotFound();
            }

            // create a view model object
            var viewModel = new ActivitySelectionViewModel
            {
                TripId = tripId,
                Destination = trip.Destination.Name,
                Activities = new MultiSelectList(context.Activities, "Id", "Name")
            };

            return View(viewModel);
        }

        // this method handles the form submission for adding activities
        [HttpPost]
        public IActionResult AddActivities(ActivitySelectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                // add the selected activities to the trip if valid
                var trip = context.Trips.Find(model.TripId);
                if (trip == null)
                {
                    return NotFound();
                }

                if (model.ActivityIds != null)
                {
                    foreach (var activityId in model.ActivityIds)
                    {
                        context.TripActivities.Add(new TripActivity
                        {
                            TripId = model.TripId,
                            ActivityId = activityId
                        });
                    }
                }

                // save the changes and redirect to the list of trips
                context.SaveChanges();

                TempData["Message"] = "Trip added successfully!";
                return RedirectToAction("Index");
            }

            model.Activities = new MultiSelectList(context.Activities, "Id", "Name");
            return View(model);
        }

        // this method deletes a trip
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trip = context.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            context.Trips.Remove(trip);
            context.SaveChanges();

            TempData["Message"] = "Trip deleted successfully!";
            return RedirectToAction("Index");
        }

        // this method shows the manager page
        public IActionResult Manager()
        {
            var viewModel = new ManagerViewModel
            {
                Destinations = context.Destinations.ToList(),
                Accommodations = context.Accommodations.ToList(),
                Activities = context.Activities.ToList()
            };
            return View(viewModel);
        }

        // this method adds new items to the database (ccommodation, destination, activity)
        [HttpPost]
        public IActionResult AddManager(ManagerViewModel model)
        {
            bool itemAdded = false;

            // print the model values

            if (!string.IsNullOrEmpty(model.NewDestination))
            {
                context.Destinations.Add(new Destination { Name = model.NewDestination });
                itemAdded = true;
            }
            if (!string.IsNullOrEmpty(model.NewAccommodation))
            {
                context.Accommodations.Add(new Accommodation
                {
                    Name = model.NewAccommodation,
                    Phone = model.NewAccommodationPhone,
                    Email = model.NewAccommodationEmail
                });
                itemAdded = true;
            }
            if (!string.IsNullOrEmpty(model.NewActivity))
            {
                context.Activities.Add(new Activity { Name = model.NewActivity });
                itemAdded = true;
            }

            if (itemAdded)
            {
                context.SaveChanges();
                TempData["Message"] = "Items added successfully!";
            }
            else
            {
                TempData["Error"] = "No items were added. Please enter at least one item.";
            }
            return RedirectToAction("Manager");
        }

        // this method deletes items from the database (destination, accommodation, activity)
        [HttpPost]
        public IActionResult DeleteManager(int? DeleteDestination, int? DeleteAccommodation, int? DeleteActivity)
        {
            bool itemDeleted = false;

            if (DeleteDestination.HasValue)
            {
                var destination = context.Destinations.Find(DeleteDestination.Value);
                if (destination != null)
                {
                    try
                    {
                        context.Destinations.Remove(destination);
                        context.SaveChanges();
                        itemDeleted = true;
                    }
                    catch (DbUpdateException)
                    {
                        TempData["Error"] = "Cannot delete destination as it is associated with one or more trips.";
                        return RedirectToAction("Manager");
                    }
                }
            }
            if (DeleteAccommodation.HasValue)
            {
                var accommodation = context.Accommodations.Find(DeleteAccommodation.Value);
                if (accommodation != null)
                {
                    context.Accommodations.Remove(accommodation);
                    context.SaveChanges();
                    itemDeleted = true;
                }
            }
            if (DeleteActivity.HasValue)
            {
                var activity = context.Activities.Find(DeleteActivity.Value);
                if (activity != null)
                {
                    context.Activities.Remove(activity);
                    context.SaveChanges();
                    itemDeleted = true;
                }
            }

            if (itemDeleted)
            {
                TempData["Message"] = "Items deleted successfully!";
            }
            else
            {
                TempData["Error"] = "No items were deleted. Please select at least one item to delete.";
            }
            return RedirectToAction("Manager");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}