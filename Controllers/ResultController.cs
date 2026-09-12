using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResultController(ApplicationDbContext context)
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

            var results = await _context.Results
                .Where(x => x.StudentId == studentId.Value)
                .ToListAsync();

            return View(results);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Result result)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // StudentId bind karein aur Navigation property detach karein
            result.StudentId = studentId.Value;
            result.Student = null;

            // ModelState errors clear karein
            ModelState.Clear();

            try
            {
                _context.Results.Add(result);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Result added successfully.";
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

            var result = await _context.Results
                .FirstOrDefaultAsync(x => x.Id == id && x.StudentId == studentId.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Result result)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existing = await _context.Results
                .FirstOrDefaultAsync(x => x.Id == result.Id && x.StudentId == studentId.Value);

            if (existing == null)
            {
                return NotFound();
            }

            existing.Subject = result.Subject;
            existing.Marks = result.Marks;
            existing.MaxMarks = result.MaxMarks;
            existing.Grade = result.Grade;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Result updated successfully.";
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

            var result = await _context.Results
                .FirstOrDefaultAsync(x => x.Id == id && x.StudentId == studentId.Value);

            if (result == null)
            {
                return NotFound();
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Result deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}