using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class PurchaseDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string StaffRepId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<PurchaseGoodDTO> PurchaseGoodDTOs { get; set; }
    }
}
