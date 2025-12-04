using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eyecare.Models;

namespace eyecare.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EyeCheck()
        {
            return View();
        }

        // POST: Process the form
        [HttpPost]
        public ActionResult EyeCheck(EyeCheckModel formData)
        {
            string message = "Information submitted successfully!";

            // Simple Logic: Check screen hours
            if (formData.ScreenHours > 6)
            {
                message += " Warning: You are exceeding recommended screen time limits. Consider the 20-20-20 rule.";
            }

            // Logic: Check if they wear glasses
            if (formData.Glasses == "yes")
            {
                message += " Remember to clean your lenses to reduce strain.";
            }

            // Send the message back to the page
            ViewBag.Message = message;

            return View();
        }
     }
}