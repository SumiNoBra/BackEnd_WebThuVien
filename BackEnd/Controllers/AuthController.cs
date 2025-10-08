using BackEnd.DTo;
using BackEnd.EF_Contexts;
using BackEnd.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GenerateJwtToken _generateJwtToken;
        private readonly INguoiDungRepository _nguoiDung;
        public AuthController(GenerateJwtToken generateJwtToken,INguoiDungRepository nguoiDung)
        {
            _generateJwtToken = generateJwtToken;
            _nguoiDung = nguoiDung;
        }
        [Route("login")]
        [HttpPost]
        public async Task <IActionResult> Post([FromBody] LoginDTo userLogin)
        {
            try
            {
                string vaitro = "";
                int isValidUser = await _nguoiDung.DangNhap(userLogin.Email, userLogin.Password,vaitro);
                if (isValidUser != -1)
                {
                    if (vaitro == "admin")
                    {

                        var token = _generateJwtToken.Generate(isValidUser, userLogin.Email, "Admin");
                        return Ok(new { Token = token });
                    }
                    else
                    {
                        var token = _generateJwtToken.Generate(isValidUser, userLogin.Email, "User");
                        return Ok(new { Token = token });
                    }
                }
                return NotFound(new
                {
                    token = ""
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = "isnull" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error =  ex.Message});
            }
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userRegister)
        {
            try
            {
                int registerResult = await _nguoiDung.DangKy(userRegister);
                if (registerResult <= 0)
                {
                    return BadRequest(new { error = "uncorrect" });
                }
                return Ok(new { message = "Đăng ký thành công." });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = "isnull" });
            }
            catch (ArgumentException ex)
            {
                return Conflict(new { error = "ishas"  });
            }
            catch(InvalidCastException ex)
            {
                return StatusCode(500, new { error = "Đã xảy ra lỗi trong quá trình đăng ký." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Đã xảy ra lỗi trong quá trình đăng ký." });
            }
        }
        //[Authorize(Policy = "Admin")]
        //[Route("testlogin")]
        //[HttpGet]
        //public IActionResult TestLogin()
        //{
        //    return Ok(new
        //    {
        //        data = "login voi toke thanh cong"
        //    });
        //}
    }
}
