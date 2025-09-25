using MainAPI.Models.DTo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Login
{
    [Route("login")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly GenerateJwtToken generateJwtToken;
        public Login(GenerateJwtToken _generateJwtToken)
        {
            generateJwtToken = _generateJwtToken;
        }
        [HttpPost]
        public IActionResult Post([FromBody] LoginDTo userLogin)
        {
            if (userLogin.UserName == "admin" && userLogin.Password == "password")
            {
                var token = generateJwtToken.Generate("1", userLogin.Password, "Admin");
                return Ok(new { Token = token });
            }
            else if (userLogin.UserName == "user" && userLogin.Password == "password")
            {
                var token = generateJwtToken.Generate("2", userLogin.Password, "User");
                return Ok(new { Token = token });
            }
            return NotFound(new
            {
                token = ""
            });
        }
        [Authorize(Policy = "Admin")]
        [Route("testlogin")]
        [HttpGet]
        public IActionResult TestLogin()
        {
            return Ok("Route Admin");
        }
    }
}
