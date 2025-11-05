using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agency.Api.Controllers;
using Agency.Application.DTOs;
using Agency.Application.Services;
using Agency.Domain.Entities;
using Agency.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Agency.Tests.Controllers
{
    public class AppointmentControllerTests
    {
        private readonly Mock<IAppointmentService> _mockService;
        private readonly AppointmentController _controller;

        public AppointmentControllerTests()
        {
            _mockService = new Mock<IAppointmentService>();
            _controller = new AppointmentController(_mockService.Object);
        }

        // ✅ Test: Create Appointment
        [Fact]
        public async Task Create_ShouldReturnOk_WithCreatedAppointment()
        {
            // Arrange
            var request = new CreateAppointmentRequest
            {
                AgencyId = 1,
                CustomerName = "John Doe",
                Date = DateTime.Today
            };

            var expected = new Appointment
            {
                Id = 1,
                AgencyId = 1,
                CustomerName = "John Doe",
                AppointmentDate = DateTime.Today
            };

            _mockService
                .Setup(s => s.CreateAppointmentAsync(request))
                .ReturnsAsync(expected);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<Appointment>(okResult.Value);
            Assert.Equal(expected.Id, returned.Id);
            Assert.Equal(expected.CustomerName, returned.CustomerName);
        }

        // ✅ Test: Get All Appointments
        [Fact]
        public async Task GetAll_ShouldReturnOk_WithAppointmentsList()
        {
            // Arrange
            int agencyId = 1;
            var expectedList = new List<Appointment>
            {
                new Appointment { Id = 1, AgencyId = agencyId, CustomerName = "Alice" },
                new Appointment { Id = 2, AgencyId = agencyId, CustomerName = "Bob" }
            };

            _mockService
                .Setup(s => s.GetAppointmentsAsync(agencyId))
                .ReturnsAsync(expectedList);

            // Act
            var result = await _controller.GetAll(agencyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedList = Assert.IsAssignableFrom<IEnumerable<Appointment>>(okResult.Value);
            Assert.Collection(returnedList,
                item => Assert.Equal("Alice", item.CustomerName),
                item => Assert.Equal("Bob", item.CustomerName));
        }

        // ✅ Test: Get Daily Queue
        [Fact]
        public async Task GetDailyQueue_ShouldReturnOk_WithQueueList()
        {
            // Arrange
            var date = DateTime.Today;
            var expectedQueue = new List<CustomerAppointment>
            {
                new CustomerAppointment { Id = 1, CustomerName = "Charlie" },
                new CustomerAppointment { Id = 2, CustomerName = "Diana" }
            };

            _mockService
                .Setup(s => s.GetDailyQueue(date))
                .ReturnsAsync(expectedQueue);

            // Act
            var result = await _controller.GetDailyQueue(date);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedQueue = Assert.IsAssignableFrom<IEnumerable<CustomerAppointment>>(okResult.Value);
            Assert.Equal(2, ((List<CustomerAppointment>)returnedQueue).Count);
        }
    }
}
