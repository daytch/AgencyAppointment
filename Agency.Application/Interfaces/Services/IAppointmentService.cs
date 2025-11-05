using Agency.Application.DTOs;
using Agency.Domain.Entities;

namespace Agency.Domain.Interfaces;

public interface IAppointmentService
{
    Task<Appointment> CreateAppointmentAsync(CreateAppointmentRequest request);
    Task<IEnumerable<Appointment>> GetAppointmentsAsync(int agencyId);
    Task<List<CustomerAppointment>> GetDailyQueue(DateTime date);
}
