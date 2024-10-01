using System.Linq.Expressions;
using System.Text.Json;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Model.src.Repository.Shared;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Service.src.CategoryServiceAggregate.CategoryAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.Shared.ImageServiceAggregate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.CategoryServiceAggregate.ProductAggregate
{
    public class ProductService
        : BaseService<
            Product,
            ProductReadDto,
            ProductUpdateDto,
            ProductCreateDto,
            ProductUpdateValidator,
            ProductCreateValidator
        >,
            IProductService
    {
        public IProductRepo _productRepo;
        public IImageService _imageService;

        public ProductService(IProductRepo productRepo, IImageService imageService)
            : base(productRepo)
        {
            _productRepo = productRepo;
            _imageService = imageService;
        }

        public override async Task<ProductReadDto> CreateAsync(
            ProductCreateDto createDto,
            Expression<Func<Product, bool>>? checkCondition = null
        )
        {
            ProductCreateValidator dataValidator = new();
            var result = dataValidator.Validate(createDto);
            if (!result.IsValid)
            {
                throw new InvalidInputDataException(result.ErrorMessage);
            }

            var imageUrls = new List<string>();
            var productImages = new List<ProductImage>();
            var imageUrl = "";
            imageUrl = await _imageService.SaveImageAsync(createDto.Image);
            imageUrls.Add(imageUrl);

            var product = new Product();
            createDto.ToEntity(product);
            product.ProductImages =
            [
                new ProductImage
                {
                    ImageUrl = imageUrl,
                    AltText = createDto.AltText,
                    IsPrimary = true,
                }
            ];

            product = await _productRepo.CreateAsync(product);
            ProductReadDto readDto = new();
            readDto.FromEntity(product);
            return readDto;
        }

        public virtual async Task<ProductReadDto> GetAsync(
            Expression<Func<Product, bool>>? filter = null,
            Expression<Func<Product, object>>[] includes = null
        )
        {
            var result =
                await _productRepo.GetAsync(
                    filter,
                    [
                        p => p.Category,
                        p => p.Brand,
                        p => p.Size,
                        p => p.Color,
                        p => p.Discount,
                        p => p.ProductImages,
                        p => p.Reviews,
                    ],
                    false
                ) ?? throw new EntityNotFoundException();
            var readDto = Activator.CreateInstance<ProductReadDto>();
            readDto.FromEntity(result);
            return readDto;
        }

        public async Task<PaginatedResult<ProductReadDto>> GetAllAsync(
            QueryOptions queryOptions,
            Expression<Func<Product, bool>>? filter = null,
            Expression<Func<Product, object>>[] includes = null
        )
        {
            var result = await _productRepo.GetAllAsync(
                queryOptions,
                filter,
                [
                    p => p.Category,
                    p => p.Brand,
                    p => p.Size,
                    p => p.Color,
                    p => p.Discount,
                    p => p.ProductImages,
                    p => p.Reviews
                ],
                false
            );
            var convertedToReadDto = result.Items.Select(u =>
            {
                var readDto = Activator.CreateInstance<ProductReadDto>();
                readDto.FromEntity(u);
                return readDto;
            });
            return new PaginatedResult<ProductReadDto>
            {
                Items = convertedToReadDto,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }
    }
}
