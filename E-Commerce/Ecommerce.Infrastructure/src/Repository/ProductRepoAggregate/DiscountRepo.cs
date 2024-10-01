using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.src.Repository.ProductRepoAggreate
{
    public class DiscountRepo : BaseRepo<Discount>, IDiscountRepo
    {
        public DiscountRepo(AppDbContext context) : base(context)
        {
        }

        // Additional logic specific to Discount can be implemented here.
       
    }
}
