using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agency.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Agency> Agencies => Set<Domain.Entities.Agency>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<OffDay> OffDays => Set<OffDay>();
    public DbSet<AgencyHoliday> AgencyHolidays => Set<AgencyHoliday>();
    public DbSet<CustomerAppointment> CustomerAppointments => Set<CustomerAppointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
