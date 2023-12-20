using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class CreatePurchaseGoodDTO
    {
        [Required(ErrorMessage = "Good ID not provided")]
        public Guid GoodId { get; set; }
        [Required(ErrorMessage = "Amount not provided")]
        [Range(1, int.MaxValue, ErrorMessage = "Available amount must be positive")]
        public int Amount { get; set; }
    }
}
