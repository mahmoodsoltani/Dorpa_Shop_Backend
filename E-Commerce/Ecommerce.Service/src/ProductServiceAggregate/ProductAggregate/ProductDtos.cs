using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.CategoryAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.DiscountAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;
using Ecommerce.Service.src.UserServiceAggregate.FavoutiteAggregate;
using Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate
{
    public class ProductReadDto : BaseReadDto<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? BrandName { get; set; }
        public string? ColorCode { get; set; }
        public string? SizeName { get; set; }
        public string? CategoryName { get; set; }
        public string? AltTxt { get; set; }
        public decimal? DiscountPercentage { get; set; }

        // Navigation properties
        public BrandReadDto? Brand { get; set; }
        public CategoryReadDto? Category { get; set; }
        public DiscountReadDto? Discount { get; set; }
        public decimal? Rate { get; set; }
        public List<ProductImageReadDto>? ProductImages { get; set; }
        public List<ReviewReadDto>? Reviews { get; set; }
        public List<FavouriteReadDto>? Favourites { get; set; }

        public override void FromEntity(Product entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            BrandId = entity.BrandId;
            CategoryId = entity.CategoryId;
            SizeId = entity.SizeId;
            ColorId = entity.ColorId;
            Price = entity.Price;
            Stock = entity.Stock;
            if (entity.Color != null)
            {
                ColorCode = entity.Color.Code;
            }

            if (entity.Size != null)
            {
                SizeName = entity.Size.Name;
            }
            if (entity.Brand != null)
            {
                Brand = new BrandReadDto();
                BrandName = entity.Brand.Name;
                Brand.FromEntity(entity.Brand);
            }

            if (entity.Discount != null)
            {
                Discount = new DiscountReadDto();
                DiscountPercentage = entity.Discount.DiscountPercentage;
                Discount.FromEntity(entity.Discount);
            }
            if (entity.Category != null)
            {
                Category = new CategoryReadDto();
                CategoryName = entity.Category.Name;
                Category.FromEntity(entity.Category);
            }

            if (entity.ProductImages != null)
            {
                ProductImages = entity
                    .ProductImages?.Select(pi =>
                    {
                        var imageDto = new ProductImageReadDto();
                        imageDto.FromEntity(pi);
                        return imageDto;
                    })
                    .ToList();
                var PrimaryImage = entity.ProductImages?.FirstOrDefault(pi => pi.IsPrimary);
                if (PrimaryImage != null)
                {
                    ImageUrl = PrimaryImage.ImageUrl;
                    AltTxt = PrimaryImage.AltText;
                }
            }
            if (entity.Reviews != null)
            {
                Reviews = entity
                    .Reviews?.Select(r =>
                    {
                        var reviewDto = new ReviewReadDto();
                        reviewDto.FromEntity(r);
                        return reviewDto;
                    })
                    .ToList();
            }
            if (entity.Favourites != null)
            {
                Favourites = entity
                    .Favourites?.Select(r =>
                    {
                        var FavouriteDto = new FavouriteReadDto();
                        FavouriteDto.FromEntity(r);
                        return FavouriteDto;
                    })
                    .ToList();
            }
            base.FromEntity(entity);
        }
    }

    public class ProductCreateDto : ICreateDto<Product>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? BrandId { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public string? AltText { get; set; } = "Image";

        // public List<ProductImageCreateDto>? Images { get; set; }

        public void ToEntity(Product entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.BrandId = BrandId;
            entity.CategoryId = CategoryId;
            entity.SizeId = SizeId ?? 0;
            entity.ColorId = ColorId ?? 0;
            entity.Price = Price;
            entity.Stock = Stock;

            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ProductUpdateDto : IUpdateDto<Product>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? BrandId { get; set; }
        public int? Stock { get; set; }
        public int? CategoryId { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public decimal? Price { get; set; }

        public void UpdateEntity(Product entity)
        {
            entity.Name = Name ?? entity.Name;
            entity.Description = Description ?? entity.Description;
            entity.BrandId = BrandId ?? entity.BrandId;
            entity.CategoryId = CategoryId ?? entity.CategoryId;
            entity.SizeId = SizeId ?? entity.SizeId;
            entity.ColorId = ColorId ?? entity.ColorId;
            entity.Stock = Stock ?? entity.Stock;
            entity.Price = Price ?? entity.Price;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ProductUpdateValidator : IDataValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator() { }
    }

    public class ProductCreateValidator : IDataValidator<ProductCreateDto>
    {
        public ProductCreateValidator() { }
    }
}
