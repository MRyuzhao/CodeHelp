using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CodeHelp.API.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : BaseController
    {
        // GET api/identity
        [HttpGet]
        public IActionResult Get()
        {
            var username = User.Claims.First(x => x.Type == "email").Value;
            return Ok(username);
        }
    }
}
