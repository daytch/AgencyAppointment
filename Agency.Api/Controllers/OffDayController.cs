using Agency.Application.DTOs;
using Agency.Application.Interfaces.Services;
using Agency.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffDayController : ControllerBase
{
    private readonly IOffDayService _service;
    public OffDayController(IOffDayService service) => _service = service;

    [HttpGet("{agencyId}")]
    public async Task<IActionResult> GetByAgency(int agencyId)
        => Ok(await _service.GetByAgencyIdAsync(agencyId));

    [HttpPost]
    public async Task<IActionResult> Create(CreateOffDayRequest request)
        => Ok(await _service.CreateOffDayAsync(request));
}
