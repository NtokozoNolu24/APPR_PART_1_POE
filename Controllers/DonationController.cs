using APPR_PART_1_POE.Data;
using APPR_PART_1_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace APPR_PART_1_POE.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the donation form
        public IActionResult Index()
        {
            return View();
        }

        // Receives and saves the donation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.DonationDate = DateTime.Now;

                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(TaxCertificate), new
                {
                    id = donation.Id
                });
            }

            return View(donation);
        }

        // Displays a placeholder tax certificate
        public async Task<IActionResult> TaxCertificate(int id)
        {
            var donation = await _context.Donations.FindAsync(id);

            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }
    }
}