using Azure.Core;
using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.Domain;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IAuthorizationService _authorizationService;

        public RequestsController(IRequestService requestService,IAuthorizationService authorizationService)
        {
            _requestService = requestService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] string userId = null, [FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] bool activeOnly = false)
        {
            var requests = await _requestService.GetAllAsync(pageNumber, pageSize, userId, activeOnly);
            if (requests.Status == Status.Ok)
            {

                foreach (var a in requests.Data)
                {
                    var result = await _authorizationService.AuthorizeAsync(User, a, "CanAccessRequestPolicy");
                    if (!result.Succeeded) return new ForbidResult();
                }
                return Ok(requests.Data);
            }

            return StatusCode(500);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = await _requestService.GetByIdAsync(id);

            if(request.Status == Status.Ok)
            {
                    var result = await _authorizationService.AuthorizeAsync(User, request.Data, "CanAccessRequestPolicy");
                    if (!result.Succeeded) return new ForbidResult();
                    return Ok(request.Data);
            }
            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var request = await _requestService.GetByIdAsync(id);

            var authResult = await _authorizationService.AuthorizeAsync(User, request.Data, "CanAccessRequestPolicy");
            if (!authResult.Succeeded) return new ForbidResult();

            var result = await _requestService.DeleteAsync(id);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return NotFound(result.Description);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] ModifyRequestDTO model)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, model, "CanCreateRequestPolicy");
            if (!authResult.Succeeded) return new ForbidResult();

            var result = await _requestService.UpdateAsync(id, model);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return BadRequest(result.Description);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ModifyRequestDTO model)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, model, "CanCreateRequestPolicy");
            if (!authResult.Succeeded) return new ForbidResult();

            var result = await _requestService.AddAsync(model);

            if (result.Status == Status.Ok) return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);

            return BadRequest(result.Description);
        }
    }
}
