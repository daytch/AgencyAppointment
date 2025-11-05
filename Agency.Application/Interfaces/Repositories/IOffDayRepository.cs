using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agency.Application.Interfaces.Repositories
{
    public interface IOffDayRepository
    {
        Task<IEnumerable<OffDay>> GetByAgencyIdAsync(int agencyId);
        Task<bool> IsHolidayAsync(DateTime date, int agencyId);
        Task<OffDay> AddSync(OffDay offDay);
    }
}
