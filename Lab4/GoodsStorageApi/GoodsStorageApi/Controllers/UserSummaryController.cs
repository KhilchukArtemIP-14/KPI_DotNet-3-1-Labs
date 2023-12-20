using GoodsStorage.BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace GoodsStorage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSummaryController : Controller
    {
        private readonly IUserSummaryService _userSummaryService;
        private readonly IAuthorizationService _authorizationService;
        public UserSummaryController(IUserSummaryService userSummaryService, IAuthorizationService authorizationService)
        {
            _userSummaryService = userSummaryService;
            _authorizationService = authorizationService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSummaryById(string id)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessUserSummaryPolicy");
            if (!authResult.Succeeded) return new ForbidResult();

            var result = await _userSummaryService.GetUserSummaryById(id);
            
            if (result.Status == Status.Ok) return Ok(result.Data);
            
            return NotFound(result.Description);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserSummaryById(string id, [FromBody] UpdateUserSummaryDTO dto)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessUserSummaryPolicy");
            if (!authResult.Succeeded) return new ForbidResult();

            var result = await _userSummaryService.UpdateUserSummaryById(dto,id);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return NotFound(result.Description);
        }
    }
}
