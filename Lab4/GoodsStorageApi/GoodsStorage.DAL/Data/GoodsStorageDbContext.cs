using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodsStorage.DAL.Models.Domain;
using Azure.Core;
using Request = GoodsStorage.DAL.Models.Domain.Request;

namespace GoodsStorage.DAL.Data
{
    public class GoodsStorageDbContext: DbContext
    {
        public GoodsStorageDbContext(DbContextOptions<GoodsStorageDbContext> options): base(options)
        {
            
        }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseGood> PurchaseGoods { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
