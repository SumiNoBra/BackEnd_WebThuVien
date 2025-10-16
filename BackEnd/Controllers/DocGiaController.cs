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
    public class DocGiaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DocGiaController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        // lấy tất cả độc giả
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var docgias = await _unitOfWork.DocGias.GetAll();
            return Ok(docgias);
        }
        // tạo độc giả
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] DocGiaDTO docGiaDTO)
        {
            if (docGiaDTO == null || docGiaDTO.Manguoidung == null)
            {
                return BadRequest(new { error = "invalid" });
            }
            if (await _unitOfWork.DocGias.ExistNguoiDungID(docGiaDTO.Manguoidung.Value)
                && !await _unitOfWork.NguoiDungs.ExistIDAsync(docGiaDTO.Manguoidung.Value))
            {
                return BadRequest(new { error = "Manguoidung not exists" });
            }
            Docgia docgia = _mapper.Map<Docgia>(docGiaDTO);
            await _unitOfWork.DocGias.Create(docgia);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        // update độc giả
        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] DocGiaDTO docGiaDTO)
        {
            Docgia docgia = _mapper.Map<Docgia>(docGiaDTO);
            bool kq = await _unitOfWork.DocGias.Update(docgia);
            if (kq)
            {
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
