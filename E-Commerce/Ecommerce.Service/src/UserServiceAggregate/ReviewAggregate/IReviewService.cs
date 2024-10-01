using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate
{
    public interface IReviewService
        : IBaseService<
            Review,
            ReviewReadDto,
            ReviewUpdateDto,
            ReviewCreateDto,
            ReviewUpdateValidator,
            ReviewCreateValidator
        > { }
}
