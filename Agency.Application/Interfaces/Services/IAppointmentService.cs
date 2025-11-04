using Agency.Domain.Entities;

namespace Agency.Domain.Interfaces;

public interface IAppointmentService
{
    Task<Appointment> CreateAppointmentAsync(Appointment appointment);
    Task<IEnumerable<Appointment>> GetAppointmentsAsync(int agencyId);
}
