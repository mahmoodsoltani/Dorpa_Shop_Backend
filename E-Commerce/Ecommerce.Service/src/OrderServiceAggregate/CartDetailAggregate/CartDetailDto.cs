using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.OrderServiceAggregate.CartDetailAggregate
{
    public class CartDetailReadDto : BaseReadDto<CartDetail>
    {
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }

        public override void FromEntity(CartDetail entity)
        {
            Quantity = entity.Quantity;
            UserId = entity.UserId;
            ProductId = entity.ProductId;
            Quantity = entity.Quantity;
            ProductName = entity.Product?.Name;
            Price = entity.Product?.Price;
            Discount = entity.Product?.Discount?.DiscountPercentage;
            base.FromEntity(entity);
        }
    }

    public class CartDetailCreateDto : ICreateDto<CartDetail>
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public void ToEntity(CartDetail entity)
        {
            entity.UserId = UserId;
            entity.ProductId = ProductId;
            entity.Quantity = Quantity;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class CartDetailUpdateDto : IUpdateDto<CartDetail>
    {
        public int Quantity { get; set; }
        public int Id { get; set; }

        public void UpdateEntity(CartDetail entity)
        {
            entity.Quantity = Quantity;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class CartDetailUpdateValidator : IDataValidator<CartDetailUpdateDto>
    {
        public CartDetailUpdateValidator() { }
    }

    public class CartDetailCreateValidator : IDataValidator<CartDetailCreateDto>
    {
        public CartDetailCreateValidator() { }
    }
}
