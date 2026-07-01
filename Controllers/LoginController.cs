using Microsoft.AspNetCore.Mvc;
using registration.Data;
using registration.Models;
using BCrypt.Net;


namespace registration.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LoginController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
           

            return View();
        }

        [HttpPost]
        public IActionResult Login(
     string username,
     string password,
     string CaptchaInput)
        {
            string? captcha =
                HttpContext.Session.GetString("CaptchaCode");

            if (string.IsNullOrEmpty(CaptchaInput))
            {
                ViewBag.Message = "Please enter captcha.";
                return View("Index");
            }

            if (CaptchaInput != captcha)
            {
                ViewBag.Message = "Invalid captcha.";
                return View("Index");
            }

            var user =
                _context.UserMasters
                .FirstOrDefault(x =>
                    x.UserName == username);

            if (user == null)
            {
                ViewBag.Error =
                    "Invalid Username or Password";

                return View("Index");
            }

            bool isValid =
    BCrypt.Net.BCrypt.Verify(
        password,
        user.Password);

            if (!isValid)
            {
                ViewBag.Error =
                    "Invalid Username or Password";

                return View("Index");
            }

            

            HttpContext.Session.SetString(
                "UserName",
                user.UserName);

            HttpContext.Session.SetInt32(
                "RoleId",
                user.RoleId);

            if (user.RoleId == 1)
            {
                return RedirectToAction(
                    "Index",
                    "Home");
            }

            return RedirectToAction(
                "Index",
                "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("Index", "Login");
        }
    }
}
