using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.OrderAggregate;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Model.src.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.ProductRepoAggreate
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext context) : base(context)
        {
        }

        // Additional logic specific to Category can be implemented here.

    }
}
