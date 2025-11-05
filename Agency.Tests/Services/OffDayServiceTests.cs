using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Agency.Application.DTOs;
using Agency.Application.Interfaces.Repositories;
using Agency.Application.Services;
using Agency.Domain.Entities;

namespace Agency.Tests.Services
{
    public class OffDayServiceTests
    {
        private readonly Mock<IOffDayRepository> _repoMock;
        private readonly OffDayService _service;

        public OffDayServiceTests()
        {
            _repoMock = new Mock<IOffDayRepository>();
            _service = new OffDayService(_repoMock.Object);
        }

        [Fact]
        public async Task GetByAgencyIdAsync_ShouldReturnListOfOffDays()
        {
            // Arrange
            int agencyId = 10;
            var offDays = new List<OffDay>
            {
                new OffDay { Id = 1, AgencyId = agencyId, Date = new DateTime(2025, 12, 25), Reason = "Christmas" },
                new OffDay { Id = 2, AgencyId = agencyId, Date = new DateTime(2025, 12, 26), Reason = "Boxing Day" }
            };

            _repoMock.Setup(r => r.GetByAgencyIdAsync(agencyId))
                .ReturnsAsync(offDays);

            // Act
            var result = await _service.GetByAgencyIdAsync(agencyId);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                d => Assert.Equal("Christmas", d.Reason),
                d => Assert.Equal("Boxing Day", d.Reason));
            _repoMock.Verify(r => r.GetByAgencyIdAsync(agencyId), Times.Once);
        }

        [Fact]
        public async Task CreateOffDayAsync_ShouldReturnCreatedOffDay()
        {
            // Arrange
            var request = new CreateOffDayRequest
            {
                AgencyId = 5,
                Date = new DateTime(2025, 11, 15),
                Reason = "Maintenance"
            };

            var expectedOffDay = new OffDay
            {
                Id = 123,
                AgencyId = request.AgencyId,
                Date = request.Date,
                Reason = request.Reason
            };

            _repoMock.Setup(r => r.AddSync(It.IsAny<OffDay>()))
                .ReturnsAsync(expectedOffDay);

            // Act
            var result = await _service.CreateOffDayAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOffDay.Id, result.Id);
            Assert.Equal(expectedOffDay.AgencyId, result.AgencyId);
            Assert.Equal(expectedOffDay.Date, result.Date);
            Assert.Equal(expectedOffDay.Reason, result.Reason);
            _repoMock.Verify(r => r.AddSync(It.Is<OffDay>(d =>
                d.AgencyId == request.AgencyId &&
                d.Date == request.Date &&
                d.Reason == request.Reason
            )), Times.Once);
        }

        [Fact]
        public async Task CreateOffDayAsync_ShouldThrow_WhenRepositoryThrows()
        {
            // Arrange
            var request = new CreateOffDayRequest
            {
                AgencyId = 99,
                Date = DateTime.Today,
                Reason = "Invalid agency"
            };

            _repoMock.Setup(r => r.AddSync(It.IsAny<OffDay>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.CreateOffDayAsync(request));
            Assert.Equal("Database error", ex.Message);
            _repoMock.Verify(r => r.AddSync(It.IsAny<OffDay>()), Times.Once);
        }
    }
}
