using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolesAuthorizeApi.Providers;

namespace RolesAuthorizeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadersController : ControllerBase
    {
        // The Roles expects a comma separated list of Role values
        [Authorize(Roles = "Admin,Editor")]
        [Authorize("ShouldContainRole")]
        [Route("all")]
        [HttpGet]
        public List<Reader> Get()
        {
            return ReaderStore.Readers;
        }
    }
}