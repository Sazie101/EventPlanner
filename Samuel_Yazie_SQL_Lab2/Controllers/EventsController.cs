using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPBLL;
using IPENTITES;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Samuel_Yazie_SQL_Lab2.Controllers
{
    public class EventsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            EventsService es = new EventsService();
            var events = es.GetEvents();
            return View(events);
        }

        public IActionResult CreateEventsView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateEventsView(Events events)
        {
            EventsService es = new EventsService();
            if (es.AddEventsService(events))
            {
                ViewBag.Message = "Event added successfully";
            }
            return View();
        }

        /// <summary>
        /// EditEventView is responsible for fetching the event with the given eventId and displaying it in the view
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IActionResult EditEventsView(int eventId)
        {
            EventsService eventService = new EventsService();
            // Fetch all the events and find the event with the given eventId
            var @event = eventService.GetEvents().Find(e => e.EventId == eventId);

            return View(@event);
        }

        /// <summary>
        /// EditEventView is responsible for updating the event with the given eventId and displaying it in the view
        /// and pass that to the service and repository to update the event
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditEventsView(Events @event)
        {
            EventsService eventService = new EventsService();
            if (eventService.UpdateEventsService(@event))
            {
                ViewBag.Message = "Event updated successfully";
            }
            return View();
        }

        public IActionResult DeleteEvent(int eventId)
        {
            EventsService es = new EventsService();
            if (es.DeleteEventsService(eventId))
            {
                ViewBag.Message = "Trip deleted successfully";
                return RedirectToAction("Index");
            }
            return null;
        }
    }
}

