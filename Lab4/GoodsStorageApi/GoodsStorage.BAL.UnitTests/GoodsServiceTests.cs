using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodsStorage.BAL;
using GoodsStorage.BAL.Services.Implementations;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using GoodsStorage.BAL.Models;
using Moq;
using Xunit;

namespace GoodsStorage.BAL.UnitTests
{
    public class GoodsServiceTests
    {
        [Fact]
        public async Task AddAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            var goodsService = new GoodsService(mockRepository.Object);

            var goodDto = new ModifyGoodDTO {
                Name = "Product",
                Description = "Product description",
                Unit = "Piece",
                Price = 10.99m,
                AvailableAmount = 50
            };

            var result = await goodsService.AddAsync(goodDto);

            Assert.True(result.Status == Status.Ok);
        }

        [Fact]
        public async Task AddAsync_InvalidInput_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            var goodsService = new GoodsService(mockRepository.Object);

            var invalidDto = new ModifyGoodDTO
            {
                Name = "",
                Description = "",
                Unit = "",
                Price = -5.0m,
                AvailableAmount = -10
            };

            var result = await goodsService.AddAsync(invalidDto);

            Assert.True(result.Status == Status.Error);
        }

        [Fact]
        public async Task DeleteAsync_ExistingId_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new GoodDTO
                {
                    Name = "Product",
                    Description = "Product description",
                    Unit = "Piece",
                    Price = 10.99m,
                    AvailableAmount = 50
                });

            var goodsService = new GoodsService(mockRepository.Object);
            var existingId = Guid.NewGuid();

            var result = await goodsService.DeleteAsync(existingId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((GoodDTO)null);

            var goodsService = new GoodsService(mockRepository.Object);
            var nonExistingId = Guid.NewGuid();

            var result = await goodsService.DeleteAsync(nonExistingId);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetAllAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            mockRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<GoodDTO> { 
                    new GoodDTO() { 
                        Name = "Product1",
                        Description = "product description",
                        Unit = "Piece",
                        Price = 10.99m,
                        AvailableAmount = 50
                    },
                    new GoodDTO() {
                        Name = "Product2",
                        Description = "Product description",
                        Unit = "Piece",
                        Price = 10.99m,
                        AvailableAmount = 50
                    }
             });

            var goodsService = new GoodsService(mockRepository.Object);

            var result = await goodsService.GetAllAsync();

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
        }
        [Fact]
        public async Task GetAllAsync_InvalidPagination_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            var goodsService = new GoodsService(mockRepository.Object);

            var result = await goodsService.GetAllAsync(pageNumber: 0, pageSize: 0);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new GoodDTO
                {
                    Name = "Product",
                    Description = "product description",
                    Unit = "Piece",
                    Price = 10.99m,
                    AvailableAmount = 50
                });

            var goodsService = new GoodsService(mockRepository.Object);
            var existingId = Guid.NewGuid();

            var result = await goodsService.GetByIdAsync(existingId);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }
        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((GoodDTO)null);

            var goodsService = new GoodsService(mockRepository.Object);
            var nonExistingId = Guid.NewGuid();

            var result = await goodsService.GetByIdAsync(nonExistingId);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsSuccessResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Guid>(), It.IsAny<ModifyGoodDTO>()))
                .ReturnsAsync(new GoodDTO
                {
                    Name = "Product1",
                    Description = "product description",
                    Unit = "Piece",
                    Price = 10.99m,
                    AvailableAmount = 50
                });

            var goodsService = new GoodsService(mockRepository.Object);
            var existingId = Guid.NewGuid();
            var updatedDto = new ModifyGoodDTO
            {
                Name = "Product2",
                Description = "product description2",
                Unit = "Pieces",
                Price = 11.99m,
                AvailableAmount = 51
            };

            var result = await goodsService.UpdateAsync(existingId, updatedDto);

            Assert.True(result.Status == Status.Ok);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task UpdateAsync_InvalidInput_ReturnsErrorResponse()
        {
            var mockRepository = new Mock<IGoodRepository>();
            var goodsService = new GoodsService(mockRepository.Object);
            var existingId = Guid.NewGuid();
            var invalidDto = new ModifyGoodDTO
            {
                Name = "Pr",
                Description = "Desc",
                Unit = "",
                Price = -5.0m,
                AvailableAmount = -10
            };

            var result = await goodsService.UpdateAsync(existingId, invalidDto);

            Assert.True(result.Status == Status.Error);
            Assert.Null(result.Data);
        }

    }
}