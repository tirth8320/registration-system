using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using registration.Data;

namespace registration.Controllers
{
    public class DocumentVerificationController : Controller
    {
        private readonly ApplicationDBContext _context;

        public DocumentVerificationController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var documents = _context.UserDocuments
                .Include(x => x.UserMaster)
                .OrderByDescending(x => x.UploadedDate)
                .ToList();

            return View(documents);
        }

        public IActionResult Approve(int id)
        {
            var doc = _context.UserDocuments.Find(id);

            if (doc != null)
            {
                doc.Status = "Approved";
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var doc = _context.UserDocuments.Find(id);

            if (doc != null)
            {
                doc.Status = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var doc = _context.UserDocuments.Find(id);

            if (doc != null)
            {
                _context.UserDocuments.Remove(doc);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}