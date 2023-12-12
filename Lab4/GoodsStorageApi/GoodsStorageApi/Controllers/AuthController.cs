using Microsoft.AspNetCore.Mvc;
using GoodsStorage.BAL.Services.Implementations;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.BAL.Models.DTO;
using GoodsStorage.BAL.Models;
using Microsoft.AspNetCore.Identity;

namespace GoodsStorage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            var result = await _authService.Register(dto);

            if(result.Status==Status.Ok) return Ok("User successfully created");

            return BadRequest(result.Description);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            var result = await _authService.Login(dto);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return BadRequest(result.Description);
        }
    }
}
