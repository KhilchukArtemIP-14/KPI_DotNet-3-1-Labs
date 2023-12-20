using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class PurchaseGoodDTO
    {
        public Guid GoodId { get; set; }
        public string GoodName { get; set; }
        public int Amount { get; set; }
    }
}
