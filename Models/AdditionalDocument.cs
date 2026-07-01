namespace registration.Models
{
    public class AdditionalDocument
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FilePath { get; set; }

        public string OriginalFileName { get; set; }

        public long FileSize { get; set; }

        public string DocumentName { get; set; }

        public DateTime UploadedDate { get; set; }
    }
}