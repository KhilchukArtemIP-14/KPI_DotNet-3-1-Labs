using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsStorage.DAL.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GoodsStorage.DAL.Repositories.Implementations
{
    public class GoodRepository : IGoodRepository
    {
        private readonly GoodsStorageDbContext _dbContext;
        public GoodRepository(GoodsStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> AddAsync(ModifyGoodDTO dto)
        {

            Good good = new Good()
            {
                Id=Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Unit = dto.Unit,
                Price = dto.Price,
                AvailableAmount = dto.AvailableAmount
            };
            await _dbContext.Goods.AddAsync(good);
            await _dbContext.SaveChangesAsync();

            return good.Id;
        }

        public async Task<GoodDTO> DeleteAsync(Guid id)
        {
            var existingGood = await _dbContext.Goods.Where(g => !g.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);

            if (existingGood != null)
            {
                existingGood.IsDeleted = true;

                _dbContext.Goods.Update(existingGood);
                await _dbContext.SaveChangesAsync();

                var goodDto = new GoodDTO()
                {
                    Id=existingGood.Id,
                    Name = existingGood.Name,
                    Description = existingGood.Description,
                    Unit = existingGood.Unit,
                    Price = existingGood.Price,
                    AvailableAmount = existingGood.AvailableAmount
                };

                return goodDto;
            }

            return null;
        }

        public async Task<IEnumerable<GoodDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _dbContext.Goods
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(good => new GoodDTO(){
                    Id = good.Id,
                    Name = good.Name,
                    Description = good.Description,
                    Unit = good.Unit,
                    Price = good.Price,
                    AvailableAmount = good.AvailableAmount
                }).ToListAsync();
        }

        public async Task<GoodDTO> GetByIdAsync(Guid id)
        {   
            var good = await _dbContext.Goods.Where(g=>!g.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);
            if (good == null) return null;

            var goodDto = new GoodDTO()
            {
                Id = good.Id,
                Name = good.Name,
                Description = good.Description,
                Unit = good.Unit,
                Price = good.Price,
                AvailableAmount = good.AvailableAmount
            };
            return goodDto;
        }

        public async Task<GoodDTO> UpdateAsync(Guid id, ModifyGoodDTO dto)
        {
            var existingGood = await _dbContext.Goods.Where(g => !g.IsDeleted).FirstOrDefaultAsync(x => x.Id == id);

            if (existingGood != null)
            {
                existingGood.Name = dto.Name;
                existingGood.Description = dto.Description;
                existingGood.Unit = dto.Unit;
                existingGood.Price = dto.Price;
                existingGood.AvailableAmount = dto.AvailableAmount;

                _dbContext.Goods.Update(existingGood);
                await _dbContext.SaveChangesAsync();

                var updatedDto = new GoodDTO()
                {
                    Id = existingGood.Id,
                    Name = existingGood.Name,
                    Description = existingGood.Description,
                    Unit = existingGood.Unit,
                    Price = existingGood.Price,
                    AvailableAmount = existingGood.AvailableAmount
                };

                return updatedDto;
            }

            return null;
        }
    }
}
