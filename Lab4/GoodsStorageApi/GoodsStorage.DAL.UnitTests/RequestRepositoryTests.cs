using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using GoodsStorage.DAL.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Castle.Core.Resource;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Rewrite;

namespace GoodsStorage.DAL.UnitTests
{
    public class RequestRepositoryTests
    {

        [Fact]
        public async Task AddAsync_ValidInput_ReturnsNewRequestId()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(AddAsync_ValidInput_ReturnsNewRequestId));
            var repository = new RequestRepository(dbContext);

            var requestDto = new ModifyRequestDTO
            {
                GoodId = Guid.NewGuid(),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 12,
                Date = DateTime.Now,
                ExpectedPrice = 999.99m,
                IsActive = true,
            };

            var result = await repository.AddAsync(requestDto);
            var savedEntity = dbContext.Requests.Find(result);

            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(requestDto.GoodId, savedEntity.GoodId);
            Assert.Equal(requestDto.Amount, savedEntity.Amount);
            Assert.Equal(requestDto.Date, savedEntity.Date);
            Assert.Equal(requestDto.ExpectedPrice, savedEntity.ExpectedPrice);
            Assert.True(savedEntity.IsActive);
            Assert.Equal("646e2574-2314-47f6-b0a8-51c50c57b06f", savedEntity.CustomerId);
        }
        [Fact]
        public async Task DeleteAsync_ExistingRequest_ReturnsDeletedRequestDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(DeleteAsync_ExistingRequest_ReturnsDeletedRequestDTO));
            var repository = new RequestRepository(dbContext);
            var initialRequest = new Request
            {
                GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 12,
                Date = DateTime.Now,
                ExpectedPrice = 999.99m,
                IsActive = true,
                IsDeleted = false,
            };
            await dbContext.Requests.AddAsync(initialRequest);
            await dbContext.SaveChangesAsync();

            var result = await repository.DeleteAsync(initialRequest.Id);

            Assert.NotNull(result);
            Assert.Equal(initialRequest.GoodId, result.GoodId);
            Assert.Equal(initialRequest.Amount, result.Amount);
            Assert.Equal(initialRequest.Date, result.Date);
            Assert.Equal(initialRequest.ExpectedPrice, result.ExpectedPrice);
            Assert.Equal(initialRequest.IsActive, result.IsActive);

            Assert.Null(await dbContext.Requests.Where(r=>!r.IsDeleted).FirstOrDefaultAsync(r=>r.Id == initialRequest.Id));
        }

        [Fact]
        public async Task DeleteAsync_NonExistingRequest_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(DeleteAsync_NonExistingRequest_ReturnsNull));
            var repository = new RequestRepository(dbContext);

            var result = await repository.DeleteAsync(Guid.NewGuid());

            Assert.Null(result);
        }
        [Fact]
        public async Task GetByIdAsync_ExistingRequest_ReturnsRequestDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetByIdAsync_ExistingRequest_ReturnsRequestDTO));
            var repository = new RequestRepository(dbContext);
            var initialRequest = new Request
            {
                GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 12,
                Date = DateTime.Now,
                ExpectedPrice = 999.99m,
                IsActive = true,
                IsDeleted = false,
            };
            await dbContext.Requests.AddAsync(initialRequest);
            await dbContext.SaveChangesAsync();


            var result = await repository.GetByIdAsync(initialRequest.Id);

            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"), result.GoodId);
            Assert.Equal("646e2574-2314-47f6-b0a8-51c50c57b06f", result.CustomerId);
            Assert.Equal(12, result.Amount);
            Assert.Equal(999.99m, result.ExpectedPrice);
            Assert.True(result.IsActive);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingRequest_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetByIdAsync_NonExistingRequest_ReturnsNull));
            var repository = new RequestRepository(dbContext);

            var result = await repository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ExistingRequest_ReturnsUpdatedRequestDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(UpdateAsync_ExistingRequest_ReturnsUpdatedRequestDTO));
            var repository = new RequestRepository(dbContext);
            var initialRequest = new Request
            {
                GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 12,
                Date = DateTime.Now,
                ExpectedPrice = 999.99m,
                IsActive = true,
                IsDeleted = false,
            };
            await dbContext.Requests.AddAsync(initialRequest);
            await dbContext.SaveChangesAsync();
            var updatedDto = new ModifyRequestDTO
            {
                GoodId = Guid.Parse("6df9da5a-ac0d-4650-ba7f-dd7b3fde6e98"), 
                CustomerId = "123e2574-2314-47f6-b0a8-51c50c57b06f", 
                Amount = 15, 
                Date = DateTime.Now.AddDays(1), 
                ExpectedPrice = 888.88m,
                IsActive = false 
            };

            var result = await repository.UpdateAsync(initialRequest.Id, updatedDto);

            Assert.NotNull(result);
            Assert.Equal(updatedDto.GoodId, result.GoodId);
            Assert.Equal(updatedDto.CustomerId, result.CustomerId);
            Assert.Equal(updatedDto.Amount, result.Amount);
            Assert.Equal(updatedDto.Date, result.Date);
            Assert.Equal(updatedDto.ExpectedPrice, result.ExpectedPrice);
            Assert.Equal(updatedDto.IsActive, result.IsActive);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingRequest_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(UpdateAsync_NonExistingRequest_ReturnsNull));
            var repository = new RequestRepository(dbContext);

            var updatedDto = new ModifyRequestDTO
            {
                GoodId = Guid.NewGuid(),
                CustomerId = "123e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 15,
                Date = DateTime.Now.AddDays(1),
                ExpectedPrice = 888.88m,
                IsActive = false
            };

            var result = await repository.UpdateAsync(Guid.NewGuid(), updatedDto);

            Assert.Null(result);

        }

        [Fact]
        public async Task GetAllAsync_FilterByUserId_ReturnsRequestsForUser()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_FilterByUserId_ReturnsRequestsForUser));
            var repository = new RequestRepository(dbContext);

            var userId = "646e2574-2314-47f6-b0a8-51c50c57b06f";
            var result = await repository.GetAllAsync(userId: userId);

            Assert.All(result, r => Assert.Equal(userId, r.CustomerId));
        }

        [Fact]
        public async Task GetAllAsync_ActiveOnly_ReturnsOnlyActiveRequests()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_ActiveOnly_ReturnsOnlyActiveRequests));
            var repository = new RequestRepository(dbContext);

            var result = await repository.GetAllAsync(activeOnly: true);

            Assert.All(result, r => Assert.True(r.IsActive));
        }

        [Fact]
        public async Task GetAllAsync_NoData_ReturnsEmptyList()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_NoData_ReturnsEmptyList));
            var repository = new RequestRepository(dbContext);

            var result = await repository.GetAllAsync(pageNumber: 10, pageSize: 100);

            Assert.Empty(result);
        }
    }
}
