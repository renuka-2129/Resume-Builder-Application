using ResumeBuilder.Core.Entities;

namespace ResumeBuilder.Core.Interfaces;

public interface IResumeRepository : IRepository<Resume>
{
    Task<IEnumerable<Resume>> GetByEmailAsync(string email);
    Task<IEnumerable<Resume>> SearchAsync(string searchTerm);
}
