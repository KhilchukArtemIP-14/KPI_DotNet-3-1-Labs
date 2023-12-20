using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Models.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        [MinLength(5)]
        public string Name {  get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string[] Roles { get; set; }
    }
}
