using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Implementation;

namespace Ecommerce.Service.src.UserServiceAggregate.AddressAggregate
{
    public class AddressService(IAddressRepo addressRepo)
        : BaseService<
            Address,
            AddressReadDto,
            AddressUpdateDto,
            AddressCreateDto,
            AddressUpdateValidator,
            AddressCreateValidator
        >(addressRepo),
            IAddressService { }
}
