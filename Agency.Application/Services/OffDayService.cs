using Agency.Application.Interfaces.Repositories;
using Agency.Domain.Entities;

namespace Agency.Application.Services;

public class OffDayService
{
    private readonly IOffDayRepository _offDayRepo;

    public OffDayService(IOffDayRepository offDayRepo)
    {
        _offDayRepo = offDayRepo;
    }

    public Task<IEnumerable<OffDay>> GetByAgencyIdAsync(int agencyId)
        => _offDayRepo.GetByAgencyIdAsync(agencyId);
}
