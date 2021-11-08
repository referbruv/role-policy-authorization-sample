using RolesAuthorize.Contracts.Models;
using RolesAuthorize.Contracts.Models.User;

namespace RolesAuthorize.Contracts.Interfaces
{
    public interface ITokenManager
    {
        AuthToken Generate(User user);
    }
}