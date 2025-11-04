using Agency.Domain.Entities;

namespace Agency.Application.Interfaces.Repositories
{
    public interface IAgencyHolidayRepository
    {
        Task<bool> IsHolidayAsync(DateTime date, int agencyId);
        Task<AgencyHoliday> AddAsync(AgencyHoliday holiday);
        Task<List<AgencyHoliday>> GetAllAsync();
    }
}
