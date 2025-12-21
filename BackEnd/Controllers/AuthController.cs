using API.DTOs;
using Application.Interfaces;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers_V2
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _generateJwtToken;
        private readonly IMapper _mapper;
        private readonly IDocGiaRepo _docgiarepo;
        private readonly IGoogleAuthService _authService;
        public AuthController(IJwtTokenService generateJwtToken,IMapper mapper,IDocGiaRepo docgia, IGoogleAuthService googleAuthService) { 
            _mapper = mapper;
            _docgiarepo = docgia;
            _generateJwtToken = generateJwtToken;
            _authService = googleAuthService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTo userlogin)
        {
            if (userlogin == null || string.IsNullOrEmpty(userlogin.Email) || string.IsNullOrEmpty(userlogin.MatKhau))
            {
                return BadRequest(new { message = "Vui lòng nhập Email và Mật khẩu." });
            }
            DocGia? userEntity = await _docgiarepo.GetByEmailAsync(userlogin.Email);
            if (userEntity==null)
            {
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không chính xác." });
            }
            string vaitro = userEntity.VaiTro ?? "User";
            var accessToken = _generateJwtToken.GenerateAccessToken(userEntity.MaDocGia, userEntity.Email!, vaitro);
            var refreshToken = _generateJwtToken.GenerateRefreshToken();

            await _docgiarepo.SaveRefreshTokenAsync(userEntity.MaDocGia, refreshToken.Token, refreshToken.ExpiryDate);
            SetRefreshTokenCookie(refreshToken.Token, refreshToken.ExpiryDate);
            var userResponse = new
            {
                Id = userEntity.MaDocGia,
                Email = userEntity.Email,
                HoTen = userEntity.HoTen,
                VaiTro = vaitro
            };
            return Ok(new
            {
                success = true,
                message = "Đăng nhập thành công",
                accessToken = accessToken,
                user = userResponse
            });
        }
        private void SetRefreshTokenCookie(string token, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,    
                Expires = expires, 
                Secure = true,      
                SameSite = SameSiteMode.Strict,
                IsEssential = true
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userRegister)
        {
            DocGia user = _mapper.Map<DocGia>(userRegister);
            bool emailExists = await _docgiarepo.ExistEmail(user.Email!);
            if (emailExists)
            {
                return Conflict();
            }
            await _docgiarepo.CreateDocGia(user);
            return Created();
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
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest(new { message = "Không tìm thấy Refresh Token trong Cookie." });
            }
            var user = await _docgiarepo.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                return Unauthorized(new { message = "Token không hợp lệ." });
            }

            if (user.RefreshToken != refreshToken)
            {
                return Unauthorized(new { message = "Token không khớp." });
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại." });
            }
            var newAccessToken = _generateJwtToken.GenerateAccessToken(user.MaDocGia, user.Email!, user.VaiTro!);
            return Ok(new
            {
                success = true,
                accessToken = newAccessToken
            });
        }
    }
}
