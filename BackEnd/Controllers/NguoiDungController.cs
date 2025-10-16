using AutoMapper;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.EF_Contexts;
using BackEnd.DTOs;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NguoiDungController( IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        // lấy tất cả người dùng
        [HttpGet("getall")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var nguoidungs = await _unitOfWork.NguoiDungs.GetAll();
            return Ok(nguoidungs);
        }
        // lấy người dùng theo mã người dùng
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nguoidung = await _unitOfWork.NguoiDungs.GetByIdAsync(id);
            if (nguoidung == null)
            {
                return NotFound();
            }
            return Ok(nguoidung);
        }
        // cập nhật người dùng theo mã người dùng
        [HttpPatch("update")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateById([FromBody] NguoiDungDTO user)
        {
            if (!await _unitOfWork.NguoiDungs.ExistIDAsync(user.Manguoidung))
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(user.Email) && await _unitOfWork.NguoiDungs.ExistEmail(user.Email))
            {
                return BadRequest(new { error = "ishas" });
            }
            Nguoidung nguoidung =_mapper.Map<Nguoidung>(user);
            var result = await _unitOfWork.NguoiDungs.UpdateById(nguoidung);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return NoContent();
        }
    }
}
