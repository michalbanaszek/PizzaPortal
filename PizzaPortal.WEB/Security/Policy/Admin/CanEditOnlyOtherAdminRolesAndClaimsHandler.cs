﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Security.Policy.Admin
{
    public class CanEditOnlyOtherAdminRolesAndClaimsHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRequirement requirement)
        {
            var authFilterContext = context.Resource as AuthorizationFilterContext;

            if (authFilterContext == null)
            {
                return Task.CompletedTask;
            }

            string loggedInAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string adminEditedId = authFilterContext.HttpContext.Request.Query["userId"];

            if (context.User.IsInRole("Admin") && 
                context.User.HasClaim(claim => claim.Type == "Edit Role" && 
                                      claim.Value == "true" &&
                                      loggedInAdminId.ToLower() != adminEditedId.ToLower()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
