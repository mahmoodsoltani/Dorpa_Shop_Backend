using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.OrderAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.OrderAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.OrderControllerAggregate
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class OrderController(IOrderService orderService)
        : BaseController<
            Order,
            OrderReadDto,
            OrderUpdateDto,
            OrderCreateDto,
            OrderUpdateValidator,
            OrderCreateValidator
        >(orderService)
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet("checkout/{userId}")]
        public async Task<ActionResult<bool>> CartCheckoutAsync(int userId)
        {
            var order = await _orderService.CartCheckoutAsync(userId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
