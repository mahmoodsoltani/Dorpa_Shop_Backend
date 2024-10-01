using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository.Shared;
using Ecommerce.Model.src.Entity.UserAggregate;
using Ecommerce.Model.src.Repository.UserRepoAggregate;
using Ecommerce.Model.src.Shared.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository.UserRepoAggregate
{
    public class AddressRepo(AppDbContext appDbContext)
        : BaseRepo<Address>(appDbContext),
            IAddressRepo { }
}
