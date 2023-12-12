using GoodsStorage.DAL.Models.DTO;
using GoodsStorage.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Services.Interfaces
{
    public interface IRequestService
    {
        public Task<BaseResponse<IEnumerable<RequestDTO>>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, string userId = null, bool? activeOnly = false);
        public Task<BaseResponse<RequestDTO>> GetByIdAsync(Guid id);
        public Task<BaseResponse<Guid>> AddAsync(RequestDTO dto);
        public Task<BaseResponse<RequestDTO>> UpdateAsync(Guid id, RequestDTO dto);
        public Task<BaseResponse<RequestDTO>> DeleteAsync(Guid id);
    }
}
