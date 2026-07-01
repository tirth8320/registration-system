using Microsoft.AspNetCore.Mvc;
using registration.Models;
using System.Diagnostics;
using registration.Data;

namespace registration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalUsers =
                _context.UserMasters.Count();

            ViewBag.TotalRoles =
                _context.RoleMasters.Count();

            ViewBag.PendingDocuments =
                _context.UserDocuments
                .Count(x => x.Status == "Pending");

            ViewBag.ApprovedDocuments =
                _context.UserDocuments
                .Count(x => x.Status == "Approved");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id ??
                        HttpContext.TraceIdentifier
                });
        }
    }
}