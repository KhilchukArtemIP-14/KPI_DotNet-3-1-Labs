using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.Domain
{
    internal class Purchase
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int StaffRepId { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<PurchaseGood> PurchaseGoods { get; set; }
    }
}
