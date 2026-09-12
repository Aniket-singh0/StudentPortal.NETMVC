
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var attendance = await _context.Attendances
                .Where(x => x.StudentId == studentId.Value)
                .ToListAsync();

            return View(attendance);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance attendance)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            attendance.StudentId = studentId.Value;

            if (ModelState.IsValid)
            {
                _context.Attendances.Add(attendance);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Attendance added successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(attendance);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId.Value);

            if (attendance == null)
                return NotFound();

            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Attendance attendance)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            if (id != attendance.Id)
                return NotFound();

            var existing = await _context.Attendances
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId.Value);

            if (existing == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                existing.Subject = attendance.Subject;
                existing.TotalClasses = attendance.TotalClasses;
                existing.PresentClasses = attendance.PresentClasses;

                await _context.SaveChangesAsync();

                TempData["Success"] = "Attendance updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId.Value);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Attendance deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
