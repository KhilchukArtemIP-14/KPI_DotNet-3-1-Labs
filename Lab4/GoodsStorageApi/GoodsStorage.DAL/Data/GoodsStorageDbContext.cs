using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodsStorage.DAL.Models.Domain;
using Azure.Core;
using Request = GoodsStorage.DAL.Models.Domain.Request;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GoodsStorage.DAL.Data
{
    public class GoodsStorageDbContext: IdentityDbContext
    {
        public GoodsStorageDbContext(DbContextOptions<GoodsStorageDbContext> options): base(options)
        {
            
        }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseGood> PurchaseGoods { get; set; }
        public DbSet<Request> Requests { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PurchaseGood>().HasKey(pg => new { pg.PurchaseId, pg.GoodId });
            base.OnModelCreating(builder);
            //
            var roles = new List<IdentityRole>() {
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
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
