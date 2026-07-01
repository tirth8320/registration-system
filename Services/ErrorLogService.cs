using registration.Data;
using registration.Models;

namespace registration.Services
{
    public class ErrorLogService
    {
        private readonly ApplicationDBContext _context;

        public ErrorLogService(
            ApplicationDBContext context)
        {
            _context = context;
        }

        public void LogError(
    Exception ex,
    HttpContext context)
        {
            int? userId = null;
            string? userName = "Guest";
            string? roleName = "Guest";

            try
            {
                var session = context.Features
                    .Get<Microsoft.AspNetCore.Http.Features.ISessionFeature>()
                    ?.Session;

                if (session != null)
                {
                    userId = session.GetInt32("UserId");
                    userName = session.GetString("UserName") ?? "Guest";
                    roleName = session.GetString("RoleName") ?? "Guest";
                }
            }
            catch
            {
                // Ignore session errors
            }

            ErrorLog error = new ErrorLog
            {
                UserId = userId,

                UserName = userName,

                UserRole = roleName,

                ControllerName =
                    context.Request.RouteValues["controller"]?.ToString(),

                ActionName =
                    context.Request.RouteValues["action"]?.ToString(),

                RequestMethod =
                    context.Request.Method,

                Url =
                    context.Request.Path,

                Browser =
                    context.Request.Headers["User-Agent"].ToString(),

                IPAddress =
                    context.Connection.RemoteIpAddress?.ToString(),

                ErrorMessage =
                    ex.Message,

                StackTrace =
                    ex.StackTrace,

                InnerException =
                    ex.InnerException?.Message,

                Severity = "Critical",

                CreatedDate = DateTime.UtcNow,

                Status = "New"
            };

            _context.ErrorLogs.Add(error);

            _context.SaveChanges();
        }
    }
}