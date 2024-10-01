using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.UserServiceAggregate.ReviewAggregate
{
    public class ReviewService(IReviewRepo reviewRepo)
        : BaseService<
            Review,
            ReviewReadDto,
            ReviewUpdateDto,
            ReviewCreateDto,
            ReviewUpdateValidator,
            ReviewCreateValidator
        >(reviewRepo),
            IReviewService { }
}
