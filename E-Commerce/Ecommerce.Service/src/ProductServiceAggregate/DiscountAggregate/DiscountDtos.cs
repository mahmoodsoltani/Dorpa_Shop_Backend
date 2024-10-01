using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.DiscountAggregate
{
    public class DiscountReadDto : BaseReadDto<Discount>
    {
        public int ProductId { get; set; }

        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Product Product { get; set; }

        public override void FromEntity(Discount entity)
        {
            DiscountPercentage = entity.DiscountPercentage;
            StartDate = entity.StartDate;
            EndDate = entity.EndDate;
            ProductId = entity.ProductId;
            base.FromEntity(entity);
        }
    }

    public class DiscountCreateDto : ICreateDto<Discount>
    {
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProductId { get; set; }

        public void ToEntity(Discount entity)
        {
            entity.DiscountPercentage = DiscountPercentage;
            entity.StartDate = StartDate;
            entity.EndDate = EndDate;
            entity.ProductId = ProductId;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class DiscountUpdateDto : IUpdateDto<Discount>
    {
        public int Id { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void UpdateEntity(Discount entity)
        {
            entity.DiscountPercentage = DiscountPercentage ?? entity.DiscountPercentage;
            entity.StartDate = StartDate ?? entity.StartDate;
            entity.EndDate = EndDate ?? entity.EndDate;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class DiscountUpdateValidator : IDataValidator<DiscountUpdateDto>
    {
        public DiscountUpdateValidator() { }
    }

    public class DiscountCreateValidator : IDataValidator<DiscountCreateDto>
    {
        public DiscountCreateValidator() { }
    }
}
