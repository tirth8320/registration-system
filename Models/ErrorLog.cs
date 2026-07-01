using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class ErrorLog
    {
        [Key]
        public int ErrorId { get; set; }

        public int? UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserRole { get; set; }

        public string? ControllerName { get; set; }

        public string? ActionName { get; set; }

        public string? RequestMethod { get; set; }

        public string? Url { get; set; }

        public string? Browser { get; set; }

        public string? IPAddress { get; set; }

        public string? ErrorMessage { get; set; }

        public string? StackTrace { get; set; }

        public string? InnerException { get; set; }

        public string? Severity { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Status { get; set; } = "New";
    }
}