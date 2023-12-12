using GoodsStorage.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Models.DTO;

namespace GoodsStorage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSummaryController : Controller
    {
        private readonly IUserSummaryService _userSummaryService;
        public UserSummaryController(IUserSummaryService userSummaryService)
        {
            _userSummaryService = userSummaryService;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetUserSummaryById(string id)
        {
            var result = await _userSummaryService.GetUserSummaryById(id);
            
            if (result.Status == Status.Ok) return Ok(result.Data);
            
            return NotFound(result.Description);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateUserSummaryById(string id, [FromBody] UserSummaryDTO dto)
        {
            var result = await _userSummaryService.UpdateUserSummaryById(dto,id);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return NotFound(result.Description);
        }
    }
}
