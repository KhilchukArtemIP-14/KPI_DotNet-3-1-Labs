using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.Domain
{
    internal class Good
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int AvailableAmount { get; set; }
        public bool IsDeleted { get; set; }

        public IEnumerable<PurchaseGood> PurchaseGoods { get; set; }
    }
}
