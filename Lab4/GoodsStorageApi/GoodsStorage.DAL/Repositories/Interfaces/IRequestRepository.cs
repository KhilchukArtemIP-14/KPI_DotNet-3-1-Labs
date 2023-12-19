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
        public Task<IEnumerable<RequestDTO>> GetAllAsync(int pageNumber = 1, int pageSize = 5, string userId = null, bool activeOnly = false);
        public Task<RequestDTO> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(ModifyRequestDTO dto);
        public Task<RequestDTO> UpdateAsync(Guid id, ModifyRequestDTO dto);
        public Task<RequestDTO> DeleteAsync(Guid id);
    }
}
