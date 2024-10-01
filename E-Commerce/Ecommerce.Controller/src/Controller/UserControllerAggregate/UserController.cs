using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.UserServiceAggregate.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controller.src.Controller.UserControllerAggregate
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class UserController(IUserService userService)
        : BaseController<
            User,
            UserReadDto,
            UserUpdateDto,
            UserCreateDto,
            UserUpdateValidator,
            UserCreateValidator
        >(userService)
    {
        private readonly IUserService _userService = userService;

        // GET: api/users?pageNumber=1&pageSize=10
        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllAsync(
            [FromQuery] QueryOptions queryOptions
        )
        {
            return await base.GetAllAsync(queryOptions);
        }

        // POST: api/users
        [AllowAnonymous]
        [HttpPost]
        public override async Task<ActionResult<UserReadDto>> CreateAsync(
            [FromBody] UserCreateDto userCreateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userReadDto = await _userService.CreateAsync(
                userCreateDto,
                u => u.Email.ToLower() == userCreateDto.Email.ToLower()
            );
            return CreatedAtAction(nameof(GetAsync), new { id = userReadDto.Id }, userReadDto);
        }

        // PUT: api/users/5
        [HttpPut]
        [Authorize]
        public override async Task<IActionResult> UpdateAsync(
            [FromBody] UserUpdateDto userUpdateDto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.UpdateAsync(userUpdateDto);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        [Authorize]
        [HttpGet("{id}")]
        public override async Task<ActionResult<UserReadDto>> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }
    }
}
