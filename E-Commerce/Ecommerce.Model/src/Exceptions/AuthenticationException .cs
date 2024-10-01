using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class AuthenticationException : Exception
    {
        public string AuthenticationError { get; }

        public AuthenticationException(string authenticationError)
            : base($"Authentication failed: {authenticationError}")
        {
            AuthenticationError = authenticationError;
        }

        public AuthenticationException()
            : base("Authentication failed") { }
    }
}
