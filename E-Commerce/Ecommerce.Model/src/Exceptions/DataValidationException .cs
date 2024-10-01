using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class DataValidationException : Exception
    {
        public DataValidationException()
            : base("Data validation failed.") { }

        public DataValidationException(string message)
            : base(message) { }
    }
}
