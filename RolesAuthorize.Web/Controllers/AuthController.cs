using Microsoft.AspNetCore.Mvc;
using RolesAuthorize.Contracts.Interfaces;
using RolesAuthorize.Contracts.Models;

namespace RolesAuthorize.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository repo)
        {
            _authRepository = repo;
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult Validate(LoginModel model)
        {
            return Ok(_authRepository.Authenticate(model));
        }
    }
}