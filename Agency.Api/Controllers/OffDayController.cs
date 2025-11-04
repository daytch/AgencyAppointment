using Agency.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffDayController : ControllerBase
{
    private readonly OffDayService _service;
    public OffDayController(OffDayService service) => _service = service;

    [HttpGet("{agencyId}")]
    public async Task<IActionResult> GetByAgency(int agencyId)
        => Ok(await _service.GetByAgencyIdAsync(agencyId));
}
