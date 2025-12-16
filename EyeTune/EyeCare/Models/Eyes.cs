namespace EyeCare.Models
{
    public class Eyes
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int ScreenTimeHoursPerDay { get; set; }
        public string GlassesType { get; set; } 

        // Eye history
        public bool HasEyeDisease { get; set; }
        public string EyeDiseaseDetails { get; set; }
        public bool FamilyHistoryOfEyeDisease { get; set; }
        public string PastEyeSurgeries { get; set; }

        // Symptoms
        public bool ExperiencesEyeStrain { get; set; }
        public bool ExperiencesDryEyes { get; set; }
        public bool BlurredVision { get; set; }

        // Lifestyle
        public bool Smokes { get; set; }
        public bool WearsSunglasses { get; set; }

        // Eye care habits
        public int LastEyeCheckupInMonths { get; set; }
        public bool UsesEyeDrops { get; set; }

        public Eyes() { 
        }
    }
}