using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.Domain;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GoodsStorage.DAL.UnitTests
{
    public class GoodRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ValidInput_ReturnsNewGoodId()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(AddAsync_ValidInput_ReturnsNewGoodId));
            var repository = new GoodRepository(dbContext);

            var goodDto = new ModifyGoodDTO
            {
                Name = "Test Good",
                Description = "Test Description",
                Unit = "Test Unit",
                Price = 10.0m,
                AvailableAmount = 50
            };

            var result = await repository.AddAsync(goodDto);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task DeleteAsync_ExistingGood_ReturnsDeletedGoodDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(DeleteAsync_ExistingGood_ReturnsDeletedGoodDTO));
            var repository = new GoodRepository(dbContext);

            var existingGoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48");

            var result = await repository.DeleteAsync(existingGoodId);

            Assert.NotNull(result);
            Assert.Equal("Good 1", result.Name);
        }
        [Fact]
        public async Task DeleteAsync_NonExistingGood_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(DeleteAsync_NonExistingGood_ReturnsNull));
            var repository = new GoodRepository(dbContext);

            var nonExistingGoodId = Guid.NewGuid(); 

            var result = await repository.DeleteAsync(nonExistingGoodId);

            Assert.Null(result);
        }
        [Fact]
        public async Task UpdateAsync_ExistingGood_ReturnsUpdatedGoodDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(UpdateAsync_ExistingGood_ReturnsUpdatedGoodDTO));
            var repository = new GoodRepository(dbContext);

            var existingGoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48");
            var updatedGoodDto = new ModifyGoodDTO
            {
                Name = "Updated Good",
                Description = "Updated Description",
                Unit = "Updated Unit",
                Price = 15.0m,
                AvailableAmount = 30
            };

            var result = await repository.UpdateAsync(existingGoodId, updatedGoodDto);

            Assert.NotNull(result);
            Assert.Equal(updatedGoodDto.Name, result.Name);
            Assert.Equal(updatedGoodDto.Description, result.Description);
            Assert.Equal(updatedGoodDto.Unit, result.Unit);
            Assert.Equal(updatedGoodDto.Price, result.Price);
            Assert.Equal(updatedGoodDto.AvailableAmount, result.AvailableAmount);
        }
        [Fact]
        public async Task UpdateAsync_NonExistingGood_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(UpdateAsync_NonExistingGood_ReturnsNull));
            var repository = new GoodRepository(dbContext);

            var nonExistingGoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e56");
            var updatedGoodDto = new ModifyGoodDTO();

            var result = await repository.UpdateAsync(nonExistingGoodId, updatedGoodDto);

            Assert.Null(result);
        }
        [Fact]
        public async Task GetAllAsync_ReturnsCorrectGoodsForPageAndSize()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetAllAsync_ReturnsCorrectGoodsForPageAndSize));
            var repository = new GoodRepository(dbContext);

            var pageNumber = 1; 
            var pageSize = 1;

            var result = await repository.GetAllAsync(pageNumber, pageSize);

            Assert.NotNull(result);
            Assert.Equal(pageSize, result.Count());
            Assert.Equal("Good 1", result.First().Name); 
        }
        [Fact]
        public async Task GetByIdAsync_ExistingGoodId_ReturnsGoodDTO()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetByIdAsync_ExistingGoodId_ReturnsGoodDTO));
            var repository = new GoodRepository(dbContext);

            var existingGoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48");

            var result = await repository.GetByIdAsync(existingGoodId);

            Assert.NotNull(result);
            Assert.Equal("Good 1", result.Name);
            Assert.Equal("Description 1", result.Description);
            Assert.Equal("Unit 1", result.Unit);
            Assert.Equal(20.0m, result.Price);
            Assert.Equal(40, result.AvailableAmount);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingGoodId_ReturnsNull()
        {
            var dbContext = DbContextMocker.GetGoodsStorageDbContext(nameof(GetByIdAsync_NonExistingGoodId_ReturnsNull));
            var repository = new GoodRepository(dbContext);

            var nonExistingGoodId = Guid.NewGuid();

            var result = await repository.GetByIdAsync(nonExistingGoodId);

            Assert.Null(result);
        }
        public static class DbContextMocker
        {
            public static GoodsStorageDbContext GetGoodsStorageDbContext(string dbName)
            {
                var options = new DbContextOptionsBuilder<GoodsStorageDbContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;

                var dbContext = new GoodsStorageDbContext(options);

                dbContext.Goods.AddRange(GetSeedingGoods());
                dbContext.SaveChanges();

                return dbContext;
            }

            private static IEnumerable<Good> GetSeedingGoods()
            {
                return new List<Good>
        {
            new Good
            {
                Id = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                Name = "Good 1",
                Description = "Description 1",
                Unit = "Unit 1",
                Price = 20.0m,
                AvailableAmount = 40,
                IsDeleted = false

            },
            new Good
            {
                Id = Guid.Parse("6df9da5a-ac0d-4650-ba7f-dd7b3fde6e98"),
                Name = "Good 2",
                Description = "Description 2",
                Unit = "Unit 2",
                Price = 25.0m,
                AvailableAmount = 30,
                IsDeleted = false
            },
            new Good
            {
                Id = Guid.Parse("f9f3afad-6a9f-480b-b706-ff94884ab249"),
                Name = "Good 3",
                Description = "Description 3",
                Unit = "Unit 3",
                Price = 30.0m,
                AvailableAmount = 20,
                IsDeleted = false
            }
        };
            }
        }
    }
}