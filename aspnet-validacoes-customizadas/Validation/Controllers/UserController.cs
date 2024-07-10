using Microsoft.AspNetCore.Mvc;
using Validation.Models;

namespace Validation.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("/")]
        public IActionResult Post([FromBody] CreateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok();
        }
    }
}