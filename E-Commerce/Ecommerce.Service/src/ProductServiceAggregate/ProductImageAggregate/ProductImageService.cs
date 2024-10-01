using System.Linq.Expressions;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.ImageServiceAggregate;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.ProductServiceAggregate
{
    public class ProductImageService
        : BaseService<
            ProductImage,
            ProductImageReadDto,
            ProductImageUpdateDto,
            ProductImageCreateDto,
            ProductImageUpdateValidator,
            ProductImageCreateValidator
        >,
            IProductImageService
    {
        public IProductImageRepo _productImageRepo;
        public IImageService _imageService;

        public ProductImageService(IProductImageRepo productImageRepo, IImageService imageService)
            : base(productImageRepo)
        {
            _productImageRepo = productImageRepo;
            _imageService = imageService;
        }

        public override async Task<ProductImageReadDto> CreateAsync(
            ProductImageCreateDto createDto,
            Expression<Func<ProductImage, bool>>? checkCondition = null
        )
        {
            ProductImageCreateValidator dataValidator = new();
            var result = dataValidator.Validate(createDto);
            if (!result.IsValid)
            {
                throw new InvalidInputDataException(result.ErrorMessage);
            }

            var imageUrl = "";

            imageUrl = await _imageService.SaveImageAsync(createDto.Image);

            var productImage = new ProductImage();
            createDto.ToEntity(productImage);
            productImage.ImageUrl = imageUrl;

            productImage = await _productImageRepo.CreateAsync(productImage);
            ProductImageReadDto readDto = new();
            readDto.FromEntity(productImage);
            return readDto;
        }
    }
}
