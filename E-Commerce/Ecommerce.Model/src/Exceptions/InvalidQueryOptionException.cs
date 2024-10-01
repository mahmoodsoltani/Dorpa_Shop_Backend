using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class InvalidQueryOptionException : Exception
    {
        public string Option { get; }
        public string Reason { get; }

        public InvalidQueryOptionException(string option, string reason)
            : base($"Invalid option value for '{option}' : {reason}")
        {
            Option = option;
            Reason = reason;
        }

        public InvalidQueryOptionException(string message)
            : base(message) { }
    }
}
