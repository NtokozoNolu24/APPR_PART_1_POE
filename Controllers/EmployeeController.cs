using APPR_PART_1_POE.Data;
using APPR_PART_1_POE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APPR_PART_1_POE.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var volunteerCount = await _context.Volunteers.CountAsync();

            ViewBag.VolunteerCount = volunteerCount;

            return View();
        }

        public async Task<IActionResult> Volunteers()
        {
            var volunteers = await _context.Volunteers.ToListAsync();

            return View(volunteers);
        }
    }
}