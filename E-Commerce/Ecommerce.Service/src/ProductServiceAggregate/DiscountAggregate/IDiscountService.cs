using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.Dto;
using Ecommerce.Service.src.Shared;
using Ecommerce.Service.src.Shared.Interface;

namespace Ecommerce.Service.src.ProductServiceAggregate.DiscountAggregate
{
    public interface IDiscountService
        : IBaseService<
            Discount,
            DiscountReadDto,
            DiscountUpdateDto,
            DiscountCreateDto,
            DiscountUpdateValidator,
            DiscountCreateValidator
        > { }
}
