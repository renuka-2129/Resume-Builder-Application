using Microsoft.AspNetCore.Mvc;
using ResumeBuilder.Core.DTOs;
using ResumeBuilder.Core.Entities;
using ResumeBuilder.Core.Interfaces;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumesController : ControllerBase
{
    private readonly IResumeRepository _resumeRepository;

    public ResumesController(IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resumes = await _resumeRepository.GetAllAsync();
        return Ok(resumes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resume = await _resumeRepository.GetByIdAsync(id);
        if (resume == null)
            return NotFound();
        return Ok(resume);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResumeDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var resume = new Resume
        {
            FullName = createDto.FullName,
            Email = createDto.Email,
            JobTitle = createDto.JobTitle,
            ProfessionalSummary = createDto.ProfessionalSummary,
            Skills = createDto.Skills,
            Projects = createDto.Projects,
            Qualifications = createDto.Qualifications,
            References = createDto.References
        };

        var created = await _resumeRepository.AddAsync(resume);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Resume resume)
    {
        if (id != resume.Id)
            return BadRequest();

        var existing = await _resumeRepository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _resumeRepository.UpdateAsync(resume);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var resume = await _resumeRepository.GetByIdAsync(id);
        if (resume == null)
            return NotFound();

        await _resumeRepository.DeleteAsync(resume);
        return NoContent();
    }

    [HttpGet("search/{term}")]
    public async Task<IActionResult> Search(string term)
    {
        var results = await _resumeRepository.SearchAsync(term);
        return Ok(results);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var resumes = await _resumeRepository.GetByEmailAsync(email);
        return Ok(resumes);
    }
}
