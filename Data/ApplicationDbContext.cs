using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DocumentProcessing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DocumentProcessing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        static ApplicationDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<AppointmentCharacters>();
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Purpose> Purposes { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<VisaType> VisaType { get; set; }

        public DbSet<VisaDateType> VisaDateType { get; set; }

        public DbSet<Applicant> Applicants { get; set; }

        public DbSet<ScannedFile> ScannedFiles { get; set; }

        public DbSet<RequestId> RequestId { get; set; }

        public DbSet<DocumentOwner> DocumentOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ForNpgsqlHasEnum<AppointmentCharacters>();

            var lastEntryNumberString = _configuration.GetValue<string>("UserSettings:LastEntryNumber");

            int.TryParse(lastEntryNumberString, out var lastEntryNumber);
            
            modelBuilder.HasSequence<long>("EntryNumbers")
                .StartsAt(lastEntryNumber);
            
            modelBuilder.Entity<Document>()
                .Property(o => o.EntryNumber)
                .HasDefaultValueSql("nextval('\"EntryNumbers\"')");

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

            modelBuilder.Entity<VisaType>()
                .HasMany(x => x.Documents)
                .WithOne(x => x.VisaType)
                .IsRequired(false);

            modelBuilder.Entity<VisaDateType>()
            .HasMany(x => x.Documents)
            .WithOne(x => x.VisaDateType)
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

            modelBuilder.Entity<Document>()
                .HasOne(x => x.Appointment)
                .WithOne(x => x.Document)
                .HasForeignKey<Appointment>(x => x.DocumentId);
        }
    }
}