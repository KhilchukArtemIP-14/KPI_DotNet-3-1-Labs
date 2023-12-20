using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.UnitTests
{
    public class PurchaseRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ValidInput_ReturnsNewPurchaseId()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(AddAsync_ValidInput_ReturnsNewPurchaseId));
            var repository = new PurchaseRepository(dbContext);

            var purchaseDto = new CreatePurchaseDTO
            {
                UserId = "user123",
                StaffRepId = "staff456",
                Date = DateTime.Now,
                PurchaseGoodDTOs = new List<CreatePurchaseGoodDTO>
            {
                new CreatePurchaseGoodDTO { GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"), Amount = 2 },
            }
            };

            var result = await repository.AddAsync(purchaseDto);

            Assert.NotEqual(Guid.Empty, result);
            var addedPurchase = await dbContext.Purchases
                .Include(p => p.PurchaseGoods)
                .FirstOrDefaultAsync(p => p.Id == result);

            Assert.NotNull(addedPurchase);
            Assert.Equal(purchaseDto.UserId, addedPurchase.UserId);
            Assert.Equal(purchaseDto.StaffRepId, addedPurchase.StaffRepId);
            Assert.Equal(purchaseDto.Date, addedPurchase.Date);

            Assert.Equal(purchaseDto.PurchaseGoodDTOs.Count(), addedPurchase.PurchaseGoods.Count());
            foreach (var expectedPurchaseGood in purchaseDto.PurchaseGoodDTOs)
            {
                var addedPurchaseGood = addedPurchase.PurchaseGoods.FirstOrDefault(pg =>
                    pg.GoodId == expectedPurchaseGood.GoodId && pg.Amount == expectedPurchaseGood.Amount);

                Assert.NotNull(addedPurchaseGood);
            }
        }
        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsPurchaseDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetByIdAsync_ExistingId_ReturnsPurchaseDTO));
            var repository = new PurchaseRepository(dbContext);
            var existingPurchaseId = Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c");

            var result = await repository.GetByIdAsync(existingPurchaseId);

            Assert.NotNull(result);
            Assert.Equal("646e2574-2314-47f6-b0a8-51c50c57b06f", result.UserId);
            Assert.Equal("656e2574-2314-47f6-b0a8-51c50c57b06f", result.StaffRepId);
            Assert.NotEmpty(result.PurchaseGoodDTOs);

            var expectedGoods = DbContextMocker.GetSeedingPurchaseGoods()
                .Where(pg => pg.PurchaseId == existingPurchaseId)
                .ToList();
            Assert.Equal(expectedGoods.Count, result.PurchaseGoodDTOs.Count());
            foreach (var expectedGood in expectedGoods)
            {
                var actualGood = result.PurchaseGoodDTOs.FirstOrDefault(g =>
                    g.GoodId == expectedGood.GoodId && g.Amount == expectedGood.Amount);

                Assert.NotNull(actualGood);
            }
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetByIdAsync_NonExistingId_ReturnsNull));
            var repository = new PurchaseRepository(dbContext);

            var result = await repository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }
        [Fact]
        public async Task GetAllAsync_NoUserId_ReturnsAllPurchases()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_NoUserId_ReturnsAllPurchases));
            var repository = new PurchaseRepository(dbContext);

            var result = await repository.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_WithUserId_ReturnsPurchasesForUser()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_WithUserId_ReturnsPurchasesForUser));
            var repository = new PurchaseRepository(dbContext);

            var result = await repository.GetAllAsync(userId: "646e2574-2314-47f6-b0a8-51c50c57b06f");

            Assert.NotNull(result);
            Assert.All(result, purchase => Assert.Equal("646e2574-2314-47f6-b0a8-51c50c57b06f", purchase.UserId));
        }

        [Fact]
        public async Task GetAllAsync_Paginated_ReturnsCorrectPageSize()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_Paginated_ReturnsCorrectPageSize));
            var repository = new PurchaseRepository(dbContext);

            var result = await repository.GetAllAsync(pageSize: 2);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAsync_Paginated_ReturnsCorrectPageNumber()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_Paginated_ReturnsCorrectPageNumber));
            var repository = new PurchaseRepository(dbContext);

            var result = await repository.GetAllAsync(pageNumber: 2, pageSize: 2);

            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }
    }
}
