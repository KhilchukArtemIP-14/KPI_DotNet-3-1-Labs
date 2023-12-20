using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.DAL.Models.DTO
{
    public class CreatePurchaseDTO
    {
        [Required(ErrorMessage = "Buyers ID not provided")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Staff representative ID not provided")]
        public string StaffRepId { get; set; }
        [Required(ErrorMessage = "Date not provided")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Items bought not provided")]
        public IEnumerable<CreatePurchaseGoodDTO> PurchaseGoodDTOs { get; set; }
    }
}
