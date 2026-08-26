using APPR_PART_1_POE.Data;
using APPR_PART_1_POE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APPR_PART_1_POE.Controllers
{
    [Authorize(Roles = "Employee")]
    public class ReliefController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReliefController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all relief operations
        public async Task<IActionResult> Index()
        {
            var operations = await _context.ReliefOperations
                .OrderByDescending(r => r.DatePosted)
                .ToListAsync();

            return View(operations);
        }

        // Display the form for creating an operation
        public IActionResult Create()
        {
            return View();
        }

        // Save the new relief operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReliefOperation reliefOperation)
        {
            if (ModelState.IsValid)
            {
                reliefOperation.DatePosted = DateTime.Now;

                _context.ReliefOperations.Add(reliefOperation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(reliefOperation);
        }
    }
}