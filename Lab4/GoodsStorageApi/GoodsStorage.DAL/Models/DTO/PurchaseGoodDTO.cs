using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    internal class PurchaseGoodDTO
    {
        public Guid GoodId { get; set; }
        public Guid PurchaseId { get; set; }
        public int Amount { get; set; }
    }
}
