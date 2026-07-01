using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using registration.Data;
using registration.Models;
using registration.Services;
using System.Text.RegularExpressions;


namespace registration.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ErrorLogService _errorLogService;

        public RegisterController(
    ApplicationDBContext context,
    ErrorLogService errorLogService)
        {
            _context = context;

            _errorLogService = errorLogService;
        }

        // GET
        public IActionResult Index()
        {
            ViewBag.Roles = _context.RoleMasters.ToList();

            return View();
        }

        // POST

        [HttpPost]
        public IActionResult Index(UserMaster user)
        {
            string pattern =
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

            if (!Regex.IsMatch(user.Password, pattern))
            {
                ModelState.AddModelError(
                    "Password",
                    "Password must be at least 8 characters and contain an uppercase letter, lowercase letter, number and special character.");

                ViewBag.Roles = _context.RoleMasters.ToList();

                return View(user);
            }

            if (ModelState.IsValid)
            {
                user.Password =
                    BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.UserMasters.Add(user);

                _context.SaveChanges();

                ViewBag.Message = "Registration Successful";

                ViewBag.Roles = _context.RoleMasters.ToList();

                return View();
            }

            ViewBag.Roles = _context.RoleMasters.ToList();

            return View(user);
        }
    }
}

