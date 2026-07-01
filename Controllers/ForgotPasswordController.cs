using Microsoft.AspNetCore.Mvc;
using registration.Data;
using registration.Models;
using registration.Services;

namespace registration.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly EmailServices _emailService;

        public ForgotPasswordController(
            ApplicationDBContext context,
            EmailServices emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendOtp(string username,string email)
        {
            var user =
    _context.UserMasters
    .FirstOrDefault(x =>
        x.UserName == username &&
        x.EmailID == email);

            if (user == null)
            {
                TempData["Error"] =
                    "Email not found.";

                return RedirectToAction("Index");
            }

            string otp =
                new Random()
                .Next(100000, 999999)
                .ToString();

            PasswordResetOtp otpRecord =
                new PasswordResetOtp
                {
                    Email = email,
                    Otp = otp,
                    CreatedDate = DateTime.UtcNow,
                    IsUsed = false
                };

            _context.PasswordResetOtps.Add(otpRecord);

            _context.SaveChanges();

            _emailService.SendOtp(email, otp);

            HttpContext.Session.SetInt32(
    "ResetUserId",
    user.UserId);

            HttpContext.Session.SetString(
    "ResetEmail",
    email);

            return RedirectToAction("VerifyOtp");
        }

        public IActionResult VerifyOtp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyOtp(string otp)
        {
            var otpRecord =
                _context.PasswordResetOtps
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefault(x =>
                    x.Otp == otp &&
                    !x.IsUsed);
            

            if (otpRecord == null)
            {
                TempData["Error"] = "Invalid OTP";
                return View();
            }

            otpRecord.IsUsed = true;

            _context.SaveChanges();


            otpRecord.IsUsed = true;

            _context.SaveChanges();

            HttpContext.Session.SetString(
                "ResetEmail",
                otpRecord.Email);

            return RedirectToAction("ResetPassword");
        }

        public IActionResult ResetPassword()
        {
            string email =
                HttpContext.Session.GetString(
                    "ResetEmail");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(
    string newPassword,
    string confirmPassword)
        {
            string email =
HttpContext.Session.GetString(
    "ResetEmail");

            if (email == null)
            {
                return RedirectToAction("Index");
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] =
                    "Passwords do not match.";

                TempData["ResetEmail"] = email;

                return View();
            }

            int? userId =
    HttpContext.Session.GetInt32(
        "ResetUserId");

            var user =
                _context.UserMasters
                .FirstOrDefault(
                    x => x.UserId == userId);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            user.Password =
    BCrypt.Net.BCrypt.HashPassword(
        newPassword);

            _context.SaveChanges();

            TempData["Success"] =
                "Password reset successfully.";

            HttpContext.Session.Remove(
    "ResetEmail");

            return RedirectToAction(
                "Index",
                "Login");

        }
    }
}