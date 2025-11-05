using Agency.Application.DTOs;
using Agency.Application.Interfaces.Repositories;
using Agency.Application.Interfaces.Services;
using Agency.Domain.Entities;

namespace Agency.Application.Services;

public class OffDayService : IOffDayService
{
    private readonly IOffDayRepository _offDayRepo;

    public OffDayService(IOffDayRepository offDayRepo)
    {
        _offDayRepo = offDayRepo;
    }

    public Task<IEnumerable<OffDay>> GetByAgencyIdAsync(int agencyId)
        => _offDayRepo.GetByAgencyIdAsync(agencyId);
    public async Task<OffDay> CreateOffDayAsync(CreateOffDayRequest request)
    {
        OffDay offDay = new OffDay() { AgencyId = request.AgencyId, Date = request.Date, Reason = request.Reason };
        return await _offDayRepo.AddSync(offDay);
    }
}
