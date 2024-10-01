using System;

namespace Ecommerce.Model.src.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string? EntityName { get; }
        public object? EntityId { get; }

        public EntityNotFoundException(string entityName, object entityId)
            : base($"{entityName} with ID {entityId} not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public EntityNotFoundException(string entityName)
            : base($"{entityName}  not found.")
        {
            EntityName = entityName;
        }

        public EntityNotFoundException()
            : base("Entity not found.") { }
    }
}
