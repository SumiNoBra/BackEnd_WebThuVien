using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.EF_Contexts;
using Microsoft.AspNetCore.Authorization;
using BackEnd.DTOs;
using AutoMapper;
using BackEnd.Interfaces;
namespace BackEnd.Controllers
{
    [Route("api/sach")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapping;
        public SachController( IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapping = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Route("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            List<Sach> result = await _unitOfWork.Saches.GetAllAsync();
            List<SachDTO> resultDTo = _mapping.Map<List<SachDTO>>(result);
            return Ok(resultDTo);
        }
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
        [HttpGet("theloai/{matheloai}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByTheLoai(int matheloai)
        {
            List<Sach> result = await _unitOfWork.Saches.GetByTheLoaiIDAsync(matheloai);
            if (result == null )
            {
                return NotFound();
            }
            List<SachDTO> mappedResult = _mapping.Map<List<SachDTO>>(result);
            return Ok(mappedResult);
        }
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            List<Sach> result = await _unitOfWork.Saches.SearchByNameAsync(keyword);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            List<SachDTO> resultDTO = _mapping.Map<List<SachDTO>>(result); 
            return Ok(resultDTO);
        }
        [HttpPut("update")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SachDTO sachdto)
        {
            Sach sach = _mapping.Map<Sach>(sachdto);
            await _unitOfWork.Saches.UpdateAsync(sach);
            await _unitOfWork.CompleteAsync();
            return Ok(new { message = "Cập nhật sách thành công." });

        }
        [HttpPost("add")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] SachDTO sachdto)
        {
            Sach sach = _mapping.Map<Sach>(sachdto);
            await _unitOfWork.Saches.AddAsync(sach);
            await _unitOfWork.CompleteAsync();
            return Ok(new { message = "Thêm sách thành công." });

        }
    }
}
