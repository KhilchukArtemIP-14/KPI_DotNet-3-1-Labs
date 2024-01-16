using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsStorageApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using GoodsStorage.DAL.Data;
using Microsoft.Extensions.DependencyInjection;
using GoodsStorage.DAL.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace GoodsStorageApi.IntegrationTests
{
    public class WebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GoodsStorageDbContext>));

                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                services.AddDbContext<GoodsStorageDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MyDb");
                });

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<GoodsStorageDbContext>();
                    
                    SeedData.PopulateTestData(db);
                }
            });
        }
        public static class SeedData
        {
            public static void PopulateTestData(GoodsStorageDbContext dbContext)
            {
                dbContext.Goods.AddRange(GetSeedingGoods());
                dbContext.Requests.AddRange(GetSeedingRequests());
                dbContext.Purchases.AddRange(GetSeedingPurchases());
                dbContext.PurchaseGoods.AddRange(GetSeedingPurchaseGoods());
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(GetRoles());
                }
                dbContext.SaveChanges();
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
            public static IEnumerable<IdentityRole> GetRoles()
            {
                return new List<IdentityRole>() {
                new IdentityRole()
                {
                    Id="20931b5b-772c-49b3-b033-75c5fd551649",
                    ConcurrencyStamp="20931b5b-772c-49b3-b033-75c5fd551649",
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper(),
                },
                new IdentityRole()
                {
                    Id="148bd49e-c3d7-42c3-8a9d-842e35fa37d0",
                    ConcurrencyStamp="148bd49e-c3d7-42c3-8a9d-842e35fa37d0",
                    Name = "Staff",
                    NormalizedName = "Staff".ToUpper(),
                }
            };
            }

        }
    }
}
