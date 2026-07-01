using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace registration.Models
{

    public class UserDocument
    {
        [Key]
        public int DocumentId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserMaster? UserMaster { get; set; }


        public string? AadhaarPath { get; set; }

        public string? PanPath { get; set; }

        public string? OtherDocumentPath { get; set; }

        public string Status { get; set; } = "Pending";
    

    
        //public UserMaster? UserMaster { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.Now;
    }
}

