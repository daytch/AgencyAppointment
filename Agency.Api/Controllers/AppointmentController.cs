using Agency.Application.DTOs;
using Agency.Application.Services;
using Agency.Domain.Entities;
using Agency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;
    public AppointmentController(IAppointmentService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentRequest request)
    {
        var created = await _service.CreateAppointmentAsync(request);
        return Ok(created);
    }

    [HttpGet("{agencyId}")]
    public async Task<IActionResult> GetAll(int agencyId)
    {
        var data = await _service.GetAppointmentsAsync(agencyId);
        return Ok(data);
    }

    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyQueue([FromQuery] DateTime date)
    {
        var queue = await _service.GetDailyQueue(date);
        return Ok(queue);
    }
}
