using System.Linq.Expressions;
using Ecommerce.Model.src;
using Ecommerce.Model.src.Shared;
using Ecommerce.Model.src.Shared.ValueObject;

namespace Ecommerce.Service.src.Shared.Interface
{
    public interface IBaseService<
        T,
        TReadDto,
        TUpdateDto,
        TCreateDto,
        TUpdateDataValidation,
        TCreateDataValidation
    >
        where T : BaseEntity
        where TReadDto : IReadDto<T>
        where TCreateDto : ICreateDto<T>
        where TUpdateDto : IUpdateDto<T>
        where TUpdateDataValidation : IDataValidator<TUpdateDto>
        where TCreateDataValidation : IDataValidator<TCreateDto>
    {
        Task<PaginatedResult<TReadDto>> GetAllAsync(
            QueryOptions paginationOptions,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[] includes = null
        );
        Task<TReadDto> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[] includes = null
        );
        Task<TReadDto> CreateAsync(
            TCreateDto createDto,
            Expression<Func<T, bool>>? checkDuplicate = null
        );
        Task<TReadDto> UpdateAsync(
            TUpdateDto updateDto,
            Expression<Func<T, bool>>? checkDuplicate = null
        );
        Task<bool> DeleteByIdAsync(int id);
    }
}
