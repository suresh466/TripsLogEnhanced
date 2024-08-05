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
            // Include related entities when fetching trips
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
                Accommodations = new SelectList(context.Accommodations, "Id", "Name"),
                Activities = new MultiSelectList(context.Activities, "Id", "Name")
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

                // Add activities to the trip
                if (model.ActivityIds != null)
                {
                    trip.TripActivities = model.ActivityIds.Select(activityId => new TripActivity
                    {
                        ActivityId = activityId
                    }).ToList();
                }

                context.Trips.Add(trip);
                context.SaveChanges();

                TempData["Message"] = "Trip added successfully!";
                return RedirectToAction("Index");
            }

            // If we got this far, something failed; redisplay form
            model.Destinations = new SelectList(context.Destinations, "Id", "Name");
            model.Accommodations = new SelectList(context.Accommodations, "Id", "Name");
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

        // POST: Home/AddDestination
        [HttpPost]
        public IActionResult AddDestination(ManagerViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewDestination))
            {
                context.Destinations.Add(new Destination { Name = model.NewDestination });
                context.SaveChanges();
                TempData["Message"] = "Destination added successfully!";
            }
            return RedirectToAction("Manager");
        }

        // POST: Home/AddAccommodation
        [HttpPost]
        public IActionResult AddAccommodation(ManagerViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewAccommodation))
            {
                context.Accommodations.Add(new Accommodation
                {
                    Name = model.NewAccommodation,
                    Phone = model.NewAccommodationPhone,
                    Email = model.NewAccommodationEmail
                });
                context.SaveChanges();
                TempData["Message"] = "Accommodation added successfully!";
            }
            return RedirectToAction("Manager");
        }

        // POST: Home/AddActivity
        [HttpPost]
        public IActionResult AddActivity(ManagerViewModel model)
        {
            if (!string.IsNullOrEmpty(model.NewActivity))
            {
                context.Activities.Add(new Activity { Name = model.NewActivity });
                context.SaveChanges();
                TempData["Message"] = "Activity added successfully!";
            }
            return RedirectToAction("Manager");
        }

        // POST: Home/DeleteDestination/5
        [HttpPost]
        public IActionResult DeleteDestination(int id)
        {
            var destination = context.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            try
            {
                context.Destinations.Remove(destination);
                context.SaveChanges();
                TempData["Message"] = "Destination deleted successfully!";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "Cannot delete destination as it is associated with one or more trips.";
            }

            return RedirectToAction("Manager");
        }

        // POST: Home/DeleteAccommodation/5
        [HttpPost]
        public IActionResult DeleteAccommodation(int id)
        {
            var accommodation = context.Accommodations.Find(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            context.Accommodations.Remove(accommodation);
            context.SaveChanges();
            TempData["Message"] = "Accommodation deleted successfully!";

            return RedirectToAction("Manager");
        }

        // POST: Home/DeleteActivity/5
        [HttpPost]
        public IActionResult DeleteActivity(int id)
        {
            var activity = context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }

            context.Activities.Remove(activity);
            context.SaveChanges();
            TempData["Message"] = "Activity deleted successfully!";

            return RedirectToAction("Manager");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}