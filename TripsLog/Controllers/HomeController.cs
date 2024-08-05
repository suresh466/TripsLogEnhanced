using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            // initialize the TripContext
            context = ctx;
        }

        // just get all trips and pass them to the view
        public IActionResult Index()
        {
            var trips = context.Trips.ToList();
            return View(trips);
        }

        [HttpGet]
        public IActionResult Add()
        {
            // filling the subhead with appropriate text
            ViewData["Subhead"] = "Add Trip Destination and Dates";
            // return the view with an empty model so the user can fill it
            return View(new TripDetailsViewModel());
        }
        
        // post controller for add we have to store data in TempData for now by invoking Keep() for retention
        [HttpPost]
        public IActionResult Add(TripDetailsViewModel model)
        {
            if (!ModelState.IsValid) ViewData["Subhead"] = "Add Trip Destination and Dates";
            // if input is valid put the data in tempdata
            if (ModelState.IsValid)
            {
                TempData["Destination"] = model.Destination;
                TempData["StartDate"] = model.StartDate;
                TempData["EndDate"] = model.EndDate;
                TempData["Accommodation"] = model.Accommodation;

                //if accommodation field is SqlAlreadyFilledException then redirect to add accommodation
                if (!string.IsNullOrEmpty(model.Accommodation))
                {
                    TempData.Keep("Destination");
                    TempData.Keep("StartDate");
                    TempData.Keep("EndDate");
                    TempData.Keep("Accommodation");
                    return RedirectToAction("AddAccommodation");
                }
                // if accomodation field not filled just take to add thingstodo
                TempData.Keep("Destination");
                TempData.Keep("StartDate");
                TempData.Keep("EndDate");
                return RedirectToAction("AddThingsToDo");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddAccommodation()
        {
            ViewData["Subhead"] = $"Add Info for {TempData["Accommodation"]}";
            // invoke keep again because we'll need them at the end to save
            TempData.Keep("Accommodation");
            TempData.Keep("Destination");
            TempData.Keep("StartDate");
            TempData.Keep("EndDate");
            return View(new AccommodationViewModel
            {
                Accommodation = TempData["Accommodation"]?.ToString()
            });
        }

        [HttpPost]
        public IActionResult AddAccommodation(AccommodationViewModel model)
        {
            // if input is valid then put the data in tempdata for later
            if (ModelState.IsValid)
            {
                TempData["AccommodationPhone"] = model.AccommodationPhone;
                TempData["AccommodationEmail"] = model.AccommodationEmail;
                TempData.Keep("Accommodation");
                TempData.Keep("Destination");
                TempData.Keep("StartDate");
                TempData.Keep("EndDate");
                // then redirect to addthingstodo view
                return RedirectToAction("AddThingsToDo");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult AddThingsToDo()
        {
            ViewData["Subhead"] = $"Add things to do in {TempData["Destination"]}";
            // again invoke keep here to keep data across next request
            TempData.Keep("Accommodation");
            TempData.Keep("Destination");
            TempData.Keep("StartDate");
            TempData.Keep("EndDate");
            TempData.Keep("AccommodationPhone");
            TempData.Keep("AccommodationEmail");
            // return empty thingstodoviewmodel so user can fill it
            return View(new ThingsToDoViewModel());
        }

        [HttpPost]
        public IActionResult AddThingsToDo(ThingsToDoViewModel model)
        {
            // if data is valid then finally create a new Trip object with the data from tempdata
            if (ModelState.IsValid)
            {
                var trip = new Trip
                {
                    // these values are required so we don't do null check we handle it in the view
                    Destination = TempData["Destination"].ToString(),
                    StartDate = DateTime.Parse(TempData["StartDate"].ToString()),
                    EndDate = DateTime.Parse(TempData["EndDate"].ToString()),
                    // accommodation data can is optional so we use null-conditionals
                    Accommodation = TempData["Accommodation"]?.ToString(),
                    AccommodationPhone = TempData["AccommodationPhone"]?.ToString(),
                    AccommodationEmail = TempData["AccommodationEmail"]?.ToString(),
                    // we don't need null conditionals here because if no value then automatically null
                    ThingToDo1 = model.ThingToDo1,
                    ThingToDo2 = model.ThingToDo2,
                    ThingToDo3 = model.ThingToDo3
                };
                // add the trip to database
                context.Trips.Add(trip);
                context.SaveChanges();

                // clear out the tempdata pre-emptively 
                TempData.Clear();
                TempData["Message"] = "Trip added successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // it is cancel controller in case user wants to cancel at any point
        public IActionResult Cancel()
        {
            // it just clears the tempdata and starts over
            TempData.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
