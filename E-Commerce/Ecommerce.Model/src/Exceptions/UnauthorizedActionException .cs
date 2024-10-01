using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public string Action { get; }

        public UnauthorizedActionException(string action)
            : base($"You are not authorized to perform the action: {action}")
        {
            Action = action;
        }

        public UnauthorizedActionException()
            : base($"You are not authorized to perform the action") { }
    }
}
