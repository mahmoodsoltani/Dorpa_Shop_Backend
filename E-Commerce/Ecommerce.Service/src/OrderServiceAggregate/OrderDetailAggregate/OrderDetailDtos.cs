using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.OrderDetailAggregate
{
    public class OrderDetailReadDto : BaseReadDto<OrderDetail>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Quantity { get; set; }

        public override void FromEntity(OrderDetail entity)
        {
            OrderId = entity.OrderId;
            ProductId = entity.ProductId;
            Price = entity.Price;
            if (entity.Product != null)
                ProductName = entity.Product.Name;
            Discount = entity.Discount;
            Quantity = entity.Quantity;
            base.FromEntity(entity);
        }
    }

    public class OrderDetailCreateDto : ICreateDto<OrderDetail>
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public void ToEntity(OrderDetail entity)
        {
            entity.OrderId = OrderId;
            entity.Price = Price;
            entity.Quantity = Quantity;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class OrderDetailUpdateDto : IUpdateDto<OrderDetail>
    {
        public int Quantity { get; set; }
        public int Id { get; set; }

        public void UpdateEntity(OrderDetail entity)
        {
            entity.Quantity = Quantity;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class OrderDetailUpdateValidator : IDataValidator<OrderDetailUpdateDto>
    {
        public OrderDetailUpdateValidator() { }
    }

    // Validator for OrderDetailCreateDto
    public class OrderDetailCreateValidator : IDataValidator<OrderDetailCreateDto>
    {
        public OrderDetailCreateValidator() { }
    }
}
