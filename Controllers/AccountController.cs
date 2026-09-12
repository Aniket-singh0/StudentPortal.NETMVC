using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentAccountmvc.Data;
using StudentAccountmvc.Models;
using StudentAccountmvc.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentAccountmvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Student> _passwordHasher;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Student>();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            bool emailExists = await _context.Students
                .AnyAsync(x => x.Email == student.Email);

            if (emailExists)
            {
                ModelState.AddModelError(
                    "Email",
                    "Email already registered."
                );

                return View(student);
            }

            student.Password =
                _passwordHasher.HashPassword(
                    student,
                    student.Password
                );

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Registration successful. Please login.";

            return RedirectToAction("Login");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(
            string email,
            string password)
        {
            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password))
            {
                ViewBag.Error =
                    "Email and Password are required.";

                return View();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(x => x.Email == email);

            if (student == null)
            {
                ViewBag.Error =
                    "Invalid email or password.";

                return View();
            }

            var result =
                _passwordHasher.VerifyHashedPassword(
                    student,
                    student.Password,
                    password
                );

            if (result == PasswordVerificationResult.Failed)
            {
                ViewBag.Error =
                    "Invalid email or password.";

                return View();
            }

            HttpContext.Session.SetInt32(
                "StudentId",
                student.Id
            );

            HttpContext.Session.SetString(
                "StudentName",
                student.FullName
            );

            HttpContext.Session.SetString(
                "StudentEmail",
                student.Email
            );

            return RedirectToAction(
                "Index",
                "Dashboard"
            );
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}