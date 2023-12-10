using GoodsStorage.DAL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    internal class RequestDTO
    {
        public Guid GoodId { get; set; }
        public int CustomerId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal ExpectedPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
