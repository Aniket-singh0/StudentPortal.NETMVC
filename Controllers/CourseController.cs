using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int studentId =
                HttpContext.Session.GetInt32("StudentId").Value;

            var courses = await _context.Courses
                .Where(x => x.StudentId == studentId)
                .ToListAsync();

            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
       
        public async Task<IActionResult> Create(Course course)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            course.StudentId = studentId.Value;
            course.Student = null; // Navigation property ko null set karein

            ModelState.Clear(); // Clear any model state errors

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Course added successfully.";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            int studentId =
                HttpContext.Session.GetInt32("StudentId").Value;

            var course = await _context.Courses
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            int studentId =
                HttpContext.Session.GetInt32("StudentId").Value;

            var existing = await _context.Courses
                .FirstOrDefaultAsync(x =>
                    x.Id == course.Id &&
                    x.StudentId == studentId);

            if (existing == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(course);
            }

            existing.CourseName = course.CourseName;
            existing.Instructor = course.Instructor;
            existing.Duration = course.Duration;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Course updated successfully.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int studentId =
                HttpContext.Session.GetInt32("StudentId").Value;

            var course = await _context.Courses
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StudentId == studentId);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Course deleted successfully.";

            return RedirectToAction("Index");
        }
    }
}