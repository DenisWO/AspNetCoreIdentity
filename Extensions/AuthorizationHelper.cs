using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentity.Extensions
{
    public class PermissionRequired : IAuthorizationRequirement
    {
        public string Permission { get;  }

        public PermissionRequired(string permission)
        {
            Permission = permission;
        }
    }

    public class PermissionRequiredHandler : AuthorizationHandler<PermissionRequired>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequired requirement)
        {
            if (context.User.HasClaim(c => c.Type.Equals("Permission") && c.Value.Contains(requirement.Permission)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
