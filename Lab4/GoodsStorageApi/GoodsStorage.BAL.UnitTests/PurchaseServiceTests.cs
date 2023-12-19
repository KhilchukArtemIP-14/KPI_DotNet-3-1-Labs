using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Implementations;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GoodsStorage.BAL.UnitTests
{
    public class PurchaseServiceTests
    {
        [Fact]
        public async Task AddAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>(); 
            var purchaseService = new PurchaseService(mockRepository.Object);

            var purchaseDto = new CreatePurchaseDTO {
                UserId = "12345", 
                StaffRepId = "67890", 
                Date = DateTime.Now, 
                PurchaseGoodDTOs = new List<CreatePurchaseGoodDTO>
                {
                    new CreatePurchaseGoodDTO
                    {
                    GoodId = Guid.NewGuid(), 
                    Amount = 2 
                    },
                }   
            };

            var result = await purchaseService.AddAsync(purchaseDto);

            Assert.True(result.Status == Status.Ok);
        }

        [Fact]
        public async Task AddAsync_InvalidInput_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>();
            var purchaseService = new PurchaseService(mockRepository.Object);

            var invalidDto = new CreatePurchaseDTO
            {
                UserId = "",
                StaffRepId = "", 
                Date = DateTime.MinValue, 
                PurchaseGoodDTOs = null
            };

            var result = await purchaseService.AddAsync(invalidDto);

            Assert.True(result.Status == Status.Error);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new PurchaseDTO {
                    UserId = "12345",
                    StaffRepId = "67890",
                    Date = DateTime.Now,
                    PurchaseGoodDTOs = new List<PurchaseGoodDTO>
                    {
                        new PurchaseGoodDTO
                        {
                            GoodId = Guid.NewGuid(),
                            Amount = 2
                        },
                    }
                });

            var purchaseService = new PurchaseService(mockRepository.Object);
            var existingId = Guid.NewGuid();

            var result = await purchaseService.GetByIdAsync(existingId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }
        [Fact]
        public async Task GetAllAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new List<PurchaseDTO> {
                    new PurchaseDTO {
                    UserId = "12345",
                    StaffRepId = "67890",
                    Date = DateTime.Now,
                    PurchaseGoodDTOs = new List<PurchaseGoodDTO>
                    {
                        new PurchaseGoodDTO
                        {
                            GoodId = Guid.NewGuid(),
                            Amount = 2
                        },
                    }
                    }
                });

            var purchaseService = new PurchaseService(mockRepository.Object);

            var result = await purchaseService.GetAllAsync();

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task GetAllAsync_InvalidPagination_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>();
            var purchaseService = new PurchaseService(mockRepository.Object);

            var result = await purchaseService.GetAllAsync(pageNumber: 0, pageSize: 0);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((PurchaseDTO)null);

            var purchaseService = new PurchaseService(mockRepository.Object);
            var nonExistingId = Guid.NewGuid();

            var result = await purchaseService.GetByIdAsync(nonExistingId);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IPurchaseRepository>();
            var purchaseService = new PurchaseService(mockRepository.Object);

            var result = await purchaseService.GetByIdAsync(Guid.Empty);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }
    }
}
