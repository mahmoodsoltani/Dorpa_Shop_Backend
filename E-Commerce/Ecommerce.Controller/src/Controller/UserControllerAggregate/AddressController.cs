using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;
using Ecommerce.Service.src.UserServiceAggregate.AddressAggregate;

namespace Ecommerce.Controller.src.Controller.UserControllerAggregate
{
    public class AddressController(IAddressService addressService)
        : BaseController<
            Address,
            AddressReadDto,
            AddressUpdateDto,
            AddressCreateDto,
            AddressUpdateValidator,
            AddressCreateValidator
        >(addressService) { }
}
