using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Shared;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate
{
    public class ReviewReadDto : BaseReadDto<Review>
    {
        public string Message { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public override void FromEntity(Review entity)
        {
            Message = entity.Message;
            ReviewDate = entity.Review_Date;
            Rate = entity.Rate;
            UserId = entity.UserId;
            ProductId = entity.ProductId;
            base.FromEntity(entity);
        }
    }

    public class ReviewCreateDto : ICreateDto<Review>
    {
        public string Message { get; set; }
        public int Rate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public void ToEntity(Review entity)
        {
            entity.Message = Message;
            entity.Review_Date = DateTime.UtcNow;
            entity.Rate = Rate;
            entity.UserId = UserId;
            entity.ProductId = ProductId;
            entity.Create_Date = DateTime.UtcNow;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ReviewUpdateDto : IUpdateDto<Review>
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public int? Rate { get; set; }

        public void UpdateEntity(Review entity)
        {
            entity.Message = Message ?? entity.Message;
            entity.Rate = Rate ?? entity.Rate;
            entity.Update_Date = DateTime.UtcNow;
        }
    }

    public class ReviewUpdateValidator : IDataValidator<ReviewUpdateDto>
    {
        public ReviewUpdateValidator()
        {
            RuleFor(
                x => x.Message,
                message => message != null && message.ToString().Length > 0,
                "The review field cannot be empty. Please provide your feedback."
            );
        }
    }

    public class ReviewCreateValidator : IDataValidator<ReviewCreateDto>
    {
        public ReviewCreateValidator()
        {
            RuleFor(
                x => x.Message,
                message => message != null && message.ToString().Length > 0,
                "The review field cannot be empty. Please provide your feedback."
            );
        }
    }
}
