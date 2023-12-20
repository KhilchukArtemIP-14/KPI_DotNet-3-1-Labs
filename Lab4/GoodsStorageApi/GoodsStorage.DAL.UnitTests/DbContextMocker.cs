using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.UnitTests
{
    public static class DbContextMocker
    {
            public static GoodsStorageDbContext GetGoodsStorageDbContext(string dbName)
            {
                var options = new DbContextOptionsBuilder<GoodsStorageDbContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;

                var dbContext = new GoodsStorageDbContext(options);

                dbContext.Goods.AddRange(GetSeedingGoods());
                dbContext.Requests.AddRange(GetSeedingRequests());
                dbContext.Purchases.AddRange(GetSeedingPurchases());
                dbContext.PurchaseGoods.AddRange(GetSeedingPurchaseGoods());

                dbContext.SaveChanges();

                return dbContext;
            }
            public static IEnumerable<Good> GetSeedingGoods()
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
        public static IEnumerable<Request> GetSeedingRequests()
        {
            return new List<Request>
                {
                new Request
                {
                Id = Guid.Parse("636e2574-2314-47f6-b0a8-51c50c57b06c"),
                GoodId = Guid.Parse("636e2574-2314-47f6-b0a8-51c50c57b06f"),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 10,
                Date = DateTime.Now,
                ExpectedPrice = 99.99m,
                IsActive = true,
                IsDeleted = false,
                },
                        new Request
                {
                Id = Guid.Parse("636e2574-2314-47f6-b0a8-51c50c57b07c"),
                GoodId = Guid.Parse("636e2574-2314-47f6-b0a8-51c50c57b06f"),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 11,
                Date = DateTime.Now,
                ExpectedPrice = 998.99m,
                IsActive = true,
                IsDeleted = false,
                },
                        new Request
                {
                Id = Guid.Parse("636e2574-2314-47f6-b0a8-51c50c57b08c"),
                GoodId = Guid.Parse("636e2574-2314-47f6-b0a8-51c50c57b06f"),
                CustomerId = "646e2574-2314-47f6-b0a8-51c50c57b06f",
                Amount = 12,
                Date = DateTime.Now,
                ExpectedPrice = 999.99m,
                IsActive = false,
                IsDeleted = false,
                }
            };
        }
        public static IEnumerable<Purchase> GetSeedingPurchases()
        {
            return new List<Purchase>
                {
                new Purchase()
                {
                     Id=Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c"),
                     UserId ="646e2574-2314-47f6-b0a8-51c50c57b06f",
                     StaffRepId = "656e2574-2314-47f6-b0a8-51c50c57b06f",
                     Date = DateTime.Now,
                },
                new Purchase()
                {
                     Id=Guid.Parse("636b2574-2314-47f6-b0a8-51c50c57b06c"),
                     UserId ="646e2574-2314-47f6-b0a8-51c50c57b06f",
                     StaffRepId = "656e2574-2314-47f6-b0a8-51c50c57b06f",
                     Date = DateTime.Now,
                },
                new Purchase()
                {
                     Id=Guid.Parse("636c2574-2314-47f6-b0a8-51c50c57b06c"),
                     UserId ="946e2574-2314-47f6-b0a8-51c50c57b06f",
                     StaffRepId = "656e2574-2314-47f6-b0a8-51c50c57b06f",
                     Date = DateTime.Now,
                },
            };
        }
        public static IEnumerable<PurchaseGood> GetSeedingPurchaseGoods()
        {
            return new List<PurchaseGood>
                {
                new PurchaseGood()
                {
                     PurchaseId =Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c"),
                     GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                     Amount=10
                },
                new PurchaseGood()
                {
                     PurchaseId =Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c"),
                     GoodId = Guid.Parse("6df9da5a-ac0d-4650-ba7f-dd7b3fde6e98"),
                     Amount=15
                },
                new PurchaseGood()
                {
                     PurchaseId =Guid.Parse("636a2574-2314-47f6-b0a8-51c50c57b06c"),
                     GoodId = Guid.Parse("f9f3afad-6a9f-480b-b706-ff94884ab249"),
                     Amount=1
                },
                new PurchaseGood()
                {
                     PurchaseId =Guid.Parse("636b2574-2314-47f6-b0a8-51c50c57b06c"),
                     GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                     Amount=10
                },
                new PurchaseGood()
                {
                     PurchaseId =Guid.Parse("636c2574-2314-47f6-b0a8-51c50c57b06c"),
                     GoodId = Guid.Parse("92ab2bb8-e4d7-43c4-a144-00e8bfec9e48"),
                     Amount=10
                },
                new PurchaseGood()
                {
                     PurchaseId =Guid.Parse("636c2574-2314-47f6-b0a8-51c50c57b06c"),
                     GoodId = Guid.Parse("6df9da5a-ac0d-4650-ba7f-dd7b3fde6e98"),
                     Amount=15
                },
            };
        }
    }
}
