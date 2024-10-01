using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException()
            : base("Duplicate Entity.") { }
    }
}
