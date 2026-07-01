using System.ComponentModel.DataAnnotations;

namespace registration.Models
{
    public class RoleMaster
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
        public List<UserMaster>? Users { get; set; }
    }
}