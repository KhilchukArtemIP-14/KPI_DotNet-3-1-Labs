using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStorage.BAL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("userId", user.Id)

            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<BaseResponse<string>> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);
                if (passwordCheck)
                {
                    var roles= await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var token = CreateJWTToken(user, roles.ToList());
                        return BaseResponse<string>.OkResponse(token);
                    }
                }
            }
            return BaseResponse<string>.ErrorResponse("Login error: incorrect email or password!");
        }

        public async Task<BaseResponse<IdentityResult>> Register(RegisterUserDTO registerUserDTO)
        {
            var identityUser = new IdentityUser()
            {
                UserName= registerUserDTO.Name,
                Email = registerUserDTO.Email,
                PhoneNumber = registerUserDTO.PhoneNumber,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerUserDTO.Password);
            if (identityResult.Succeeded)
            {
                if (registerUserDTO.Roles != null && registerUserDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerUserDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return BaseResponse<IdentityResult>.OkResponse(identityResult);
                    }
                    return BaseResponse<IdentityResult>.ErrorResponse("User role assignment error: failed to assign role");
                }
                return BaseResponse<IdentityResult>.OkResponse(identityResult);
            }
            else return BaseResponse<IdentityResult>.ErrorResponse("User registration error: could not register user");
        }
    }
}
