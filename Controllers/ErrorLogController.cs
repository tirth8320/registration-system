using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using registration.Data;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;


namespace registration.Controllers
{
    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]

    public class ErrorLogController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ErrorLogController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(
    string search = "",
    string status = "All")
        {
            var logs = _context.ErrorLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                logs = logs.Where(x =>
                    (x.UserName != null &&
                     x.UserName.Contains(search))
                    ||

                    (x.ControllerName != null &&
                     x.ControllerName.Contains(search))
                    ||

                    (x.ErrorMessage != null &&
                     x.ErrorMessage.Contains(search)));
            }

            if (status != "All")
            {
                logs = logs.Where(x =>
                    x.Status == status);
            }

            ViewBag.Status = status;

            ViewBag.Search = search;

            ViewBag.TotalErrors =
                _context.ErrorLogs.Count();

            ViewBag.NewErrors =
                _context.ErrorLogs
                .Count(x => x.Status == "New");

            ViewBag.CriticalErrors =
                _context.ErrorLogs
                .Count(x => x.Severity == "Critical");

            ViewBag.ReviewedErrors =
                _context.ErrorLogs
                .Count(x => x.Status == "Reviewed");
            // ==============================
            // UTC Date Filters
            // ==============================

            DateTime startOfToday = DateTime.UtcNow.Date;
            DateTime last7Days = DateTime.UtcNow.Date.AddDays(-7);
            DateTime last30Days = DateTime.UtcNow.Date.AddMonths(-1);

            ViewBag.TodayErrors =
                _context.ErrorLogs.Count(x => x.CreatedDate >= startOfToday);

            ViewBag.ThisWeekErrors =
                _context.ErrorLogs.Count(x => x.CreatedDate >= last7Days);

            ViewBag.ThisMonthErrors =
                _context.ErrorLogs.Count(x => x.CreatedDate >= last30Days);

            // Today's Errors
            ViewBag.TodayErrors =
                _context.ErrorLogs.Count(x =>
                    x.CreatedDate >= startOfToday);

            // Last 7 Days
            ViewBag.ThisWeekErrors =
                _context.ErrorLogs.Count(x =>
                    x.CreatedDate >= last7Days);

            // Last Month
            ViewBag.ThisMonthErrors =
                _context.ErrorLogs.Count(x =>
                    x.CreatedDate >= last30Days);

            // Top 5 Controllers
            ViewBag.TopControllers =
                _context.ErrorLogs
                .GroupBy(x => x.ControllerName)
                .Select(x => new
                {
                    Controller = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();
            // Top 5 Error Messages
            ViewBag.TopErrors =
                _context.ErrorLogs
                .GroupBy(x => x.ErrorMessage)
                .Select(x => new
                {
                    Message = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            // Top 5 Users
            ViewBag.TopUsers =
                _context.ErrorLogs
                .GroupBy(x => x.UserName)
                .Select(x => new
                {
                    User = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            return View(
                logs.OrderByDescending(x =>
                    x.CreatedDate)
                .ToList());
        }

        public IActionResult Details(int id)
        {
            var error =
                _context.ErrorLogs
                .FirstOrDefault(x =>
                    x.ErrorId == id);

            if (error == null)
            {
                return NotFound();
            }

            return View(error);
        }
        public IActionResult MarkReviewed(int id)
        {
            var log = _context.ErrorLogs.Find(id);

            if (log == null)
                return NotFound();

            log.Status = "Reviewed";

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var log = _context.ErrorLogs.Find(id);

            if (log == null)
            {
                return NotFound();
            }

            _context.ErrorLogs.Remove(log);

            _context.SaveChanges();

            TempData["Success"] = "Error log deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExportExcel()
        {
            var logs = _context.ErrorLogs
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            using var workbook = new XLWorkbook();

            var ws = workbook.Worksheets.Add("Error Logs");

            ws.Cell(1, 1).Value = "Error ID";
            ws.Cell(1, 2).Value = "User";
            ws.Cell(1, 3).Value = "Controller";
            ws.Cell(1, 4).Value = "Action";
            ws.Cell(1, 5).Value = "Severity";
            ws.Cell(1, 6).Value = "Status";
            ws.Cell(1, 7).Value = "Date";
            ws.Cell(1, 8).Value = "Error Message";

            int row = 2;

            foreach (var item in logs)
            {
                ws.Cell(row, 1).Value = item.ErrorId;
                ws.Cell(row, 2).Value = item.UserName;
                ws.Cell(row, 3).Value = item.ControllerName;
                ws.Cell(row, 4).Value = item.ActionName;
                ws.Cell(row, 5).Value = item.Severity;
                ws.Cell(row, 6).Value = item.Status;
                ws.Cell(row, 7).Value = item.CreatedDate.ToString("dd MMM yyyy HH:mm");
                ws.Cell(row, 8).Value = item.ErrorMessage;

                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"ErrorLogs_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

        public IActionResult ExportPdf()
        {
            var logs = _context.ErrorLogs
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            QuestPDF.Settings.License = LicenseType.Community;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(25);

                    page.Header().Text("Error Log Report")
                        .FontSize(22)
                        .Bold();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ID").Bold();
                            header.Cell().Text("User").Bold();
                            header.Cell().Text("Controller").Bold();
                            header.Cell().Text("Severity").Bold();
                            header.Cell().Text("Status").Bold();
                        });

                        foreach (var item in logs)
                        {
                            table.Cell().Text(item.ErrorId.ToString());
                            table.Cell().Text(item.UserName ?? "");
                            table.Cell().Text(item.ControllerName ?? "");
                            table.Cell().Text(item.Severity ?? "");
                            table.Cell().Text(item.Status ?? "");
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated : {DateTime.Now}");
                });
            });

            var stream = new MemoryStream();

            pdf.GeneratePdf(stream);

            return File(
                stream.ToArray(),
                "application/pdf",
                $"ErrorLogs_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }

    }
}