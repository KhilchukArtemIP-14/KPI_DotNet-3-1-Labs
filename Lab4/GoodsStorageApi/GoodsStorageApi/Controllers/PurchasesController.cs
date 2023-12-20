using GoodsStorage.BAL.Models;
using GoodsStorage.BAL.Services.Interfaces;
using GoodsStorage.DAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsStorage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IAuthorizationService _authorizationService;

        public PurchasesController(IPurchaseService purchaseService, IAuthorizationService authorizationService)
        {
            _purchaseService = purchaseService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] string userId = null, [FromQuery] int pageSize = 5, [FromQuery] int pageNumber = 1)
        {
            var purchases = await _purchaseService.GetAllAsync(pageNumber, pageSize, userId);

            if (purchases.Status == Status.Ok) { 
                
                foreach(var a in purchases.Data)
                {
                    var result = await _authorizationService.AuthorizeAsync(User, a, "CanAccessPurchasePolicy");
                    if(!result.Succeeded) return new ForbidResult();
                }
                return Ok(purchases.Data); 
            }

            return StatusCode(500);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var purchase = await _purchaseService.GetByIdAsync(id);

            if (purchase.Status == Status.Ok) {

                var result = await _authorizationService.AuthorizeAsync(User, purchase.Data, "CanAccessPurchasePolicy");
                if (!result.Succeeded) return new ForbidResult();

                return Ok(purchase.Data);
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles ="Staff")]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseDTO model)
        {
            var purchase = await _purchaseService.AddAsync(model);

            if (purchase.Status == Status.Ok) return CreatedAtAction(nameof(GetById), new { id = purchase.Data }, (await _purchaseService.GetByIdAsync(purchase.Data)).Data);

            return BadRequest(purchase.Description);
        }
    }
}
