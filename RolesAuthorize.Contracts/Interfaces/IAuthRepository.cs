using RolesAuthorize.Contracts.Models;

namespace RolesAuthorize.Contracts.Interfaces
{
    public interface IAuthRepository
    {
        AuthResult Authenticate(LoginModel credentials);
    }
}