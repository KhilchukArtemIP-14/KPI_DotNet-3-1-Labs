using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class PurchaseDTO
    {
        [Required(ErrorMessage ="Buyers ID not provided")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Staff representative ID not provided")]
        public int StaffRepId { get; set; }
        [Required(ErrorMessage = "Date not provided")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Items bought not provided")]
        public IEnumerable<PurchaseGoodDTO> PurchaseGoodDTOs { get; set; }
    }
}
