using Microsoft.AspNetCore.Mvc;
using S4NET6.Models;
using S4NET6.Services;

namespace S4NET6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtServices _jwtServices;
        public AuthController(IJwtServices jwtServices)
        {
            this._jwtServices = jwtServices;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserInfo model)
        {
            if (model.Username == "admin" && model.Password == "123456") {
                var token = _jwtServices.GenToken(model.Username, "admin");
                return Ok(new
                {
                    token,
                    user = model.Username
                });
            }

            return BadRequest();
        }

    }
}
