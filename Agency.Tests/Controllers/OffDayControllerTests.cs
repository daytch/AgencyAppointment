using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Agency.Api.Controllers;
using Agency.Application.DTOs;
using Agency.Application.Interfaces.Services;
using Agency.Domain.Entities;

namespace Agency.Tests.Controllers
{
    public class OffDayControllerTests
    {
        private readonly Mock<IOffDayService> _serviceMock;
        private readonly OffDayController _controller;

        public OffDayControllerTests()
        {
            _serviceMock = new Mock<IOffDayService>();
            _controller = new OffDayController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetByAgency_ShouldReturnOk_WithListOfOffDays()
        {
            // Arrange
            int agencyId = 1;
            var offDays = new List<OffDay>
            {
                new OffDay { Id = 1, AgencyId = agencyId, Date = new DateTime(2025, 12, 25) },
                new OffDay { Id = 2, AgencyId = agencyId, Date = new DateTime(2025, 12, 26) }
            };

            _serviceMock.Setup(s => s.GetByAgencyIdAsync(agencyId))
                .ReturnsAsync(offDays);

            // Act
            var result = await _controller.GetByAgency(agencyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<OffDay>>(okResult.Value);
            Assert.Collection(value,
                d => Assert.Equal(new DateTime(2025, 12, 25), d.Date),
                d => Assert.Equal(new DateTime(2025, 12, 26), d.Date));

            _serviceMock.Verify(s => s.GetByAgencyIdAsync(agencyId), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WithCreatedOffDay()
        {
            // Arrange
            var request = new CreateOffDayRequest
            {
                AgencyId = 2,
                Date = new DateTime(2025, 11, 15),
                Reason = "National Holiday"
            };

            var created = new OffDay
            {
                Id = 10,
                AgencyId = request.AgencyId,
                Date = request.Date,
                Reason = request.Reason
            };

            _serviceMock.Setup(s => s.CreateOffDayAsync(request))
                .ReturnsAsync(created);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<OffDay>(okResult.Value);
            Assert.Equal(10, value.Id);
            Assert.Equal(request.Date, value.Date);
            Assert.Equal(request.Reason, value.Reason);
            _serviceMock.Verify(s => s.CreateOffDayAsync(request), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldThrowException_WhenServiceFails()
        {
            // Arrange
            var request = new CreateOffDayRequest
            {
                AgencyId = 99,
                Date = DateTime.Today,
                Reason = "Invalid Agency"
            };

            _serviceMock.Setup(s => s.CreateOffDayAsync(request))
                .ThrowsAsync(new Exception("Agency not found"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _controller.Create(request));
            Assert.Equal("Agency not found", ex.Message);
            _serviceMock.Verify(s => s.CreateOffDayAsync(request), Times.Once);
        }
    }
}
