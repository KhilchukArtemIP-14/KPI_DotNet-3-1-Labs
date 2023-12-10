using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    internal class PurchaseDTO
    {
        public int UserId { get; set; }
        public int StaffRepId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<PurchaseGoodDTO> PurchaseGoodDTOs { get; set; }
    }
}
