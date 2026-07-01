using Microsoft.AspNetCore.Mvc;
using registration.Data;
using registration.Models;

namespace registration.Controllers
{
    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]

    public class DocumentController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _environment;

        public DocumentController(
            ApplicationDBContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Upload");
        }

        public IActionResult Upload()
        {
            var username =
                HttpContext.Session.GetString("UserName");

            if (username != null)
            {
                var user =
                    _context.UserMasters
                    .FirstOrDefault(x =>
                        x.UserName == username);

                if (user != null)
                {
                    var document =
                        _context.UserDocuments
                        .FirstOrDefault(x =>
                            x.UserId == user.UserId);

                    if (document != null)
                    {
                        ViewBag.DocumentStatus =
                            document.Status;

                        ViewBag.MainDocument = document;

                        int progress = 0;

                        if (!string.IsNullOrEmpty(document.AadhaarPath))
                        {
                            progress += 25;
                        }

                        if (!string.IsNullOrEmpty(document.PanPath))
                        {
                            progress += 25;
                        }

                        if (!string.IsNullOrEmpty(document.OtherDocumentPath))
                        {
                            progress += 25;
                        }

                        if (document.Status == "Approved")
                        {
                            progress = 100;
                        }
                        else if (document.Status == "Pending")
                        {
                            progress += 25;
                        }

                        ViewBag.Progress = progress;


                        ViewBag.UserDocuments =
                        _context.AdditionalDocuments
                        .Where(x => x.UserId == user.UserId)
                        .ToList();
                    }
                }
            }

            return View();
        }

        //    public IActionResult Upload(
        //IFormFile AadhaarFile,
        //IFormFile PanFile,
        //IFormFile OtherFile)
        //    {
        //        var username =
        //            HttpContext.Session.GetString("UserName");

        //        var user = _context.UserMasters
        //            .FirstOrDefault(x => x.UserName == username);

        //        UserDocument document = new UserDocument
        //        {
        //            UserId = user.UserId,
        //            Status = "Pending",
        //            UploadedDate = DateTime.Now
        //        };

        //        _context.UserDocuments.Add(document);
        //        _context.SaveChanges();

        //        return Content("Saved Successfully");
        //    }

        private bool IsValidPdf(IFormFile file)
        {
            if (file == null)
                return false;

            // Max 2 MB
            if (file.Length > 2 * 1024 * 1024)
                return false;

            // Extension check
            string extension =
                Path.GetExtension(file.FileName)
                .ToLower();

            if (extension != ".pdf")
                return false;

            // Check actual PDF header
            using (var stream = file.OpenReadStream())
            {
                byte[] header = new byte[4];

                stream.Read(header, 0, 4);

                return header[0] == 0x25 &&  // %
                       header[1] == 0x50 &&  // P
                       header[2] == 0x44 &&  // D
                       header[3] == 0x46;    // F
            }
        }



        [HttpPost]
        public IActionResult Upload(
            IFormFile AadhaarFile,
            IFormFile PanFile,
            IFormFile OtherFile,
            List<IFormFile> ExtraFiles,
            List<string> ExtraDocumentNames)
        {
            if (AadhaarFile == null)
            {
                TempData["Error"] =
                    "Aadhaar Card is required.";

                return RedirectToAction("Upload");
            }

            if (PanFile == null)
            {
                TempData["Error"] =
                    "PAN Card is required.";

                return RedirectToAction("Upload");
            }

            if (!IsValidPdf(AadhaarFile))
            {
                TempData["Error"] =
                    "Aadhaar must be a PDF file under 2 MB.";

                return RedirectToAction("Upload");
            }

            if (!IsValidPdf(PanFile))
            {
                TempData["Error"] =
                    "PAN must be a PDF file under 2 MB.";

                return RedirectToAction("Upload");
            }

            if (OtherFile != null &&
                !IsValidPdf(OtherFile))
            {
                TempData["Error"] =
                    "Other Document must be a PDF file under 2 MB.";

                return RedirectToAction("Upload");
            }



            var username =
                HttpContext.Session.GetString("UserName");

            if (username == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var user = _context.UserMasters
                .FirstOrDefault(x => x.UserName == username);

            var oldDocument =
    _context.UserDocuments
    .FirstOrDefault(x => x.UserId == user.UserId);

            if (oldDocument != null)
            {
                if (oldDocument.Status == "Pending")
                {
                    TempData["Error"] =
                        "Your documents are already under review.";

                    return RedirectToAction("Upload");
                }

                if (oldDocument.Status == "Approved")
                {
                    TempData["Error"] =
                        "Your documents have already been approved.";

                    return RedirectToAction("Upload");
                }
            }

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            string? aadhaarPath = null;
            string? panPath = null;
            string? otherPath = null;

            // Aadhaar

            if (AadhaarFile != null)
            {
                string fileName =
                    Guid.NewGuid() +
                    Path.GetExtension(AadhaarFile.FileName);

                string path =
                    Path.Combine(
                        _environment.WebRootPath,
                        "uploads",
                        fileName);

                using (var stream =
                       new FileStream(path, FileMode.Create))
                {
                    AadhaarFile.CopyTo(stream);
                }

                aadhaarPath = "/uploads/" + fileName;
            }

            // PAN

            if (PanFile != null)
            {
                string fileName =
                    Guid.NewGuid() +
                    Path.GetExtension(PanFile.FileName);

                string path =
                    Path.Combine(
                        _environment.WebRootPath,
                        "uploads",
                        fileName);

                using (var stream =
                       new FileStream(path, FileMode.Create))
                {
                    PanFile.CopyTo(stream);
                }

                panPath = "/uploads/" + fileName;
            }

            // Other

            if (OtherFile != null)
            {
                string fileName =
                    Guid.NewGuid() +
                    Path.GetExtension(OtherFile.FileName);

                string path =
                    Path.Combine(
                        _environment.WebRootPath,
                        "uploads",
                        fileName);

                using (var stream =
                       new FileStream(path, FileMode.Create))
                {
                    OtherFile.CopyTo(stream);
                }

                otherPath = "/uploads/" + fileName;
            }


            UserDocument document =
                new UserDocument
                {
                    UserId = user.UserId,

                    AadhaarPath = aadhaarPath,

                    PanPath = panPath,

                    OtherDocumentPath = otherPath,

                    Status = "Pending"
                };

            //var oldDocument =
            //    _context.UserDocuments
            //    .FirstOrDefault(x => x.UserId == user.UserId);

            if (oldDocument != null)
            {
                if (!string.IsNullOrEmpty(oldDocument.AadhaarPath))
                {
                    string oldFile =
                        Path.Combine(
                            _environment.WebRootPath,
                            oldDocument.AadhaarPath.TrimStart('/'));

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                if (!string.IsNullOrEmpty(oldDocument.PanPath))
                {
                    string oldFile =
                        Path.Combine(
                            _environment.WebRootPath,
                            oldDocument.PanPath.TrimStart('/'));

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                if (!string.IsNullOrEmpty(oldDocument.OtherDocumentPath))
                {
                    string oldFile =
                        Path.Combine(
                            _environment.WebRootPath,
                            oldDocument.OtherDocumentPath.TrimStart('/'));

                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                }

                _context.UserDocuments.Remove(oldDocument);

                _context.SaveChanges();
            }

            _context.UserDocuments.Add(document);

            _context.SaveChanges();

            if (ExtraFiles != null && ExtraFiles.Count > 0)
            {
                int index = 0;

                if (ExtraFiles != null)
                {
                    foreach (var file in ExtraFiles)
                    {
                        if (!IsValidPdf(file))
                        {
                            TempData["Error"] =
                                "Only PDF files up to 2 MB are allowed.";

                            return RedirectToAction("Upload");
                        }
                    }
                }

                foreach (var file in ExtraFiles)
                {
                    if (file.Length > 0)
                    {
                        string fileName =
                            Guid.NewGuid() +
                            Path.GetExtension(file.FileName);

                        string path =
                            Path.Combine(
                                _environment.WebRootPath,
                                "uploads",
                                fileName);

                        using (var stream =
                            new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        AdditionalDocument extraDoc =
                        new AdditionalDocument
                        {
                            UserId = user.UserId,

                            FilePath = "/uploads/" + fileName,

                            OriginalFileName = file.FileName,

                            FileSize = file.Length,

                            DocumentName =
                                ExtraDocumentNames[index],

                            UploadedDate = DateTime.Now
                        };

                        _context.AdditionalDocuments
                            .Add(extraDoc);
                        index++;
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Upload");
        }
    
    }
}
        