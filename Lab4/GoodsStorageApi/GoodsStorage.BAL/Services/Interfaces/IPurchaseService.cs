using GoodsStorage.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsStorage.BAL.Models;

namespace GoodsStorage.BAL.Services.Interfaces
{
    public interface IPurchaseService
    {
        public Task<BaseResponse<IEnumerable<PurchaseDTO>>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, int? userId = null);
        public Task<BaseResponse<PurchaseDTO>> GetByIdAsync(Guid id);
        public Task<BaseResponse<Guid>> AddAsync(PurchaseDTO dto);
    }
}
