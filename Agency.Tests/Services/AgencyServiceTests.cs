using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Agency.Application.Services;
using Agency.Application.Interfaces.Repositories;
using Agency.Domain.Entities;

namespace Agency.Tests.Services
{
    public class AgencyServiceTests
    {
        private readonly Mock<IAgencyRepository> _repoMock;
        private readonly AgencyService _service;

        public AgencyServiceTests()
        {
            _repoMock = new Mock<IAgencyRepository>();
            _service = new AgencyService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfAgencies()
        {
            // Arrange
            var agencies = new List<Domain.Entities.Agency>
            {
                new Domain.Entities.Agency { Id = 1, Name = "Agency A" },
                new Domain.Entities.Agency { Id = 2, Name = "Agency B" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(agencies);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                a => Assert.Equal("Agency A", a.Name),
                a => Assert.Equal("Agency B", a.Name));
            _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAgency_WhenFound()
        {
            // Arrange
            var agency = new Domain.Entities.Agency { Id = 10, Name = "Agency X" };
            _repoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(agency);

            // Act
            var result = await _service.GetByIdAsync(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Agency X", result.Name);
            _repoMock.Verify(r => r.GetByIdAsync(10), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Domain.Entities.Agency)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
            _repoMock.Verify(r => r.GetByIdAsync(999), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldCallAddAsync_AndReturnAgency()
        {
            // Arrange
            var agency = new Domain.Entities.Agency { Id = 1, Name = "Agency A" };
            _repoMock.Setup(r => r.AddAsync(agency)).ReturnsAsync(agency);

            // Act
            var result = await _service.CreateAsync(agency);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(agency, result);
            _repoMock.Verify(r => r.AddAsync(agency), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdateAsync()
        {
            // Arrange
            var agency = new Domain.Entities.Agency { Id = 2, Name = "Updated Agency" };
            _repoMock.Setup(r => r.UpdateAsync(agency)).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(agency);

            // Assert
            _repoMock.Verify(r => r.UpdateAsync(agency), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteAsync()
        {
            // Arrange
            var id = 5;
            _repoMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _repoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}
