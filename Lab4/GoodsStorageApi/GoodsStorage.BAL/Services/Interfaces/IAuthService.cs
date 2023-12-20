using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<BaseResponse<IdentityResult>> Register(RegisterUserDTO registerUserDTO);
        public Task<BaseResponse<string>> Login(LoginUserDTO loginUserDto);
        public string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
