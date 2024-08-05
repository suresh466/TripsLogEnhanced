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
            context = ctx;
        }

        // GET: Home
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

        // GET: Home/Add
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Subhead"] = "Add Trip Details";
            var viewModel = new TripDetailsViewModel
            {
                Destinations = new SelectList(context.Destinations, "Id", "Name"),
                Accommodations = new SelectList(context.Accommodations, "Id", "Name")
            };
            return View(viewModel);
        }

        // POST: Home/Add
        [HttpPost]
        public IActionResult Add(TripDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    DestinationId = model.DestinationId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    AccommodationId = model.AccommodationId
                };

                context.Trips.Add(trip);
                context.SaveChanges();

                return RedirectToAction("AddActivities", new { tripId = trip.Id });
            }

            model.Destinations = new SelectList(context.Destinations, "Id", "Name");
            model.Accommodations = new SelectList(context.Accommodations, "Id", "Name");
            return View(model);
        }

        // GET: Home/AddActivities
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

            var viewModel = new ActivitySelectionViewModel
            {
                TripId = tripId,
                Destination = trip.Destination.Name,
                Activities = new MultiSelectList(context.Activities, "Id", "Name")
            };

            return View(viewModel);
        }

        // POST: Home/AddActivities
        [HttpPost]
        public IActionResult AddActivities(ActivitySelectionViewModel model)
        {
            if (ModelState.IsValid)
            {
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

                context.SaveChanges();

                TempData["Message"] = "Trip added successfully!";
                return RedirectToAction("Index");
            }

            model.Activities = new MultiSelectList(context.Activities, "Id", "Name");
            return View(model);
        }

        // POST: Home/Delete/5
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

        // GET: Home/Manager
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

        [HttpPost]
        public IActionResult AddManager(ManagerViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewDestination))
            {
                context.Destinations.Add(new Destination { Name = model.NewDestination });
            }
            if (!string.IsNullOrEmpty(model.NewAccommodation))
            {
                context.Accommodations.Add(new Accommodation
                {
                    Name = model.NewAccommodation,
                    Phone = model.NewAccommodationPhone,
                    Email = model.NewAccommodationEmail
                });
            }
            if (!string.IsNullOrEmpty(model.NewActivity))
            {
                context.Activities.Add(new Activity { Name = model.NewActivity });
            }
            context.SaveChanges();
            TempData["Message"] = "Items added successfully!";
            return RedirectToAction("Manager");
        }

        [HttpPost]
        public IActionResult DeleteManager(int? DeleteDestination, int? DeleteAccommodation, int? DeleteActivity)
        {
            if (DeleteDestination.HasValue)
            {
                var destination = context.Destinations.Find(DeleteDestination.Value);
                if (destination != null)
                {
                    context.Destinations.Remove(destination);
                }
            }
            if (DeleteAccommodation.HasValue)
            {
                var accommodation = context.Accommodations.Find(DeleteAccommodation.Value);
                if (accommodation != null)
                {
                    context.Accommodations.Remove(accommodation);
                }
            }
            if (DeleteActivity.HasValue)
            {
                var activity = context.Activities.Find(DeleteActivity.Value);
                if (activity != null)
                {
                    context.Activities.Remove(activity);
                }
            }
            context.SaveChanges();
            TempData["Message"] = "Items deleted successfully!";
            return RedirectToAction("Manager");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}