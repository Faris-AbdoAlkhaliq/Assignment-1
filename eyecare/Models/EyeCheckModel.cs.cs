using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eyecare.Models
{
    public class EyeCheckModel
    {
        public int Age { get; set; }

        public int ScreenHours { get; set; }

        public string DeviceType { get; set; }

        public string Glasses { get; set; }

        public string BreakReminder { get; set; }

    }
}