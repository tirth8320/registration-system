using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    

namespace registration.Models
{
    public class UserMaster
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } 
                                                      
        [Required]                                    
        [StringLength(50)]                            
        public string UserName { get; set; }

        [RegularExpression(
@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
ErrorMessage = "Password must contain uppercase, lowercase, number and special character.")]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailID { get; set; } 

        [Required]
        public string MobileNo { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public RoleMaster? RoleMaster { get; set; }

        public List<UserDocument>? Documents { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}