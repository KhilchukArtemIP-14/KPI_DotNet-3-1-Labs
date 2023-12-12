using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsStorage.DAL.Models.Domain;
using GoodsStorage.DAL.Models.DTO;
using Request = GoodsStorage.DAL.Models.Domain.Request;


using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace GoodsStorage.DAL.Repositories.Implementations
{
    public class RequestRepository : IRequestRepository
    {
        private readonly GoodsStorageDbContext _dbContext;

        public RequestRepository(GoodsStorageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddAsync(RequestDTO dto)
        {
            var request = new Request()
            {
                Id = Guid.NewGuid(),
                GoodId = dto.GoodId,
                Amount = dto.Amount,
                Date = dto.Date,
                ExpectedPrice = dto.ExpectedPrice,
                IsActive = dto.IsActive
            };

            await _dbContext.Requests.AddAsync(request);
            await _dbContext.SaveChangesAsync();

            return request.Id;
        }

        public async Task<RequestDTO> DeleteAsync(Guid id)
        {
            var existingRequest = await _dbContext.Requests.FindAsync(id);

            if (existingRequest != null)
            {
                _dbContext.Requests.Remove(existingRequest);
                await _dbContext.SaveChangesAsync();

                var requestDTO = new RequestDTO()
                {
                    GoodId = existingRequest.GoodId,
                    Amount = existingRequest.Amount,
                    Date = existingRequest.Date,
                    ExpectedPrice = existingRequest.ExpectedPrice,
                    IsActive = existingRequest.IsActive
                };

                return requestDTO;
            }

            return null;
        }

        public async Task<IEnumerable<RequestDTO>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, string? userId = null, bool? activeOnly = false)
        {
            var query = _dbContext.Requests.AsQueryable();

            if (userId!=null)
            {
                query = query.Where(r => r.CustomerId == userId);
            }

            if (activeOnly.HasValue && activeOnly.Value)
            {
                query = query.Where(r => r.IsActive);
            }

            var requests = await query
                .Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 5))
                .Take(pageSize ?? 5)
                .ToListAsync();

            return requests.Select(request => new RequestDTO()
            {
                GoodId = request.GoodId,
                CustomerId=request.CustomerId,
                Amount = request.Amount,
                Date = request.Date,
                ExpectedPrice = request.ExpectedPrice,
                IsActive = request.IsActive
            });
        }

        public async Task<RequestDTO> GetByIdAsync(Guid id)
        {
            var request = await _dbContext.Requests.FindAsync(id);

            if (request == null)
            {
                return null;
            }

            var requestDTO = new RequestDTO()
            {
                GoodId = request.GoodId,
                CustomerId = request.CustomerId,
                Amount = request.Amount,
                Date = request.Date,
                ExpectedPrice = request.ExpectedPrice,
                IsActive = request.IsActive
            };

            return requestDTO;
        }

        public async Task<RequestDTO> UpdateAsync(Guid id, RequestDTO dto)
        {
            var existingRequest = await _dbContext.Requests.FindAsync(id);

            if (existingRequest != null)
            {
                existingRequest.GoodId = dto.GoodId;
                existingRequest.CustomerId = dto.CustomerId;
                existingRequest.Amount = dto.Amount;
                existingRequest.Date = dto.Date;
                existingRequest.ExpectedPrice = dto.ExpectedPrice;
                existingRequest.IsActive = dto.IsActive;

                _dbContext.Requests.Update(existingRequest);
                await _dbContext.SaveChangesAsync();

                var updatedDto = new RequestDTO()
                {
                    GoodId = existingRequest.GoodId,
                    CustomerId = existingRequest.CustomerId,
                    Amount = existingRequest.Amount,
                    Date = existingRequest.Date,
                    ExpectedPrice = existingRequest.ExpectedPrice,
                    IsActive = existingRequest.IsActive
                };

                return updatedDto;
            }

            return null;
        }
    }
}