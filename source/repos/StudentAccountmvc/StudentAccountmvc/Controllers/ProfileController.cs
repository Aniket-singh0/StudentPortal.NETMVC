using Microsoft.AspNetCore.Mvc;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
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

            var student = await _context.Students.FindAsync(studentId.Value);

            if (student == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Student student)
        {
            int? studentId = HttpContext.Session.GetInt32("StudentId");

            if (studentId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingStudent =
                await _context.Students.FindAsync(studentId.Value);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FullName = student.FullName;
            existingStudent.Mobile = student.Mobile;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.Gender = student.Gender;
            existingStudent.Course = student.Course;
            existingStudent.Branch = student.Branch;
            existingStudent.CollegeName = student.CollegeName;
            existingStudent.EnrollmentNumber =
                student.EnrollmentNumber;

            await _context.SaveChangesAsync();

            HttpContext.Session.SetString(
                "StudentName",
                existingStudent.FullName ?? ""
            );

            TempData["Success"] =
                "Profile updated successfully.";

            return RedirectToAction("Index");
        }
    }
}