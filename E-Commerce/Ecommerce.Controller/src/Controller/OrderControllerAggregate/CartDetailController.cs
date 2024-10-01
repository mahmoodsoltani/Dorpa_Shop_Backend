using System.Security.Claims;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.OrderServiceAggregate.CartDetailAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.OrderControllerAggregate
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class CartDetailController(ICartDetailService cartDetailService)
        : BaseController<
            CartDetail,
            CartDetailReadDto,
            CartDetailUpdateDto,
            CartDetailCreateDto,
            CartDetailUpdateValidator,
            CartDetailCreateValidator
        >(cartDetailService)
    {
        private readonly ICartDetailService _cartDetailService = cartDetailService;

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<CartDetailReadDto>>> GetAllAsync(int id)
        {
            var result = await _cartDetailService.GetAllAsync(
                new QueryOptions(),
                cd => cd.UserId == id
            );
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/cartDetails
        
        [HttpPost]
        public override async Task<ActionResult<CartDetailReadDto>> CreateAsync(
            [FromBody] CartDetailCreateDto cartDetailCreateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != cartDetailCreateDto.UserId.ToString())
            {
                throw new UnauthorizedActionException();
            }
            var cartDetailReadDto = await _cartDetailService.CreateAsync(cartDetailCreateDto);
            return CreatedAtAction(
                nameof(GetAsync),
                new { id = cartDetailReadDto.Id },
                cartDetailReadDto
            );
        }
   
    }
}
