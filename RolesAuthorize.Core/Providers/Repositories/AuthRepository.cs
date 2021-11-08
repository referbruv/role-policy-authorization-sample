using RolesAuthorize.Contracts.Interfaces;
using RolesAuthorize.Contracts.Models;
using System.Linq;

namespace RolesAuthorize.Core.Providers.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private ITokenManager _tokenManager;

        public AuthRepository(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public virtual AuthResult Authenticate(LoginModel credentials)
        {
            var user = ReaderStore.Users.FirstOrDefault(x => x.EmailAddress == credentials.Email);

            if (user != null)
            {
                return new AuthResult
                {
                    IsSuccess = true,
                    Token = _tokenManager.Generate(user)
                };
            }

            return new AuthResult { IsSuccess = false };
        }
    }
}