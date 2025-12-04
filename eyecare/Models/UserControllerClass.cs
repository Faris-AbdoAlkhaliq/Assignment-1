using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace eyecare.Models
{
    public class UserControllerClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int? Age { get; set; }
        public int ScreenHours { get; set; }
        public string Glasses { get; set; }
        public string DeviceType { get; set; }
    }
}