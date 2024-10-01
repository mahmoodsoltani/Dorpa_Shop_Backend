using System.Linq.Expressions;
using Ecommerce.Model.src;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository;
using Ecommerce.Model.src.Shared;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.Shared.Implementation
{
    public class BaseService<
        T,
        TReadDto,
        TUpdateDto,
        TCreateDto,
        TUpdateDataValidation,
        TCreateDataValidation
    >
        : IBaseService<
            T,
            TReadDto,
            TUpdateDto,
            TCreateDto,
            TUpdateDataValidation,
            TCreateDataValidation
        >
        where T : BaseEntity, new()
        where TReadDto : BaseReadDto<T>, new()
        where TCreateDto : ICreateDto<T>
        where TUpdateDto : IUpdateDto<T>
        where TUpdateDataValidation : IDataValidator<TUpdateDto>, new()
        where TCreateDataValidation : IDataValidator<TCreateDto>, new()
    {
        private readonly IBaseRepo<T> _repo;

        public BaseService(IBaseRepo<T> baseRepo)
        {
            _repo = baseRepo;
        }

        public virtual async Task<TReadDto> CreateAsync(
            TCreateDto createDto,
            Expression<Func<T, bool>>? checkCondition = null
        )
        {
            TCreateDataValidation dataValidator = new();
            var result = dataValidator.Validate(createDto);
            if (!result.IsValid)
            {
                throw new InvalidInputDataException(result.ErrorMessage);
            }
            T entity = new();
            createDto.ToEntity(entity);
            await _repo.CreateAsync(entity, checkCondition);
            TReadDto readDto = new();
            readDto.FromEntity(entity);
            return readDto;
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }

        public async Task<PaginatedResult<TReadDto>> GetAllAsync(
            QueryOptions queryOptions,
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[] includes = null
        )
        {
            var result = await _repo.GetAllAsync(queryOptions, filter, includes, false);
            var convertedToReadDto = result.Items.Select(u =>
            {
                var readDto = Activator.CreateInstance<TReadDto>();
                readDto.FromEntity(u);
                return readDto;
            });
            return new PaginatedResult<TReadDto>
            {
                Items = convertedToReadDto,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public virtual async Task<TReadDto> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[] includes = null
        )
        {
            var result =
                await _repo.GetAsync(filter, includes, false)
                ?? throw new EntityNotFoundException();
            var readDto = Activator.CreateInstance<TReadDto>();
            readDto.FromEntity(result);
            return readDto;
        }

        public virtual async Task<TReadDto> UpdateAsync(
            TUpdateDto updateDto,
            Expression<Func<T, bool>>? checkDuplicate = null
        )
        {
            TUpdateDataValidation dataValidator = new();
            var result = dataValidator.Validate(updateDto);
            if (!result.IsValid)
            {
                throw new InvalidInputDataException(result.ErrorMessage);
            }
            var foundedEntity =
                await _repo.GetAsync(e => e.Id == updateDto.Id, null, true)
                ?? throw new EntityNotFoundException();
            updateDto.UpdateEntity(foundedEntity);
            var updatedEntity = await _repo.UpdateAsync(foundedEntity, checkDuplicate);
            TReadDto readDto = new();
            readDto.FromEntity(updatedEntity);
            return readDto;
        }
    }
}
