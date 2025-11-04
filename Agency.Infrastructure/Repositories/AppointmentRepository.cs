using Agency.Domain.Entities;
using Agency.Infrastructure.Data;
using Agency.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Agency)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date)
        {
            return await _context.Appointments
                .Include(a => a.Agency)
                .Where(a => a.AppointmentDate == date)
                .OrderBy(a => a.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Agency)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Appointments.FindAsync(id);
            if (entity != null)
            {
                _context.Appointments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountByDateAsync(int agencyId, DateTime date)
        {
            return await _context.Appointments.Where(x => x.AgencyId == agencyId)
                .CountAsync(a => a.AppointmentDate == date);
        }
        public async Task<IEnumerable<Appointment>> GetByAgencyIdAsync(int agencyId)
       => await _context.Appointments.Where(a => a.AgencyId == agencyId).ToListAsync();
    }
}
