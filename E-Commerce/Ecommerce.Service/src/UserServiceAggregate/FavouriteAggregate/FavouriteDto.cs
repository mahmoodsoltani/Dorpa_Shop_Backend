using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.FavoutiteAggregate
{
    public class FavouriteReadDto : BaseReadDto<Favourite>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Updated_Date { get; set; }
        public ProductReadDto Product { get; set; }
        public override void FromEntity(Favourite entity)
        {
            UserId = entity.UserId;
            ProductId = entity.ProductId;
             if (entity.Product != null)
            {
                Product = new ProductReadDto();
                Product.FromEntity(entity.Product);
            }
            base.FromEntity(entity);
        }
    }

    public class FavouriteCreateDto : ICreateDto<Favourite>
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public void ToEntity(Favourite entity)
        {
            entity.UserId = UserId;
            entity.ProductId = ProductId;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class FavouriteUpdateDto : IUpdateDto<Favourite>
    {
        public int Id { get; set; }

        public void UpdateEntity(Favourite entity)
        {
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class FavouriteUpdateValidator : IDataValidator<FavouriteUpdateDto>
    {
        public FavouriteUpdateValidator() { }
    }

    public class FavouriteCreateValidator : IDataValidator<FavouriteCreateDto>
    {
        public FavouriteCreateValidator() { }
    }
}
