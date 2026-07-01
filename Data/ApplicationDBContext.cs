using Microsoft.EntityFrameworkCore;
using registration.Models;

namespace registration.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext

        (
            DbContextOptions<ApplicationDBContext> options
        ) : base(options)
        {

        }

        public DbSet<UserMaster> UserMasters { get; set; }

        public DbSet<RoleMaster> RoleMasters { get; set; }

        public DbSet<UserDocument> UserDocuments { get; set; }

        public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }

        public DbSet<AdditionalDocument> AdditionalDocuments { get; set; }

        public DbSet<ErrorLog> ErrorLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserMaster>()
                .Property(u => u.DOB)
                .HasColumnType("Timestamp without time zone");

            modelBuilder.Entity<UserMaster>()
           .Property(u => u.CreatedDate)
           .HasColumnType("Timestamp without time zone");

            modelBuilder.Entity<UserDocument>()
            .Property(x => x.UploadedDate)
            .HasColumnType("timestamp without time zone");

        }
    }
}


