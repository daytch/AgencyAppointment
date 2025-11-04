using Agency.Application.DTOs;
using Agency.Application.Services;
using Agency.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Agency.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _service;
    public AppointmentController(AppointmentService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            AgencyId = request.AgencyId,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            AppointmentDate = request.Date,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _service.CreateAppointmentAsync(appointment);
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
