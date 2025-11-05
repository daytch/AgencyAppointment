using Agency.Domain.Entities;
using Agency.Application;
using Agency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Agency.Application.Interfaces.Repositories;

namespace Agency.Infrastructure.Repositories;

public class OffDayRepository : IOffDayRepository
{
    private readonly AppDbContext _context;
    public OffDayRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<OffDay>> GetByAgencyIdAsync(int agencyId)
        => await _context.OffDays.Where(o => o.AgencyId == agencyId).ToListAsync();

    public async Task<bool> IsHolidayAsync(DateTime date, int agencyId) => await _context.OffDays.AnyAsync(h => h.AgencyId == agencyId && h.Date == date.Date);

    public async Task<OffDay> AddSync(OffDay offDay) { await _context.OffDays.AddAsync(offDay); return offDay; }
}
