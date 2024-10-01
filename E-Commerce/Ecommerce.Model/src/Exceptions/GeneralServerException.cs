using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class GeneralServerException : Exception
    {
        public GeneralServerException(string message)
            : base(message) { }

        public GeneralServerException()
            : base("Internal server error.") { }
    }
}
