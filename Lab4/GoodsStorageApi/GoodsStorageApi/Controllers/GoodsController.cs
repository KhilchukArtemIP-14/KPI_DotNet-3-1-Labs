using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using GoodsStorage.BAL.Models;
namespace GoodsStorage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsService _goodsService;
        public GoodsController(IGoodsService goodsService) 
        {
            _goodsService = goodsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var goods = await _goodsService.GetAllAsync(pageNumber, pageSize);

            if(goods.Status==Status.Ok) return Ok(goods.Data);

            return StatusCode(500);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var good = await _goodsService.GetByIdAsync(id);

            if (good.Status == Status.Error) return NotFound();
            
            return Ok(good.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GoodDTO model)
        {
            var good = await _goodsService.AddAsync(model);

            if (good.Status == Status.Ok) return CreatedAtAction(nameof(GetById), new { id = good.Data }, model);
            
            return BadRequest(good.Description);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GoodDTO model)
        {
            var result = await _goodsService.UpdateAsync(id, model);

            if(result.Status == Status.Ok) return Ok(result.Data);

            return BadRequest(result.Description);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            {
                var result = await _goodsService.DeleteAsync(id);

                if (result.Status == Status.Ok) return Ok(result.Data);

                return NotFound(result.Description);
            }
        }
    }
}
