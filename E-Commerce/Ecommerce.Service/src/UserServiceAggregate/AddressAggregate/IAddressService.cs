using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.UserServiceAggregate.AddressAggregate
{
    public interface IAddressService
        : IBaseService<
            Address,
            AddressReadDto,
            AddressUpdateDto,
            AddressCreateDto,
            AddressUpdateValidator,
            AddressCreateValidator
        > { }
}
