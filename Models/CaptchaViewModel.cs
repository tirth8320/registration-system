using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class CaptchaViewModel
    {
        [Required]
        public string CaptchaCode { get; set; }
    }
}