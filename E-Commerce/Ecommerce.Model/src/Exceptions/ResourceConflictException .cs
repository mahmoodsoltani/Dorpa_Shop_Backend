using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class ResourceConflictException : Exception
    {
        public string ResourceName { get; }
        public string ConflictReason { get; }

        public ResourceConflictException(string resourceName, string conflictReason)
            : base($"Resource conflict: {resourceName} - {conflictReason}")
        {
            ResourceName = resourceName;
            ConflictReason = conflictReason;
        }

        public ResourceConflictException()
            : base($"Resource conflict.") { }
    }
}
