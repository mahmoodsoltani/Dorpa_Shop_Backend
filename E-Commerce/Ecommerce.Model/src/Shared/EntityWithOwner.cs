using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Model.src.Shared
{
    public interface IEntityWithOwner
    {
        public int UserId { get; set; }
    }
}
