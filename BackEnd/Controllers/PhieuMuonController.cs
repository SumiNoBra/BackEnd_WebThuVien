using AutoMapper;
using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapping;
        public PhieuMuonController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapping = mapper;
            _unitOfWork = unitOfWork;
        }
        // lấy tất cả phiếu mượn
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.PhieuMuons.GetAll();
            return Ok(result);
        }
        // tạo phiếu mượn
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Phieumuon phieumuon)
        {
            if (phieumuon.Madocgia == null || phieumuon == null || phieumuon.Chitietphieumuons == null || !phieumuon.Chitietphieumuons.Any())
            {
                return BadRequest(new { error = "invalid" });
            }
            if (!await _unitOfWork.PhieuMuons.ExsitsDocGiaID(phieumuon.Madocgia.Value))
            {
                return BadRequest(new { error = "Madocgia not exists" });
            }
            await _unitOfWork.PhieuMuons.Create(phieumuon);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        // cập nhật trạng thái phiếu mượn
        [HttpPatch("updatetrangthai")]

        public async Task<IActionResult> UpdateTrangThai([FromBody] UpdateTrangThaiDto trangthai)
        {
            if (trangthai.MaYeuCau <= 0 || string.IsNullOrWhiteSpace(trangthai.TrangThai))
            {
                return BadRequest();
            }
            if (!await _unitOfWork.YeuCauMuons.ExistID(trangthai.MaYeuCau))
            {
                return NotFound();
            }
            var result = await _unitOfWork.PhieuMuons.UpdateTrangThai(trangthai.MaYeuCau, trangthai.TrangThai);
            if (result)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return BadRequest();
        }
        // lấy phiếu mượn theo trạng thái
        [HttpGet("getbytrangthai/{trangthai}")]
        public async Task<IActionResult> GetByTrangThai(string trangthai)
        {
            if (string.IsNullOrWhiteSpace(trangthai))
            {
                return BadRequest();
            }
            var result = await _unitOfWork.PhieuMuons.GetByTrangThai(trangthai);
            return Ok(result);
        }
        [HttpGet("getvacreatequahan")]
        public async Task<IActionResult> GetVaCreateQuaHan()
        {
            List<PhieuMuonDTO> lsphieumuon= await _unitOfWork.PhieuMuons.KiemTraVaTaoPhatQuaHan();
            await _unitOfWork.CompleteAsync();
            return Ok(lsphieumuon);
        }
        [HttpPost("updatetrasach")]
        public async Task<IActionResult> UpdateTraSach([FromBody] PhieuMuonDTO phieuMuon)
        {
            if (phieuMuon.Maphieumuon == 0) { 
                return BadRequest();
            }
            if (await _unitOfWork.PhieuMuons.UpdateDaTraSach(phieuMuon.Maphieumuon))
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
