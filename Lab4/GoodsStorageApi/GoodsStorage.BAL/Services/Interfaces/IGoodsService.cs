using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodsStorage.BAL.Models;
using GoodsStorage.DAL.Models.DTO;
namespace GoodsStorage.BAL.Services.Interfaces
{
    public interface IGoodsService
    {
        public Task<BaseResponse<IEnumerable<GoodDTO>>> GetAllAsync(int pageNumber = 1, int pageSize = 5);
        public Task<BaseResponse<GoodDTO>> GetByIdAsync(Guid id);
        public Task<BaseResponse<Guid>> AddAsync(ModifyGoodDTO dto);
        public Task<BaseResponse<GoodDTO>> UpdateAsync(Guid id, ModifyGoodDTO dto);
        public Task<BaseResponse<GoodDTO>> DeleteAsync(Guid id);
    }
}
