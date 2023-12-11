using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? userId = null, [FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1, [FromQuery] bool activeOnly = false)
        {
            var requests = await _requestService.GetAllAsync(pageNumber, pageSize, userId, activeOnly);

            if (requests.Status == Status.Ok) return Ok(requests.Data);

            return StatusCode(500);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = await _requestService.GetByIdAsync(id);

            if (request.Status == Status.Error) return NotFound();

            return Ok(request.Data);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var result = await _requestService.DeleteAsync(id);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return NotFound(result.Description);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RequestDTO model)
        {
            var result = await _requestService.UpdateAsync(id, model);

            if (result.Status == Status.Ok) return Ok(result.Data);

            return BadRequest(result.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestDTO model)
        {
            var result = await _requestService.AddAsync(model);

            if (result.Status == Status.Ok) return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);

            return BadRequest(result.Description);
        }
    }

}
