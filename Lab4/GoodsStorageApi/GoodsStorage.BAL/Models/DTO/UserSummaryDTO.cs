using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Models.DTO
{
    public class UserSummaryDTO
    {
        public string UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber {  get; set; }
    }
}
