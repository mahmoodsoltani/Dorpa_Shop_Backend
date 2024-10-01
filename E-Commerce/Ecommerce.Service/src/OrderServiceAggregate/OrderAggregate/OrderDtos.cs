using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.ProductServiceAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.OrderAggregate
{
    public class OrderReadDto : BaseReadDto<Order>
    {
        public DateTime OrderDate { get; set; }
        public decimal? Total { get; set; }
        public decimal? Discount { get; set; }
        public int UserId { get; set; }

        public List<OrderDetailReadDto>? OrderDetails { get; set; } = [];

        public override void FromEntity(Order entity)
        {
            OrderDate = entity.OrderDate;
            Total = entity.Total;
            Discount = entity.Discount;
            UserId = entity.UserId;
            if (entity.OrderDetails != null)
            {
                OrderDetails =
                    entity
                        .OrderDetails?.Select(oi =>
                        {
                            OrderDetailReadDto orderDetailReadDto = new();
                            orderDetailReadDto.FromEntity(oi);
                            return orderDetailReadDto;
                        })
                        ?.ToList() ?? [];
            }
            base.FromEntity(entity);
        }
    }

    public class OrderCreateDto : ICreateDto<Order>
    {
        public void ToEntity(Order entity) { }
    }

    public class OrderUpdateDto : IUpdateDto<Order>
    {
        public int Id { get; set; }

        public void UpdateEntity(Order entity)
        {
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class OrderUpdateValidator : IDataValidator<OrderUpdateDto>
    {
        public OrderUpdateValidator() { }
    }

    public class OrderCreateValidator : IDataValidator<OrderCreateDto>
    {
        public OrderCreateValidator() { }
    }
}
