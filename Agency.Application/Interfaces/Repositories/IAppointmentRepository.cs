using Agency.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Agency.Application.Interfaces.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment> AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        Task<int> CountByDateAsync(int agencyId, DateTime date);
        Task<IEnumerable<Appointment>> GetByAgencyIdAsync(int agencyId);
        Task<List<CustomerAppointment>> GetDailyQueueAsync(DateTime date);
    }
}
