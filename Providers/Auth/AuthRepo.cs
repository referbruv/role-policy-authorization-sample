using System.Linq;

namespace RolesAuthorizeApi.Providers.Auth
{
    public interface IAuthRepo
    {
        AuthResult Authenticate(LoginModel credentials);
    }

    public class AuthRepo : IAuthRepo
    {
        private ITokenManager _tokenManager;

        public AuthRepo(ITokenManager tokenManager)
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

    public class ExtendedAuthRepo : AuthRepo
    {
        public ExtendedAuthRepo(ITokenManager tokenManager) : base(tokenManager)
        {
        }

        public override AuthResult Authenticate(LoginModel credentials)
        {
            return base.Authenticate(credentials);
        }
    }
}