using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentAccountmvc.Data;
using StudentAccountmvc.Filters;
using StudentAccountmvc.Models;

namespace StudentAccountmvc.Controllers
{
    [StudentLoginFilter]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Student> _passwordHasher;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Student>();
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
        public async Task<IActionResult> ChangePassword(
            string currentPassword,
            string newPassword,
            string confirmPassword)
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

            var result = _passwordHasher.VerifyHashedPassword(
                student,
                student.Password,
                currentPassword
            );

            if (result == PasswordVerificationResult.Failed)
            {
                TempData["Error"] = "Current password is incorrect.";
                return RedirectToAction("Index");
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] =
                    "New password and confirm password do not match.";

                return RedirectToAction("Index");
            }

            student.Password = _passwordHasher.HashPassword(
                student,
                newPassword
            );

            await _context.SaveChangesAsync();

            TempData["Success"] = "Password changed successfully.";

            return RedirectToAction("Index");
        }
    }
}