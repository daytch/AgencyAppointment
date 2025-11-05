using Agency.Application.DTOs;
using Agency.Application.Interfaces.Repositories;
using Agency.Domain.Entities;
using Agency.Domain.Interfaces;

namespace Agency.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IAgencyRepository _agencyRepo;
    private readonly IOffDayRepository _offDayRepo;

    public AppointmentService(IAppointmentRepository appointmentRepo, IAgencyRepository agencyRepository, IOffDayRepository offDayRepository)
    {
        _appointmentRepo = appointmentRepo;
        _agencyRepo = agencyRepository;
        _offDayRepo = offDayRepository;
    }

    public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            AgencyId = request.AgencyId,
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            AppointmentDate = request.Date,
            CreatedAt = DateTime.UtcNow
        };
        var agency = await _agencyRepo.GetByIdAsync(appointment.AgencyId);
        if (agency == null)
            throw new Exception("Agency not found");

        if (await _offDayRepo.IsHolidayAsync(appointment.AppointmentDate, agency.Id))
            throw new Exception("Cannot book on a holiday");

        var countToday = await _appointmentRepo.CountByDateAsync(agency.Id, appointment.AppointmentDate);

        while (countToday >= agency.MaxAppointmentsPerDay)
        {
            appointment.AppointmentDate = appointment.AppointmentDate.AddDays(1);
            if (await _offDayRepo.IsHolidayAsync(appointment.AppointmentDate, agency.Id))
                continue;
            countToday = await _appointmentRepo.CountByDateAsync(agency.Id, appointment.AppointmentDate);
        }

        var nextNumber = countToday + 1;
        appointment.TokenNumber = $"{agency.Id}-{appointment.AppointmentDate:yyyyMMdd}-{nextNumber:D3}";

        return await _appointmentRepo.AddAsync(appointment);
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsAsync(int agencyId)
    {
        return await _appointmentRepo.GetByAgencyIdAsync(agencyId);
    }

    public async Task<List<CustomerAppointment>> GetDailyQueue(DateTime date)
    {
        var start = date.Date;
        var end = date.Date.AddDays(1).AddTicks(-1);
        return await _appointmentRepo.GetDailyQueueAsync(date);
    }
}
