using GoodsStorage.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task<IEnumerable<PurchaseDTO>> GetAllAsync(int? pageNumber = 1, int? pageSize = 5, int? userId = null);
        public Task<PurchaseDTO> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(PurchaseDTO dto);
    }
}
