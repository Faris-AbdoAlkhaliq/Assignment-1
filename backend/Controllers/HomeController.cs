using System.Diagnostics;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class HomeController : Controller
    {
        // we added cookies.
        public IActionResult SetCookies(string cookieName, string cookieValue)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(15), // Cookie expires in 14 days
                HttpOnly = true, // Prevent JavaScript access to the cookies
                Secure = true, // Use Secure flage
                SameSite = SameSiteMode.Strict // Prevent CSRF attacks
            };
            Response.Cookies.Append(cookieName, cookieValue, options);
            return Ok("Cookies has been set.");
        }
        public IActionResult Index()
        {
            // create a session id
            SetSession("id", Guid.NewGuid().ToString());
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // User is logged in, get their username
                SetCookies("userName"
               , User.Identity.Name);
                // create a session with username
                SetSession("username", User.Identity.Name);
            }
            else
            {
                // User is not logged in, set a cookie with "Guest"
                SetCookies("userName"
               , "guest");
                // create a session with username
                SetSession("username", "guest");
            }
            // Get the broswer type
            SetCookies("broswerName"
           , Request.Headers["UserAgent"].ToString());

            return View();
        }

        // we added sessions.
        public IActionResult SetSession(string key, string value)
        {
            // Set session value
            HttpContext.Session.SetString(key, value);
            return RedirectToAction("Index");
        }


        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
