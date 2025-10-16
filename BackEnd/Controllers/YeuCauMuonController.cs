using AutoMapper;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YeuCauMuonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public YeuCauMuonController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        // lấy tất cả yêu cầu mượn
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var yeucaumuons = await _unitOfWork.YeuCauMuons.GetAll();
            return Ok(yeucaumuons);
        }
        // lấy yêu cầu mượn theo mã yêu cầu
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var yeucaumuon = await _unitOfWork.YeuCauMuons.GetById(id);
            if (yeucaumuon == null)
            {
                return NotFound();
            }
            return Ok(yeucaumuon);
        }
        // tạo yêu cầu mượn
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Yeucaumuon yeucaumuon)
        {
            if (yeucaumuon.Madocgia == null || yeucaumuon == null || yeucaumuon.Chitietyeucaumuons == null || !yeucaumuon.Chitietyeucaumuons.Any())
            {
                return BadRequest(new { error = "invalid" });
            }

            await _unitOfWork.YeuCauMuons.Create(yeucaumuon);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        // cập nhật trạng thái yêu cầu mượn
        [HttpPatch("updatetrangthai")]
        public async Task<IActionResult> UpdateTrangThai([FromBody] UpdateTrangThaiDto update)
        {
            if (update.MaYeuCau <= 0 || string.IsNullOrWhiteSpace(update.TrangThai))
            {
                return BadRequest();
            }
            if (!await _unitOfWork.YeuCauMuons.ExistID(update.MaYeuCau))
            {
                return NotFound();
            }
            var result = await _unitOfWork.YeuCauMuons.UpdateTrangThai(update.MaYeuCau, update.TrangThai);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return BadRequest();
        }
        // xóa yêu cầu mượn
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.YeuCauMuons.Delete(id);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return BadRequest();
        }
        // lấy yêu cầu mượn theo trạng thái
        [HttpGet("getbytrangthai/{trangthai}" )]
        public async Task<IActionResult> GetByTrangThai(string trangthai)
        {
            if (string.IsNullOrWhiteSpace(trangthai))
            {
                return BadRequest();
            }
            var yeucaumuons = await _unitOfWork.YeuCauMuons.GetByTrangThai(trangthai);
            return Ok(yeucaumuons);
        }
    }
}
