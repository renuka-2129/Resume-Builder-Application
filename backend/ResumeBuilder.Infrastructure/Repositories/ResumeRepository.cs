using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Core.Entities;
using ResumeBuilder.Core.Interfaces;
using ResumeBuilder.Infrastructure.Data;

namespace ResumeBuilder.Infrastructure.Repositories
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly ApplicationDbContext _context;

        public ResumeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Resume?> GetByIdAsync(int id)
        {
            return await _context.Resumes.FindAsync(id);
        }

        public async Task<IEnumerable<Resume>> GetAllAsync()
        {
            return await _context.Resumes.ToListAsync();
        }

        public async Task<IEnumerable<Resume>> FindAsync(System.Linq.Expressions.Expression<Func<Resume, bool>> predicate)
        {
            return await _context.Resumes.Where(predicate).ToListAsync();
        }

        public async Task<Resume> AddAsync(Resume entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Resumes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Resume entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Resume entity)
        {
            _context.Resumes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<Resume, bool>> predicate)
        {
            return await _context.Resumes.AnyAsync(predicate);
        }

        public async Task<IEnumerable<Resume>> GetByEmailAsync(string email)
        {
            return await _context.Resumes
                .Where(r => r.Email == email)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resume>> SearchAsync(string searchTerm)
        {
            return await _context.Resumes
                .Where(r => r.FullName.Contains(searchTerm) || 
                           r.JobTitle.Contains(searchTerm) ||
                           r.ProfessionalSummary.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
