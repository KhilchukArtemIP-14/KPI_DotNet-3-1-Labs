using GoodsStorage.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        public Task<IEnumerable<RequestDTO>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, int? userId = null, bool? activeOnly = false);
        public Task<RequestDTO> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(RequestDTO dto);
        public Task<RequestDTO> UpdateAsync(Guid id, RequestDTO dto);
        public Task<RequestDTO> DeleteAsync(Guid id);
    }
}
