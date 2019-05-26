using DocumentProcessing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DocumentProcessing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Purpose> Purposes { get; set; }
        
        public DbSet<Document> Documents { get; set; }
        
        public DbSet<Status> Statuses { get; set; }
        
        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<ScannedFile> ScannedFiles { get; set; }
        
        public DbSet<DocumentOwner> DocumentOwners { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>()
                .HasMany(x => x.ScannedFiles)
                .WithOne(x => x.Document)
                .HasForeignKey(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Purpose>()
                .HasMany(x => x.Documents)
                .WithOne(x => x.Purpose)
                .IsRequired(false);

            modelBuilder.Entity<Status>()
                .HasMany(x => x.Documents)
                .WithOne(x => x.Status)
                .IsRequired(false);

            modelBuilder.Entity<DocumentOwner>()
                .HasMany(x => x.Documents)
                .WithOne(x => x.Owner);

            modelBuilder.Entity<Applicant>()
                .HasMany(x => x.Documents)
                .WithOne(x => x.Applicant);

            modelBuilder.Entity<Document>()
                .HasOne(x => x.Recipient)
                .WithMany();
        }
    }
}