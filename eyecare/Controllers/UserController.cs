using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using eyecare.Models;

namespace eyecare.Controllers
{
    public class UserController : Controller
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EyeTuneDB;Integrated Security=True";
        // ===========================
        // 1. LOGIN PAGE
        // ===========================

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserControllerClass user, bool? RememberMe)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Username", user.Username);
                sqlCmd.Parameters.AddWithValue("@Password", user.Password);

                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                if (count == 1)
                {
                    // 1. Set Session (Essential for logic)
                    Session["Username"] = user.Username;

                    // 2. SET COOKIE [Assignment Requirement]
                    if (RememberMe == true)
                    {
                        HttpCookie authCookie = new HttpCookie("UserCookie");
                        authCookie.Value = user.Username;
                        authCookie.Expires = DateTime.Now.AddDays(7); // Keep user logged in for 7 days
                        Response.Cookies.Add(authCookie);
                    }

                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Username or Password";
                    return View();
                }
            }
        }

        // ===========================
        // 2. SIGN UP PAGE
        // ===========================

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        // STEP 2: SUBMIT THE DATA
        // This method runs when you click the "Sign Up" button.
        [HttpPost]
        public ActionResult Signup(UserControllerClass newUser)
        {
            // 1. Validation Check: Stops empty/bad data from entering the database
            if (ModelState.IsValid)
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "INSERT INTO Users (Username, Email, Password, Age) VALUES (@Username, @Email, @Password, @Age)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                    // 2. Add Parameters safely
                    sqlCmd.Parameters.AddWithValue("@Username", newUser.Username ?? "");
                    sqlCmd.Parameters.AddWithValue("@Email", newUser.Email ?? "");
                    sqlCmd.Parameters.AddWithValue("@Password", newUser.Password ?? "");

                    // 3. Handle the optional Age correctly
                    if (newUser.Age == null)
                        sqlCmd.Parameters.AddWithValue("@Age", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@Age", newUser.Age);

                    sqlCmd.ExecuteNonQuery();
                }

                TempData["SuccessMessage"] = "Account created! Please login.";
                return RedirectToAction("Login");
            }
            else
            {
                // 4. If validation fails, show the form again with errors
                return View(newUser);
            }
        }
        // ===========================
        // 4. ADMIN PAGE (SEARCH & RETRIEVE)
        // ===========================
        [HttpGet]
        public ActionResult Admin(string search)
        {
            // Create a list to hold the users we find
            List<UserControllerClass> userList = new List<UserControllerClass>();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                // Default Query: Get All Users
                string query = "SELECT * FROM Users";

                // If user typed a search term, change query to filter results
                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE Username LIKE @Search";
                }

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);

                // Add the search parameter if needed
                if (!string.IsNullOrEmpty(search))
                {
                    sqlCmd.Parameters.AddWithValue("@Search", "%" + search + "%");
                }

                // Execute and Read Data
                SqlDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    userList.Add(new UserControllerClass
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Age = reader["Age"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Age"])
                    });
                }
            }

            // Pass the list to the View
            return View(userList);
        }
        // ===========================
        // 3. LOGOUT
        // ===========================
        public ActionResult Logout()
        {
            Session.Clear(); // Remove all session data
            return RedirectToAction("Login");
        }
    }
}