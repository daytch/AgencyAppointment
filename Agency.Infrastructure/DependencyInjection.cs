using Agency.Application.Interfaces.Repositories;
using Agency.Application.Interfaces.Services;
using Agency.Application.Services;
using Agency.Domain.Interfaces;
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

        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAgencyService,AgencyService>();
        services.AddScoped<IOffDayService,OffDayService>();

        services.AddScoped<IOffDayRepository, OffDayRepository>();
        services.AddScoped<IAgencyRepository, AgencyRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();


        return services;
    }
}
