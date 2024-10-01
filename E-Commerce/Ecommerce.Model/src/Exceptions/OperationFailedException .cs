using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class OperationFailedException : Exception
    {
        public string Operation { get; }
        public string Reason { get; }

        public OperationFailedException(string operation, string reason)
            : base($"Operation '{operation}' failed: {reason}")
        {
            Operation = operation;
            Reason = reason;
        }

        public OperationFailedException()
            : base("Operation failed.") { }
    }
}
