using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.UserServiceAggregate.FavoutiteAggregate;
using Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.UserControllerAggregate
{
    public class FavouriteController(IFavouriteService favouriteService)
        : BaseController<
            Favourite,
            FavouriteReadDto,
            FavouriteUpdateDto,
            FavouriteCreateDto,
            FavouriteUpdateValidator,
            FavouriteCreateValidator
        >(favouriteService)
    {
        private readonly IFavouriteService _favouriteService = favouriteService;

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<FavouriteReadDto>>> GetAllAsync(int id)
        {
            var result = await _favouriteService.GetAllAsync(
                new QueryOptions(),
                cd => cd.UserId == id
            );
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // [HttpDelete]
        // public virtual async Task<IActionResult> DeleteByDataAsync(
        //     [FromBody] FavouriteCreateDto createDto
        // )
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var result = await _favouriteService.DeleteByData(
        //         createDto.UserId,
        //         createDto.ProductId
        //     );
        //     if (!result)
        //     {
        //         return NotFound();
        //     }
        //     return NoContent();
        // }

        [HttpPost("delete")]
        public virtual async Task<IActionResult> DeleteByDataAsync(
            [FromBody] FavouriteCreateDto createDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _favouriteService.DeleteByData(
                createDto.UserId,
                createDto.ProductId
            );
            if (result)
                return Ok(new { message = "Favourite deleted successfully" });
            else
                return BadRequest(new { message = "Failed to delete the favourite" });
        }
    }
}
