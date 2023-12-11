using GoodsStorage.DAL.Data;
using GoodsStorage.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Repositories.Interfaces
{
    public interface IGoodRepository
    {
        public Task<IEnumerable<GoodDTO>> GetAllAsync(int pageNumber=1, int pageSize = 5);
        public Task<GoodDTO> GetByIdAsync(Guid id);
        public Task<Guid> AddAsync(GoodDTO dto);
        public Task<GoodDTO> UpdateAsync(Guid id, GoodDTO dto);
        public Task<GoodDTO> DeleteAsync(Guid id);
    }
}
