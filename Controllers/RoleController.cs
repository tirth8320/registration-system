using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using registration.Data;
using registration.Models;
namespace registration.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RoleController(ApplicationDBContext context)
        {
            _context = context;
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
        public IActionResult Index()
        {
            var Roles = _context.RoleMasters.ToList();

            return View(Roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(RoleMaster Role)
        {
            if(ModelState.IsValid)
            {
                _context.RoleMasters.Add(Role);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(Role);
        }


        public IActionResult Edit(int id)
        {

            var Role = _context.RoleMasters.Find(id);

            if (Role == null)
            {
                return NotFound();
            }
            return View(Role);
        }

        [HttpPost]
        public IActionResult Edit(RoleMaster Role)
        {
            if (ModelState.IsValid)
            {
                _context.RoleMasters.Update(Role);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(Role);
        }

        public IActionResult Delete(int id)
        {
            var Role = _context.RoleMasters.Find(id);
            if(Role != null)
            {
                _context.RoleMasters.Remove(Role);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
