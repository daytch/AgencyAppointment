using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Agency.Api.Controllers;
using Agency.Application.Interfaces.Services;
using Agency.Domain.Entities;

namespace Agency.Tests.Controllers
{
    public class AgencyControllerTests
    {
        private readonly Mock<IAgencyService> _serviceMock;
        private readonly AgencyController _controller;

        public AgencyControllerTests()
        {
            _serviceMock = new Mock<IAgencyService>();
            _controller = new AgencyController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenNameIsEmpty()
        {
            // Arrange
            var agency = new Domain.Entities.Agency { Name = "" };

            // Act
            var result = await _controller.Create(agency);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Agency name is required", badRequest.Value);
        }

        [Fact]
        public async Task Create_ShouldReturnOk_WhenAgencyIsCreated()
        {
            // Arrange
            var agency = new Domain.Entities.Agency { Id = 1, Name = "Agency A", MaxAppointmentsPerDay = 10 };
            _serviceMock.Setup(s => s.CreateAsync(It.IsAny<Domain.Entities.Agency>()))
                .ReturnsAsync(agency);

            // Act
            var result = await _controller.Create(agency);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<Domain.Entities.Agency>(okResult.Value);
            Assert.Equal(1, value.Id);
            Assert.Equal("Agency A", value.Name);
            _serviceMock.Verify(s => s.CreateAsync(It.IsAny<Domain.Entities.Agency>()), Times.Once);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithListOfAgencies()
        {
            // Arrange
            var agencies = new List<Domain.Entities.Agency>
            {
                new Domain.Entities.Agency { Id = 1, Name = "Agency A" },
                new Domain.Entities.Agency { Id = 2, Name = "Agency B" }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(agencies);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Domain.Entities.Agency>>(okResult.Value);
            Assert.Collection(value,
                a => Assert.Equal("Agency A", a.Name),
                a => Assert.Equal("Agency B", a.Name));
            _serviceMock.Verify(s => s.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_ShouldReturnServerError_WhenServiceThrowsException()
        {
            // Arrange
            var agency = new Domain.Entities.Agency { Name = "Broken Agency" };

            _serviceMock
                .Setup(s => s.CreateAsync(It.IsAny<Domain.Entities.Agency>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _controller.Create(agency));
        }
    }
}
