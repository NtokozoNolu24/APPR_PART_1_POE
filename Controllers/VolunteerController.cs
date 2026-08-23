using APPR_PART_1_POE.Data;
using APPR_PART_1_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APPR_PART_1_POE.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the volunteer registration form
        public IActionResult Index()
        {
            return View();
        }

        // Receives the completed volunteer form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                _context.Volunteers.Add(volunteer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ThankYou));
            }

            return View(volunteer);
        }

        // Displays a confirmation page after registration
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}