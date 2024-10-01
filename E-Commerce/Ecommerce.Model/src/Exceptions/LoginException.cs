using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string authenticationError)
            : base($"{authenticationError}") { }

        public LoginException()
            : base("Authentication failed.") { }
    }
}
