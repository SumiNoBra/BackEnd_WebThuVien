using AutoMapper;
using API.DTOs;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;

namespace BackEnd.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _generateJwtToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGoogleAuthService _authService;
        public AuthController(JwtTokenService generateJwtToken,IMapper mapper,IUnitOfWork unitOfWork,IGoogleAuthService googleAuthService )
        {
            _authService = googleAuthService;
            _mapper = mapper;
            _generateJwtToken = generateJwtToken;
            _unitOfWork = unitOfWork;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDTo userLogin)
        {
            string vaitro = "";
            Nguoidung user = _mapper.Map<Nguoidung>(userLogin);
            int isValidUser = await _unitOfWork.NguoiDungs.ExistNguoiDungAsync(user.Email!,user.Matkhau, vaitro);
            if (isValidUser != -1)
            {
                if (vaitro == "admin")
                {
                    var token = _generateJwtToken.Generate(isValidUser, user.Email!, "Admin");
                    return Ok(new { Token = token });
                }
                else
                {
                    var token = _generateJwtToken.Generate(isValidUser, user.Email!, "User");
                    return Ok(new { Token = token });
                }
            }
            return NotFound(new
            {
                token = ""
            });

        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO model)
        {
            if (string.IsNullOrEmpty(model.IdToken))
            {
                return BadRequest(new { Message = "ID Token is required." });
            }

            try
            {
                var result = await _authService.HandleGoogleLoginAsync(model.IdToken);

                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        Message = "Login successful",
                        Token = result.CustomJwtToken 
                    });
                }
                else
                {
                    return Unauthorized(new { Message = result.ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userRegister)
        {
            Console.WriteLine("");
            Nguoidung user = _mapper.Map<Nguoidung>(userRegister);
            bool result = await _unitOfWork.NguoiDungs.ExistIDAsync(user.Manguoidung);
            bool emailExists = await _unitOfWork.NguoiDungs.ExistEmail(user.Email!);
            if (result || emailExists)
            {
                return Conflict(new { error = "ishas" });
            }
            await _unitOfWork.NguoiDungs.AddAsync(user);

            await _unitOfWork.CompleteAsync();
            return Ok(new { message = "Đăng ký thành công." });
        }
    }
}
