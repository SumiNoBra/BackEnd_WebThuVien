using AutoMapper;
using BackEnd.DTo;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthController(GenerateJwtToken generateJwtToken,IMapper mapper,IUnitOfWork unitOfWork)
        {
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
