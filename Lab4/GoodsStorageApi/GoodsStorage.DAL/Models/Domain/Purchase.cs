using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.Domain
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string StaffRepId { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<PurchaseGood> PurchaseGoods { get; set; }
    }
}
