using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.OrderRepoAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.OrderRepoAggregate
{
    public class OrderDetailRepo(AppDbContext context)
        : BaseRepo<OrderDetail>(context),
            IOrderDetailRepo { }
}
