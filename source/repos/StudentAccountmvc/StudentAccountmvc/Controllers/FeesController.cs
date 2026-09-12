
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var fees = await _context.Fees
                .Where(x => x.StudentId == studentId.Value)
                .ToListAsync();

            return View(fees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fees fees)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            fees.StudentId = studentId.Value;

            if (fees.PaidAmount > fees.TotalAmount)
            {
                ModelState.AddModelError(
                    "PaidAmount",
                    "Paid amount cannot be greater than total amount."
                );
            }

            if (ModelState.IsValid)
            {
                _context.Fees.Add(fees);

                await _context.SaveChangesAsync();

                TempData["Success"] =
                    "Fees added successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(fees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var fees = await _context.Fees
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId.Value);

            if (fees == null)
                return NotFound();

            return View(fees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Fees fees)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            if (id != fees.Id)
                return NotFound();

            var existing = await _context.Fees
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId.Value);

            if (existing == null)
                return NotFound();

            if (fees.PaidAmount > fees.TotalAmount)
            {
                ModelState.AddModelError(
                    "PaidAmount",
                    "Paid amount cannot be greater than total amount."
                );
            }

            if (ModelState.IsValid)
            {
                existing.FeeType = fees.FeeType;
                existing.TotalAmount = fees.TotalAmount;
                existing.PaidAmount = fees.PaidAmount;

                await _context.SaveChangesAsync();

                TempData["Success"] =
                    "Fees updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(fees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var fees = await _context.Fees
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId.Value);

            if (fees == null)
                return NotFound();

            _context.Fees.Remove(fees);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Fees deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}

