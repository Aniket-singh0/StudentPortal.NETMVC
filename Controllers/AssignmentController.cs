using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var assignments = await _context.Assignments
                .Where(x => x.StudentId == studentId.Value)
                .OrderBy(x => x.DueDate)
                .ToListAsync();

            return View(assignments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            assignment.StudentId = studentId.Value;
            assignment.Student = null; // Navigation property detach

            try
            {
                _context.Assignments.Add(assignment);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Assignment added successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string sqlError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Content("SQL Error: " + sqlError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(x => x.Id == id && x.StudentId == studentId.Value);

            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Assignment assignment)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existing = await _context.Assignments
                .FirstOrDefaultAsync(x => x.Id == assignment.Id && x.StudentId == studentId.Value);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Title = assignment.Title;
            existing.Subject = assignment.Subject;
            existing.DueDate = assignment.DueDate;
            existing.Status = assignment.Status;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Assignment updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(x => x.Id == id && x.StudentId == studentId.Value);

            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Assignment deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}