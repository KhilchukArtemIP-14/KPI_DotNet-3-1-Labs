using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class ModifyGoodDTO
    {
        [Required(ErrorMessage = "Name not provided")]
        [MinLength(3, ErrorMessage = "Name must be atleast 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description not provided")]
        [MinLength(10, ErrorMessage = "Description must be atleast 10 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Unit not provided")]
        [MinLength(1, ErrorMessage = "Unit name must be atleast 1 character")]
        public string Unit { get; set; }
        [Required(ErrorMessage = "Price not provided")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Available amount not provided")]
        [Range(1, double.MaxValue, ErrorMessage = "Available amount must be non-negative")]
        public int AvailableAmount { get; set; }
    }
}
