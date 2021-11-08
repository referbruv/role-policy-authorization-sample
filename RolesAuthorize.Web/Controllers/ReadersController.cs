using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolesAuthorize.Contracts.Models.User;
using RolesAuthorize.Core.Providers;

namespace RolesAuthorize.Web.Controllers
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