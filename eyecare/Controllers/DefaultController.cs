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
        // 2. ABOUT US PAGE
        public ActionResult About()
        {
            ViewBag.Message = "About EyeTune";
            return View();
        }
        // POST: Process the form
        [HttpPost]
        public ActionResult EyeCheck(eyecare.Models.EyeCheckModel formData)
        {
            // 1. Logic to calculate the warning message (Keep your existing logic)
            string message = "Information submitted successfully!";
            if (formData.ScreenHours > 6)
            {
                message += " Warning: You are exceeding recommended screen time limits.";
            }
            if (formData.Glasses == "yes")
            {
                message += " Remember to clean your lenses.";
            }
            ViewBag.Message = message;

            // 2. NEW LOGIC: Save to Database
            // We need the logged-in username. If they aren't logged in, we save as "Guest"
            string currentUser = Session["Username"] != null ? Session["Username"].ToString() : "Guest";

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EyeTuneDB;Integrated Security=True";

            using (System.Data.SqlClient.SqlConnection sqlCon = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO EyeChecks (Username, ScreenHours, DeviceType, Glasses) VALUES (@Username, @ScreenHours, @DeviceType, @Glasses)";
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand(query, sqlCon);

                sqlCmd.Parameters.AddWithValue("@Username", currentUser);
                sqlCmd.Parameters.AddWithValue("@ScreenHours", formData.ScreenHours);
                sqlCmd.Parameters.AddWithValue("@DeviceType", formData.DeviceType ?? "Unknown"); // Handle nulls
                sqlCmd.Parameters.AddWithValue("@Glasses", formData.Glasses ?? "No");

                sqlCmd.ExecuteNonQuery();
            }

            return View();
        }
    }
}