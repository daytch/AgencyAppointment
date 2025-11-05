using Agency.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgencyController : ControllerBase
{
    private readonly IAgencyService _service;
    public AgencyController(IAgencyService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Agency.Domain.Entities.Agency agency)
    {
        if (string.IsNullOrWhiteSpace(agency.Name))
            return BadRequest("Agency name is required");

        var created = await _service.CreateAsync(agency);
        return Ok(created);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
}
