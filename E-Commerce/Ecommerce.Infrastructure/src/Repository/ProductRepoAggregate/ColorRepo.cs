using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Model.src.Entity.ProductAggregate;
using Ecommerce.Model.src.Exceptions;
using Ecommerce.Model.src.Repository.ProductRepoAggreate;
using Ecommerce.Model.src.Shared.ValueObject;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.src.Repository.ProductRepoAggreate
{
    public class ColorRepo : BaseRepo<Color>, IColorRepo
    {
        public ColorRepo(AppDbContext context) : base(context)
        {
        }

         // Additional logic specific to Color can be implemented here.
       
    }
}
