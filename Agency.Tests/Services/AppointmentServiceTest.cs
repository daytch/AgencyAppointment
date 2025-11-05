using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Agency.Application.Services;
using Agency.Application.Interfaces.Repositories;
using Agency.Domain.Entities;

using DomainAgency = Agency.Domain.Entities.Agency;
using Agency.Application.DTOs;

namespace Agency.Tests.Services
{
    public class AppointmentServiceTests
    {
        private readonly Mock<IAppointmentRepository> _appointmentRepoMock;
        private readonly Mock<IAgencyRepository> _agencyRepoMock;
        private readonly Mock<IOffDayRepository> _offDayRepoMock;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            _appointmentRepoMock = new Mock<IAppointmentRepository>();
            _agencyRepoMock = new Mock<IAgencyRepository>();
            _offDayRepoMock = new Mock<IOffDayRepository>();

            _service = new AppointmentService(
                _appointmentRepoMock.Object,
                _agencyRepoMock.Object,
                _offDayRepoMock.Object
            );
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldThrow_WhenAgencyNotFound()
        {
            // Arrange
            var appointment = new CreateAppointmentRequest { AgencyId = 999, Date = DateTime.Now };

            _agencyRepoMock
                .Setup(r => r.GetByIdAsync(appointment.AgencyId))
                .ReturnsAsync((DomainAgency)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.CreateAppointmentAsync(appointment)
            );

            Assert.Equal("Agency not found", ex.Message);
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldThrow_WhenHoliday()
        {
            var agency = new DomainAgency { Id = 1, MaxAppointmentsPerDay = 5 };
            var appointment = new CreateAppointmentRequest
            {
                AgencyId = 1,
                Date = new DateTime(2025, 11, 10)
            };

            _agencyRepoMock.Setup(r => r.GetByIdAsync(agency.Id)).ReturnsAsync(agency);
            _offDayRepoMock.Setup(r => r.IsHolidayAsync(appointment.Date, agency.Id))
                .ReturnsAsync(true);

            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.CreateAppointmentAsync(appointment)
            );

            Assert.Equal("Cannot book on a holiday", ex.Message);
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldShiftDate_WhenFull()
        {
            // Arrange
            var agency = new DomainAgency { Id = 1, MaxAppointmentsPerDay = 2 };
            var initialDate = new DateTime(2025, 11, 11);
            var appointment = new CreateAppointmentRequest
            {
                AgencyId = agency.Id,
                Date = initialDate
            };

            _agencyRepoMock.Setup(r => r.GetByIdAsync(agency.Id)).ReturnsAsync(agency);

            // Hari pertama penuh
            _offDayRepoMock.Setup(r => r.IsHolidayAsync(initialDate, agency.Id)).ReturnsAsync(false);
            _appointmentRepoMock.Setup(r => r.CountByDateAsync(agency.Id, initialDate)).ReturnsAsync(2);

            // Hari kedua tidak libur & masih ada slot
            var nextDate = initialDate.AddDays(1);
            _offDayRepoMock.Setup(r => r.IsHolidayAsync(nextDate, agency.Id)).ReturnsAsync(false);
            _appointmentRepoMock.Setup(r => r.CountByDateAsync(agency.Id, nextDate)).ReturnsAsync(0);

            _appointmentRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment a) => a);

            // Act
            var result = await _service.CreateAppointmentAsync(appointment);

            // Assert
            Assert.Equal(nextDate.Date, result.AppointmentDate.Date);
            Assert.StartsWith($"{agency.Id}-{nextDate:yyyyMMdd}-", result.TokenNumber);
        }

        [Fact]
        public async Task CreateAppointmentAsync_ShouldReturnAppointmentWithToken_WhenSuccess()
        {
            var agency = new DomainAgency { Id = 10, MaxAppointmentsPerDay = 3 };
            var date = new DateTime(2025, 11, 15);
            var appointment = new CreateAppointmentRequest { AgencyId = 10, Date = date };

            _agencyRepoMock.Setup(r => r.GetByIdAsync(agency.Id)).ReturnsAsync(agency);
            _offDayRepoMock.Setup(r => r.IsHolidayAsync(date, agency.Id)).ReturnsAsync(false);
            _appointmentRepoMock.Setup(r => r.CountByDateAsync(agency.Id, date)).ReturnsAsync(1);
            _appointmentRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Appointment>()))
                .ReturnsAsync((Appointment a) => a);

            // Act
            var result = await _service.CreateAppointmentAsync(appointment);

            // Assert
            Assert.Equal($"{agency.Id}-{date:yyyyMMdd}-002", result.TokenNumber);
            Assert.Equal(agency.Id, result.AgencyId);
        }

        [Fact]
        public async Task GetAppointmentsAsync_ShouldCallRepository()
        {
            var agencyId = 5;
            var expected = new List<Appointment> { new Appointment { AgencyId = agencyId } };

            _appointmentRepoMock.Setup(r => r.GetByAgencyIdAsync(agencyId))
                .ReturnsAsync(expected);

            var result = await _service.GetAppointmentsAsync(agencyId);

            Assert.Single(result);
            _appointmentRepoMock.Verify(r => r.GetByAgencyIdAsync(agencyId), Times.Once);
        }

        [Fact]
        public async Task GetDailyQueue_ShouldCallRepository()
        {
            var date = DateTime.Today;
            var expected = new List<CustomerAppointment>();

            _appointmentRepoMock.Setup(r => r.GetDailyQueueAsync(date)).ReturnsAsync(expected);

            var result = await _service.GetDailyQueue(date);

            Assert.Same(expected, result);
            _appointmentRepoMock.Verify(r => r.GetDailyQueueAsync(date), Times.Once);
        }
    }
}
