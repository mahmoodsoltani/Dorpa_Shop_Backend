using Ecommerce.Model.src;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Shared;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.Shared.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/v1/[controller]s")]
public class BaseController<
    T,
    TReadDto,
    TUpdateDto,
    TCreateDto,
    TUpdateValidator,
    TCreateValidator
>(IBaseService<T, TReadDto, TUpdateDto, TCreateDto, TUpdateValidator, TCreateValidator> service)
    : ControllerBase
    where T : BaseEntity
    where TReadDto : IReadDto<T>
    where TCreateDto : ICreateDto<T>
    where TUpdateDto : IUpdateDto<T>
    where TUpdateValidator : IDataValidator<TUpdateDto>
    where TCreateValidator : IDataValidator<TCreateDto>
{
    private readonly IBaseService<
        T,
        TReadDto,
        TUpdateDto,
        TCreateDto,
        TUpdateValidator,
        TCreateValidator
    > _service = service;

    // GET: api/[controller]
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TReadDto>>> GetAllAsync(
        [FromQuery] QueryOptions queryOptions
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (queryOptions.PageNumber < 1)
        {
            throw new InvalidQueryOptionException(
                "PageNumber",
                "Page number must be greater than 0."
            );
        }

        if (queryOptions.PageSize < 1)
        {
            throw new InvalidQueryOptionException("PageSize", "Page size must be greater than 0.");
        }

        var result = await _service.GetAllAsync(queryOptions);
        return Ok(result);
    }

    // GET: api/[controller]/5
    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TReadDto>> GetAsync(int id)
    {
        var dto = await _service.GetAsync(e => e.Id == id);
        if (dto == null)
        {
            return NotFound();
        }
        return Ok(dto);
    }

    // DELETE: api/[controller]/5
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteByIdAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost]
    public virtual async Task<ActionResult<TReadDto>> CreateAsync([FromBody] TCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var readDto = await _service.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetAsync), new { id = readDto.Id }, readDto);
    }

    [HttpPut]
    public virtual async Task<IActionResult> UpdateAsync([FromBody] TUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.UpdateAsync(updateDto);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
