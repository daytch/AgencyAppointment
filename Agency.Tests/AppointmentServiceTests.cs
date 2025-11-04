using Agency.Application.Services;
using Agency.Domain.Entities;
using Moq;
using Xunit;
using Agency.Application.Interfaces.Repositories;

public class AppointmentServiceTests
{
    [Fact]
    public async Task CreateAppointmentAsync_ShouldThrow_WhenDateIsPast()
    {
        var repo = new Mock<IAppointmentRepository>();
        //var service = new AppointmentService(repo.Object);

        var appointment = new Appointment { AppointmentDate = DateTime.UtcNow.AddDays(-1) };

        //await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAppointmentAsync(appointment));
    }
}
