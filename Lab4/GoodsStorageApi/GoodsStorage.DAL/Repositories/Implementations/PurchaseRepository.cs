using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.Domain;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Request = GoodsStorage.DAL.Models.Domain.Request;

namespace GoodsStorage.DAL.Repositories.Implementations
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly GoodsStorageDbContext _dbContext;

        public PurchaseRepository(GoodsStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(CreatePurchaseDTO dto)
        {
            var tempId = Guid.NewGuid();
            var purchase = new Purchase()
            {
                Id = tempId,
                UserId = dto.UserId,
                StaffRepId = dto.StaffRepId,
                Date = dto.Date,
                PurchaseGoods = dto.PurchaseGoodDTOs.Select(purchaseGoodDTO => new PurchaseGood
                {
                    PurchaseId= tempId,
                    GoodId = purchaseGoodDTO.GoodId,
                    Amount = purchaseGoodDTO.Amount
                }).ToList()
            };

            await _dbContext.Purchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();

            return purchase.Id;
        }

        public async Task<IEnumerable<PurchaseDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 5, string userId = null)
        {
            var query = _dbContext.Purchases.AsQueryable();

            if (userId!=null)
            {
                query = query.Where(p => p.UserId == userId);
            }

            var purchases = await query
                .Include(p => p.PurchaseGoods)
                .ThenInclude(pg => pg.Good)
                .Skip((pageNumber  - 1) * (pageSize))
                .Take(pageSize)
                .ToListAsync();

            return purchases.Select(purchase => new PurchaseDTO()
            {
                Id = purchase.Id,
                UserId = purchase.UserId,
                StaffRepId = purchase.StaffRepId,
                Date = purchase.Date,
                PurchaseGoodDTOs = purchase.PurchaseGoods.Select(purchaseGood => new PurchaseGoodDTO
                {
                    GoodId = purchaseGood.GoodId,
                    GoodName = purchaseGood.Good.Name,
                    Amount = purchaseGood.Amount
                })
            });
        }

        public async Task<PurchaseDTO> GetByIdAsync(Guid id)
        {
            var purchase = await _dbContext.Purchases.Include(p => p.PurchaseGoods)
                                                     .ThenInclude(pg=>pg.Good)
                                                     .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return null;
            }

            var purchaseDTO = new PurchaseDTO()
            {
                Id = purchase.Id,
                UserId = purchase.UserId,
                StaffRepId = purchase.StaffRepId,
                Date = purchase.Date,
                PurchaseGoodDTOs = purchase.PurchaseGoods.Select(purchaseGood => new PurchaseGoodDTO
                {
                    GoodId = purchaseGood.GoodId,
                    GoodName = purchaseGood.Good.Name,
                    Amount = purchaseGood.Amount
                })
            };

            return purchaseDTO;
        }
    }
}
