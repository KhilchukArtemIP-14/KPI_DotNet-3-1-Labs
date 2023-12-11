using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.Domain
{
    public class PurchaseGood
    {
        [Key]
        [Column(Order = 1)]
        public Guid GoodId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid PurchaseId { get; set; }
        public int Amount { get; set; }

        public Good Good { get; set; }
        public Purchase Purchase { get; set; }
    }
}
