using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RolesAuthorizeApi.Providers.Auth
{
    public class ShouldBeAReaderRequirement
    : IAuthorizationRequirement
    {
    }

    public class ShouldBeAnAdminRequirement
    : IAuthorizationRequirement
    {
    }

    public class ShouldBeAnAdminRequirementHandler
    : AuthorizationHandler<ShouldBeAnAdminRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ShouldBeAnAdminRequirement requirement)
        {
            // check if Role claim exists - Else Return
            // (sort of Claim-based requirement)
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
                return Task.CompletedTask;

            // claim exists - retrieve the value
            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            var role = claim.Value;

            // check if the claim equals to either Admin or Editor
            // if satisfied, set the requirement as success
            if (role == Roles.Admin || role == Roles.Editor)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }

    public class ShouldBeAReaderAuthorizationHandler
    : AuthorizationHandler<ShouldBeAReaderRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ShouldBeAReaderRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Email))
                return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var emailAddress = claim.Value;

            if (ReaderStore.Readers.Any(x => x.EmailAddress == emailAddress))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}