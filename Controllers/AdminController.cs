using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using registration.Data;
using registration.Models;
using registration.Filters;
using Dapper;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace registration.Controllers
{
    [AdminAuthorize]

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]

    public class AdminController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;
        public AdminController(
     ApplicationDBContext context,
     IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        //public IActionResult Index()
        //{
        //    ViewBag.TotalUsers =
        //        _context.UserMasters.Count();

        //    ViewBag.PendingDocuments =
        //        _context.UserDocuments
        //        .Count(x => x.Status == "Pending");

        //    ViewBag.ApprovedDocuments =
        //        _context.UserDocuments
        //        .Count(x => x.Status == "Approved");

        //    ViewBag.RejectedDocuments =
        //        _context.UserDocuments
        //        .Count(x => x.Status == "Rejected");

        //    var recentDocuments =
        //        _context.UserDocuments
        //        .OrderByDescending(x => x.UploadedDate)
        //        .Take(5)
        //        .ToList();

        //    return View(recentDocuments);
        //}

        public IActionResult Index()
        {
            ViewBag.TotalUsers =
                _context.UserMasters.Count();

            ViewBag.PendingDocuments =
                _context.UserDocuments
                .Count(x => x.Status == "Pending");

            ViewBag.ApprovedDocuments =
                _context.UserDocuments
                .Count(x => x.Status == "Approved");

            ViewBag.RejectedDocuments =
                _context.UserDocuments
                .Count(x => x.Status == "Rejected");

            // NEW
            ViewBag.TotalErrors =
                _context.ErrorLogs.Count();

            ViewBag.NewErrors =
                _context.ErrorLogs
                .Count(x => x.Status == "New");

            ViewBag.CriticalErrors =
                _context.ErrorLogs
                .Count(x => x.Severity == "Critical");

            var recentDocuments =
                _context.UserDocuments
                .OrderByDescending(x => x.UploadedDate)
                .Take(5)
                .ToList();

            return View(recentDocuments);
        }
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var roleId = HttpContext.Session.GetInt32("RoleId");

        //    if (roleId == null)
        //    {
        //        context.Result = Content("RoleId is NULL");
        //        return;
        //    }

        //    if (roleId != 1)
        //    {
        //        context.Result = Content($"Access Denied. RoleId = {roleId}");
        //        return;
        //    }

        //    base.OnActionExecuting(context);
        //}

        // USER LIST

        //public IActionResult UserList()
        //{
        //    var users = _context.UserMasters
        //    .FromSqlRaw("SELECT * FROM GetAllUsers()")
        //    .ToList();

        //    return View(users);
        //}

        // DELETE USER

        public IActionResult Delete(int id)
        {
            var user = _context.UserMasters
                .FirstOrDefault(x => x.UserId == id);

            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("UserList");
            }

            // Check if deleting an Admin
            if (user.RoleId == 1)
            {
                int adminCount = _context.UserMasters
                    .Count(x => x.RoleId == 1);

                if (adminCount <= 1)
                {
                    TempData["Error"] =
                        "Cannot delete the last Admin user.";

                    return RedirectToAction("UserList");
                }
            }

            _context.UserMasters.Remove(user);
            _context.SaveChanges();

            TempData["Success"] =
                "User deleted successfully.";

            return RedirectToAction("UserList");
        }

        // GET EDIT

        public IActionResult Edit(int id)
        {
            var user = _context.UserMasters.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = _context.RoleMasters.ToList();

            return View(user);
        }

        // POST EDIT

        [HttpPost]
        public IActionResult Edit(UserMaster user)
        {
            if (ModelState.IsValid)
            {
                _context.UserMasters.Update(user);

                _context.SaveChanges();

                return RedirectToAction("UserList");
            }

            ViewBag.Roles = _context.RoleMasters.ToList();

            return View(user);
        }

        public IActionResult UserList()
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            using (var connection =
                new NpgsqlConnection(connectionString))
            {
                var users = connection.Query<UserMaster, RoleMaster, UserMaster>(
                    @"SELECT u.*,
                     r.""RoleID"",
                     r.""RoleName""
              FROM ""UserMasters"" u
              LEFT JOIN ""RoleMasters"" r
              ON u.""RoleId"" = r.""RoleID""",
                    (user, role) =>
                    {
                        user.RoleMaster = role;
                        return user;
                    },
                    splitOn: "RoleID"
                ).ToList();

                return View(users);
            }
        }

        public IActionResult Approve(int id)
        {
            var doc = _context.UserDocuments.Find(id);

            if (doc != null)
            {
                doc.Status = "Approved";
                _context.SaveChanges();
            }

            return RedirectToAction("Documents");
        }
        public IActionResult Reject(int id)
        {
            var doc = _context.UserDocuments.Find(id);

            if (doc != null)
            {
                doc.Status = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("Documents");
        }
        public IActionResult DeleteDocument(int id)
        {
            var doc = _context.UserDocuments.Find(id);

            if (doc != null)
            {
                _context.UserDocuments.Remove(doc);

                _context.SaveChanges();
            }

            return RedirectToAction("Documents");
        }

        public IActionResult DocumentVerification()
        {
            var documents = _context.UserDocuments
                .Include(x => x.UserMaster)
                .ToList();

            return View(documents);
        }
    }
}