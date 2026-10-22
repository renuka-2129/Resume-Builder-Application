using Microsoft.AspNetCore.Mvc;

namespace ResumeBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { 
            message = "Resume Builder API is working!", 
            timestamp = DateTime.UtcNow,
            status = "Running"
        });
    }
}
