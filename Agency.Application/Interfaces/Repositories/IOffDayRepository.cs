using Agency.Domain.Entities;

namespace Agency.Application.Interfaces.Repositories
{
    public interface IOffDayRepository
    {
        Task<IEnumerable<OffDay>> GetByAgencyIdAsync(int agencyId);
    }
}
