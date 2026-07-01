using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class PasswordResetOtp
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Otp { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsUsed { get; set; }
    }
}