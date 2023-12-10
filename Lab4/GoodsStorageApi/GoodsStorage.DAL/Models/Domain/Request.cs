using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.Domain
{
    internal class Request
    {
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }
        public int CustomerId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public Decimal ExpectedPrice { get; set; }
        public bool IsActive { get;set; }
        public bool IsDeleted { get; set; }

        public Good Good { get; set; }
    }
}
