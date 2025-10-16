using AutoMapper;
using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Controllers
{
    [Route("api/sach")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapping;
        public SachController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapping = mapper;
            _unitOfWork = unitOfWork;
        }
        // lấy tất cả sách
        [HttpGet]
        [Route("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<Sach> result = await _unitOfWork.Saches.GetAllAsync();
            List<SachDTO> resultDTo = _mapping.Map<List<SachDTO>>(result);
            return Ok(resultDTo);
        }
        // lấy sách theo mã sách
        [HttpGet("{masach}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMasach(int masach)
        {
            Sach result = await _unitOfWork.Saches.GetByIDAsync(masach);
            if (result == null)
            {
                return NotFound();
            }
            SachDTO resultDTO = _mapping.Map<SachDTO>(result);
            return Ok(resultDTO);
        }
        // lấy sách theo thể loại
        [HttpGet("theloai/{matheloai}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTheLoai(int matheloai)
        {
            List<Sach> result = await _unitOfWork.Saches.GetByTheLoaiIDAsync(matheloai);
            if (result == null)
            {
                return NotFound();
            }
            List<SachDTO> mappedResult = _mapping.Map<List<SachDTO>>(result);
            return Ok(mappedResult);
        }
        [HttpGet("search")]
        [AllowAnonymous]
        // tìm kiếm sách theo tên sách
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            List<Sach> result = await _unitOfWork.Saches.GetByNameAsync(keyword);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            List<SachDTO> resultDTO = _mapping.Map<List<SachDTO>>(result);
            return Ok(resultDTO);
        }
        [HttpGet("nam/{nam}")]
        [AllowAnonymous]
        // lấy sách theo năm xuất bản
        public async Task<IActionResult> GetByNam(int nam)
        {
            List<Sach> result = await _unitOfWork.Saches.GetByNamAsync(nam);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            List<SachDTO> resultDTO = _mapping.Map<List<SachDTO>>(result);
            return Ok(resultDTO);
        }
        // cập nhật sách
        [HttpPatch("update")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SachDTO sachdto)
        {
            Sach sach = _mapping.Map<Sach>(sachdto);
            await _unitOfWork.Saches.UpdateAsync(sach);
            await _unitOfWork.CompleteAsync();
            return Ok(new { message = "Cập nhật sách thành công." });

        }
        // thêm sách
        [HttpPost("add")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] SachDTO sachdto)
        {
            Sach sach = _mapping.Map<Sach>(sachdto);
            await _unitOfWork.Saches.AddAsync(sach);
            await _unitOfWork.CompleteAsync();
            return Ok(new { message = "Thêm sách thành công." });

        }
        // lấy sách theo trạng thái 
        [HttpGet("trangthai/{trangthai}")]
        public async Task<IActionResult> GetByTrangthai(string trangthai)
        {
            if (trangthai == null || trangthai == string.Empty)
            {
                return BadRequest();
            }
            List<Sach> result = await _unitOfWork.Saches.GetByTrangThai(trangthai);
            List<SachDTO> resultDTo = _mapping.Map<List<SachDTO>>(result);
            return Ok(resultDTo);
        }
    }
}
