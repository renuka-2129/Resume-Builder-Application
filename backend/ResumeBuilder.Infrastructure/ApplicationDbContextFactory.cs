using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ResumeBuilder.Infrastructure.Data;

namespace ResumeBuilder.Infrastructure
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            // Use SQLite
            optionsBuilder.UseSqlite("Data Source=ResumeBuilder.db");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
