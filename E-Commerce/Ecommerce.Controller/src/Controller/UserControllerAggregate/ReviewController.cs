using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.UserControllerAggregate
{
    public class ReviewController(IReviewService reviewService)
        : BaseController<
            Review,
            ReviewReadDto,
            ReviewUpdateDto,
            ReviewCreateDto,
            ReviewUpdateValidator,
            ReviewCreateValidator
        >(reviewService) { 
             [AllowAnonymous]
        public override async Task<ActionResult<IEnumerable<ReviewReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<ActionResult<ReviewReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }


        }
}
