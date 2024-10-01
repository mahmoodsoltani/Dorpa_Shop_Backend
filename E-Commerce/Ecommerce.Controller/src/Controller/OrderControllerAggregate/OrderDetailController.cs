using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Service.src.OrderServiceAggregate.OrderDetailAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.BrandAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.OrderDetailAggregate;
using Ecommerce.Service.src.ProductServiceAggregate.ProductAggregate;

namespace Ecommerce.Controller.src.Controller.OrderControllerAggregate
{
    public class OrderDetailController(IOrderDetailService orderDetailService)
        : BaseController<
            OrderDetail,
            OrderDetailReadDto,
            OrderDetailUpdateDto,
            OrderDetailCreateDto,
            OrderDetailUpdateValidator,
            OrderDetailCreateValidator
        >(orderDetailService) { }
}
