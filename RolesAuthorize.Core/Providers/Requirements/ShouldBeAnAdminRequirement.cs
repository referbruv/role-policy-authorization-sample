using Microsoft.AspNetCore.Authorization;

namespace RolesAuthorize.Core.Providers.Requirements
{
    public class ShouldBeAnAdminRequirement
    : IAuthorizationRequirement
    {
    }
}