using RolesAuthorize.Contracts.Interfaces;
using RolesAuthorize.Contracts.Models;

namespace RolesAuthorize.Core.Providers.Repositories
{
    public class ExtendedAuthRepository : AuthRepository
    {
        public ExtendedAuthRepository(ITokenManager tokenManager) : base(tokenManager)
        {
        }

        public override AuthResult Authenticate(LoginModel credentials)
        {
            return base.Authenticate(credentials);
        }
    }
}