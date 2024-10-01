using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string message = "Registration Failed.")
            : base("Error:" + message) { }
    }
}
