using BackEnd.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.EF_Contexts;
using Microsoft.AspNetCore.Authorization;
using BackEnd.DTOs;
using AutoMapper;
namespace BackEnd.Controllers
{
    [Route("api/sach")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly ISachRepository _sachRepository;
        private readonly IMapper _mapping;
        public SachController( ISachRepository sachRepository, IMapper mapper)
        {
            _mapping = mapper;
            _sachRepository = sachRepository;
        }
        [HttpGet]
        [Route("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {

            List<Sach> result = await _sachRepository.GetAllSachesAsync();
            List<SachDTO> resultDTo = _mapping.Map<List<SachDTO>>(result);
            return Ok(resultDTo);
        }
        [HttpGet("{masach}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByMasach(int masach)
        {
            Sach result = await _sachRepository.GetSachByMasachAsync(masach);
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
            List<Sach> result = await _sachRepository.GetSachesByTheLoaiAsync(matheloai);
            if (result == null || result.Count == 0)
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
            List<Sach> result = await _sachRepository.SearchSachesAsync(keyword);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            List<SachDTO> resultDTO = _mapping.Map<List<SachDTO>>(result); 
            return Ok(resultDTO);
        }
        [HttpPost("update")]
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SachDTO sach)
        {
            try
            {
                await _sachRepository.UpdateSachAsync(sach);
                return Ok(new { message = "Cập nhật sách thành công." });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = "isnull" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Đã xảy ra lỗi trong quá trình cập nhật sách." });
            }
        }
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody] Sach sach)
        {
            try
            {
                await _sachRepository.AddSachAsync(sach);
                return Ok(new { message = "Thêm sách thành công." });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = "isnull" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Đã xảy ra lỗi trong quá trình thêm sách." });
            }
        }
    }
}
