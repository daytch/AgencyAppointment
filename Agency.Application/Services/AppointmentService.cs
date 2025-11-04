using Agency.Application.DTOs;
using Agency.Application.Interfaces.Repositories;
using Agency.Domain.Entities;
using Agency.Domain.Interfaces;

namespace Agency.Application.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepo;
    private readonly IAgencyHolidayRepository _holidayRepo;
    private readonly IAgencyRepository _agencyRepo;

    public AppointmentService(IAppointmentRepository appointmentRepo, IAgencyHolidayRepository holidayRepository, IAgencyRepository agencyRepository)
    {
        _appointmentRepo = appointmentRepo;
        _holidayRepo = holidayRepository;
        _agencyRepo = agencyRepository;
    }

    public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
    {
        var agency = await _agencyRepo.GetByIdAsync(appointment.AgencyId);
        if (agency == null)
            throw new Exception("Agency not found");

        // ✅ Cek hari libur
        if (await _holidayRepo.IsHolidayAsync(appointment.AppointmentDate, agency.Id))
            throw new Exception("Cannot book on a holiday");

        // ✅ Hitung jumlah appointment hari ini
        var countToday = await _appointmentRepo.CountByDateAsync(agency.Id, appointment.AppointmentDate);

        // ✅ Kalau sudah penuh, geser ke hari berikutnya yang bukan libur
        while (countToday >= agency.MaxAppointmentsPerDay)
        {
            appointment.AppointmentDate = appointment.AppointmentDate.AddDays(1);
            if (await _holidayRepo.IsHolidayAsync(appointment.AppointmentDate, agency.Id))
                continue; // lewati kalau hari libur
            countToday = await _appointmentRepo.CountByDateAsync(agency.Id, appointment.AppointmentDate);
        }

        // ✅ Generate TokenNumber
        var nextNumber = countToday + 1;
        appointment.TokenNumber = $"{agency.Id}-{appointment.AppointmentDate:yyyyMMdd}-{nextNumber:D3}";

        // ✅ Simpan appointment
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
