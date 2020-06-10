namespace RolesAuthorizeApi.Providers.Auth
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public AuthToken Token { get; set; }
    }
}