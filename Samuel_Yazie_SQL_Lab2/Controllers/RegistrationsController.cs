using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPBLL;
using IPENTITES;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Samuel_Yazie_SQL_Lab2.Controllers
{
    public class RegistrationsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int eventId)
        {
            RegistrationService rs = new RegistrationService();
            var registrations = rs.GetRegistartions(eventId);

            return View(registrations);
        }

        public IActionResult CreateRegistrations()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRegistrations(Registartions registration)
        {
            if (ModelState.IsValid)
            {
                // Assuming you have a service to handle registration creation
                RegistrationService rs = new RegistrationService();
                if (rs.AddRegistrationsService(registration))
                {
                    ViewBag.Message = "Registration added successfully";
                    return RedirectToAction("Index", new { eventId = registration.EventId });
                }
            }
            return View();
        }


        /// <summary>
        /// EditRegistrationView is responsible for fetching the registration with the given registrationId and displaying it in the view
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        public IActionResult EditRegistrations(int eventId, int registrationId)
        {
            RegistrationService registrationService = new RegistrationService();
            // Fetch all the registrations and find the registration with the given registrationId
            var registration = registrationService.GetRegistration(eventId, registrationId);

            return View(registration);
        }

        /// <summary>
        /// EditRegistrationsView is responsible for updating the registration with the given registrationId and displaying it in the view
        /// and pass that to the service and repository to update the registration
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditRegistrations(Registartions registration)
        {
            RegistrationService registrationService = new RegistrationService();
            if (registrationService.UpdateRegistrationsService(registration))
            {
                ViewBag.Message = "Registration updated successfully";
            }
            return RedirectToAction("Index", new { eventId = registration.EventId });
        }


        public IActionResult DeleteRegistration(int registrationId, int eventId)
        {
            RegistrationService rs = new RegistrationService();
            if (rs.DeleteRegistrationsService(registrationId))
            {
                ViewBag.Message = "Registration deleted successfully";
                return RedirectToAction("Index", new { eventId });
            }
            return View();
        }
    }
}

