using Agency.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Agency.Application.Interfaces.Repositories;

namespace Agency.Infrastructure.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly AppDbContext _context;

        public AgencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Agency> GetByIdAsync(int id)
            => await _context.Agencies.FindAsync(id).ConfigureAwait(false);

        public async Task<IEnumerable<Domain.Entities.Agency>> GetAllAsync()
            => await _context.Agencies.ToListAsync();

        public async Task<Domain.Entities.Agency> AddAsync(Domain.Entities.Agency agency)
        {
            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();
            return agency;
        }

        public async Task UpdateAsync(Domain.Entities.Agency agency)
        {
            _context.Agencies.Update(agency);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Agencies.FindAsync(id);
            if (entity != null)
            {
                _context.Agencies.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
