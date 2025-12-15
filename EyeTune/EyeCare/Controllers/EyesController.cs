using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EyeCare.Data;
using EyeCare.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
namespace EyeCare.Controllers
{
    public class EyesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EyesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eyes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eyes.ToListAsync());
        }

        // GET: Eyes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyes = await _context.Eyes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eyes == null)
            {
                return NotFound();
            }

            return View(eyes);
        }

        // GET: Eyes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eyes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Age,Gender,ScreenTimeHoursPerDay,GlassesType,HasEyeDisease,EyeDiseaseDetails,FamilyHistoryOfEyeDisease,PastEyeSurgeries,ExperiencesEyeStrain,ExperiencesDryEyes,BlurredVision,Smokes,WearsSunglasses,LastEyeCheckupInMonths,UsesEyeDrops")] Eyes eyes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eyes);
                await _context.SaveChangesAsync();

                // We save the Age into the session to use it later
                HttpContext.Session.SetInt32("UserAge", eyes.Age);

                // Optional: We can also save a generic "Active" flag
                HttpContext.Session.SetString("SessionStatus", "Active");
               
                return RedirectToAction("MyTips", new { id = eyes.Id });
            }
            return View(eyes);
        }

        // GET: Eyes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyes = await _context.Eyes.FindAsync(id);
            if (eyes == null)
            {
                return NotFound();
            }
            return View(eyes);
        }

        // POST: Eyes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Age,Gender,ScreenTimeHoursPerDay,GlassesType,HasEyeDisease,EyeDiseaseDetails,FamilyHistoryOfEyeDisease,PastEyeSurgeries,ExperiencesEyeStrain,ExperiencesDryEyes,BlurredVision,Smokes,WearsSunglasses,LastEyeCheckupInMonths,UsesEyeDrops")] Eyes eyes)
        {
            if (id != eyes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eyes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EyesExists(eyes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eyes);
        }

        // GET: Eyes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyes = await _context.Eyes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eyes == null)
            {
                return NotFound();
            }

            return View(eyes);
        }
        // GET: Eyes/MyTips/5
        public async Task<IActionResult> MyTips(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eyes = await _context.Eyes.FirstOrDefaultAsync(m => m.Id == id);
            if (eyes == null)
            {
                return NotFound();
            }

            // Generate the tips based on the data
            List<string> tips = GenerateEyeCareTips(eyes);

            // Send the list of tips to the View using ViewBag
            ViewBag.TipsList = tips;

            return View(eyes);
        }

        // HEALPER METHOD: The logic for your advice
        private List<string> GenerateEyeCareTips(Eyes user)
        {
            var tips = new List<string>();

            // --- 1. EXISTING BASIC LOGIC ---
            if (user.ScreenTimeHoursPerDay > 6)
            {
                tips.Add("20-20-20 Rule: Every 20 mins, look 20 feet away for 20 seconds.");
            }

            if (user.LastEyeCheckupInMonths > 12)
            {
                tips.Add("Checkup Required: It has been over a year. Schedule an appointment.");
            }

            if (user.Smokes)
            {
                tips.Add("Stop Smoking: Smoking creates 'free radicals' that damage the retina.");
            }

            // --- 2. NEW LOGIC FOR DROPDOWNS ---

            // Specific Advice for Contact Lenses
            if (user.GlassesType == "Contact Lenses")
            {
                tips.Add("Hygiene: Never use tap water to clean lenses. Replace your case every 3 months.");
                tips.Add("Rest: Remove contacts at least 1 hour before sleep to let your corneas breathe.");
            }

            // Specific Advice for Reading Glasses (Presbyopia)
            if (user.GlassesType == "Reading" || user.GlassesType == "Bifocal")
            {
                tips.Add("Lighting: Ensure you have bright, direct light on your reading material to reduce strain.");
            }

            // Specific Advice for Diseases
            if (user.EyeDiseaseDetails == "Diabetic Retinopathy")
            {
                tips.Add("Blood Sugar: The #1 way to save your vision is managing your blood glucose levels strictly.");
            }
            else if (user.EyeDiseaseDetails == "Glaucoma")
            {
                tips.Add("Consistency: Glaucoma drops must be taken at the EXACT same time every day to be effective.");
            }
            else if (user.EyeDiseaseDetails == "Cataract")
            {
                tips.Add("UV Protection: Wear a hat and sunglasses outdoors to slow cataract progression.");
            }
            else if (user.EyeDiseaseDetails == "Macular Degeneration")
            {
                tips.Add("Diet: Eat leafy greens (kale, spinach) and fish high in Omega-3s.");
            }

            // Specific Advice for Past Surgeries
            if (user.PastEyeSurgeries == "LASIK" || user.PastEyeSurgeries == "PRK")
            {
                tips.Add("Dry Eye: Post-LASIK dry eye is common. Use preservative-free tears if you feel grit.");
                tips.Add("Protection: Wear eye protection during sports; your corneal flap is sensitive.");
            }

            // Age-based generic check
            if (user.Age > 40 && user.GlassesType == "None")
            {
                tips.Add("Age Alert: After 40, natural reading vision declines. If you hold phones further away to read, get checked for Presbyopia.");
            }

            // Fallback
            if (tips.Count == 0)
            {
                tips.Add("You are doing great! Keep maintaining a healthy diet and protecting your eyes from UV light.");
            }

            return tips;
        }
        // POST: Eyes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eyes = await _context.Eyes.FindAsync(id);
            if (eyes != null)
            {
                _context.Eyes.Remove(eyes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EyesExists(int id)
        {
            return _context.Eyes.Any(e => e.Id == id);
        }
    }
}
