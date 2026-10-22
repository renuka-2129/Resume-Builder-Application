using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Core.Entities;

namespace ResumeBuilder.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Resume> Resumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resume>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ProfessionalSummary).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Skills).HasMaxLength(1000);
                entity.Property(e => e.Projects).IsRequired().HasMaxLength(4000);
                entity.Property(e => e.Qualifications).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.References).IsRequired().HasMaxLength(2000);
                
                entity.HasIndex(e => e.Email).HasDatabaseName("IX_Resumes_Email");
                entity.HasIndex(e => e.JobTitle).HasDatabaseName("IX_Resumes_JobTitle");
            });
        }
    }
}
