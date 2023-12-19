using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Implementations;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.UnitTests
{
    public class RequestServiceTests
    {
        [Fact]
        public async Task AddAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            var requestService = new RequestService(mockRepository.Object);

            var requestDto = new ModifyRequestDTO
            {
                GoodId = Guid.NewGuid(),
                CustomerId = "12345",
                Amount = 5,
                Date = DateTime.Now,
                ExpectedPrice = 20.0m,
                IsActive = true
            };

            var result = await requestService.AddAsync(requestDto);

            Assert.True(result.Status == Status.Ok);
        }

        [Fact]
        public async Task AddAsync_InvalidInput_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            var requestService = new RequestService(mockRepository.Object);

            var invalidDto = new ModifyRequestDTO
            {
                GoodId = Guid.Empty,
                CustomerId = "",
                Amount = 0,
                Date = DateTime.MinValue,
                ExpectedPrice = -5.0m,
                IsActive = true
            };

            var result = await requestService.AddAsync(invalidDto);

            Assert.True(result.Status == Status.Error);
        }

        [Fact]
        public async Task GetAllAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(new List<RequestDTO> {
                new RequestDTO
                {
                    GoodId = Guid.NewGuid(),
                    CustomerId = "12345",
                    Amount = 5,
                    Date = DateTime.Now,
                    ExpectedPrice = 20.0m,
                    IsActive = true
                }
                });

            var requestService = new RequestService(mockRepository.Object);

            var result = await requestService.GetAllAsync();

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public async Task GetAllAsync_InvalidPagination_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            var requestService = new RequestService(mockRepository.Object);

            var result = await requestService.GetAllAsync(pageNumber: 0, pageSize: 0);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }
        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new RequestDTO
                {
                    GoodId = Guid.NewGuid(),
                    CustomerId = "12345",
                    Amount = 5,
                    Date = DateTime.Now,
                    ExpectedPrice = 20.0m,
                    IsActive = true
                });

            var requestService = new RequestService(mockRepository.Object);
            var existingId = Guid.NewGuid();

            var result = await requestService.GetByIdAsync(existingId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }
        [Fact]
        public async Task GetByIdAsync_InValidId_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((RequestDTO)null);

            var requestService = new RequestService(mockRepository.Object);
            var existingId = Guid.NewGuid();

            var result = await requestService.GetByIdAsync(existingId);

            Assert.True(result.Status == Status.Error);
            Assert.NotNull(result.Description);
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ModifyRequestDTO>()))
                .ReturnsAsync(new RequestDTO
                {
                    GoodId = Guid.NewGuid(),
                    CustomerId = "12345",
                    Amount = 5,
                    Date = DateTime.Now,
                    ExpectedPrice = 20.0m,
                    IsActive = true
                });

            var requestService = new RequestService(mockRepository.Object);
            var existingId = Guid.NewGuid();
            var updatedDto = new ModifyRequestDTO
            {
                GoodId = Guid.NewGuid(),
                CustomerId = "54321",
                Amount = 8,
                Date = DateTime.Now.AddDays(1),
                ExpectedPrice = 30.0m,
                IsActive = false
            };

            var result = await requestService.UpdateAsync(existingId, updatedDto);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task UpdateAsync_InvalidInput_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            var requestService = new RequestService(mockRepository.Object);
            var existingId = Guid.NewGuid();
            var invalidDto = new ModifyRequestDTO
            {
                GoodId = Guid.Empty,
                CustomerId = "",
                Amount = 0,
                Date = DateTime.MinValue,
                ExpectedPrice = -5.0m,
                IsActive = true
            };

            var result = await requestService.UpdateAsync(existingId, invalidDto);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }
        [Fact]
        public async Task DeleteAsync_ExistingId_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IRequestRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new RequestDTO
                {
                    GoodId = Guid.NewGuid(),
                    CustomerId = "12345",
                    Amount = 5,
                    Date = DateTime.Now,
                    ExpectedPrice = 20.0m,
                    IsActive = true
                });

            var requestService = new RequestService(mockRepository.Object);
            var existingId = Guid.NewGuid();

            var result = await requestService.DeleteAsync(existingId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }
    }
}
