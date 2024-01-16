using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class ModifyRequestDTO
    {
        [Required(ErrorMessage = "Good ID not provided")]
        public Guid GoodId { get; set; }
        [Required(ErrorMessage = "Customer ID not provided")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Amount not provided")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be non-negative")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Date not provided")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Expected price not provided")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Expected price must be non-negative")]
        public decimal ExpectedPrice { get; set; }
        [Required(ErrorMessage = "Request status not provided")]
        public bool IsActive { get; set; }
    }
}
