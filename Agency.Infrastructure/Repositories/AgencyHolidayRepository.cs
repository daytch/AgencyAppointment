using Agency.Application.Interfaces.Repositories;
using Agency.Domain.Entities;
using Agency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Repositories
{
    public class AgencyHolidayRepository : IAgencyHolidayRepository
    {
        private readonly AppDbContext _context;

        public AgencyHolidayRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AgencyHoliday> AddAsync(AgencyHoliday holiday)
        {
            await _context.AgencyHolidays.AddAsync(holiday);
            await _context.SaveChangesAsync();
            return holiday;
        }

        public async Task<List<AgencyHoliday>> GetAllAsync()
        {
            return await _context.AgencyHolidays.ToListAsync();
        }
        public async Task<bool> IsHolidayAsync(DateTime date, int agencyId)
        {
            return await _context.AgencyHolidays
                .AnyAsync(h => h.AgencyId == agencyId && h.HolidayDate.Date == date.Date);
        }
    }
}
