using Agency.Application.Interfaces.Repositories;
using Agency.Application.Services;
using Agency.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agency.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<Data.AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<AppointmentService>();
        services.AddScoped<AgencyService>();
        services.AddScoped<OffDayService>();

        services.AddScoped<IAgencyHolidayRepository, AgencyHolidayRepository>();
        services.AddScoped<IOffDayRepository, OffDayRepository>();
        services.AddScoped<IAgencyRepository, AgencyRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();


        return services;
    }
}
