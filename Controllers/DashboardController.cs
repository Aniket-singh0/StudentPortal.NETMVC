using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int studentId =
                HttpContext.Session.GetInt32("StudentId").Value;

            var student = await _context.Students
                .FirstOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Login", "Account");
            }

            ViewBag.CourseCount =
                await _context.Courses
                    .CountAsync(x => x.StudentId == studentId);

            ViewBag.AssignmentCount =
                await _context.Assignments
                    .CountAsync(x => x.StudentId == studentId);

            ViewBag.ResultCount =
                await _context.Results
                    .CountAsync(x => x.StudentId == studentId);

            ViewBag.AttendanceCount =
                await _context.Attendances
                    .CountAsync(x => x.StudentId == studentId);

            return View(student);
        }
    }
}