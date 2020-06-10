using Microsoft.AspNetCore.Mvc;
using RolesAuthorizeApi.Providers.Auth;

namespace RolesAuthorizeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo repo;

        public AuthController(IAuthRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult Validate(LoginModel model)
        {
            return Ok(this.repo.Authenticate(model));
        }

        [HttpGet]
        [Route("alive")]
        public IActionResult Alive()
        {
            return Ok("Yes! I am alive.");
        }
    }
}