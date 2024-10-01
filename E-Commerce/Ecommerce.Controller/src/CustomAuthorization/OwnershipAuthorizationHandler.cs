using System.Reflection;
using System.Security.Claims;
using Ecommerce.Model.src;
using Ecommerce.Model.src.Entity.UserAggregate;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controller.src.CustomAuthorization
{
    public class OwnershipAuthorizationRequirement : IAuthorizationRequirement { }

    public class OwnershipAuthorizationHandler
        : AuthorizationHandler<OwnershipAuthorizationRequirement, BaseEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OwnershipAuthorizationRequirement requirement,
            BaseEntity resource
        )
        {
            var claims = context.User;
            var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            var property = resource
                .GetType()
                .GetProperty(
                    "userId",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase
                );
            if (property != null)
                if (property.GetValue(resource)?.ToString() == userId?.Value)
                {
                    context.Succeed(requirement);
                }
            return Task.CompletedTask;
        }
    }
}
