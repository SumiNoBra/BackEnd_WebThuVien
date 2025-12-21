using API.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers_V2
{
    [Route("api")]
    [ApiController]
    public class DocGiaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memory;
        public string cacheKey = "lst";
        public DocGiaController(IUnitOfWork unitofwork,IMemoryCache memory) { 
            _unitOfWork= unitofwork;
            _memory= memory;
        }
        [EnableRateLimiting("per_ip")]
        [HttpGet("docgias")]
        [AllowAnonymous]
        public async Task<IActionResult> GetALL() {

            List<DocGia> docgias;
            if (!_memory.TryGetValue(cacheKey, out docgias!))
            {
                docgias = await _unitOfWork.docgiarepo.GetDocGias();
                _memory.Set(cacheKey, docgias, TimeSpan.FromSeconds(10));
            }
            return Ok(docgias);
        }
        [HttpGet("docgia/{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            DocGia? docgia = await _unitOfWork.docgiarepo.GetDocGia(id);
            if(docgia == null)
            {
                return NotFound();
            }
            return Ok(docgia);
        }   
        [HttpPost("docgia")]
        public async Task<IActionResult> Create([FromBody] DocGia docgia)
        {
            if (docgia != null)
            {
                if (docgia.Email != null) {
                    bool existEmail = await _unitOfWork.docgiarepo.ExistEmail(docgia.Email);
                    if (existEmail) {
                        return BadRequest(new
                        {
                            error = "email"
                        });
                    }
                }
                await _unitOfWork.docgiarepo.CreateDocGia(docgia);
                return Created();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("docgia")]
        public async Task<IActionResult> UpdateDocGia(UpdateDocGia docgia)
        {
            DocGia? ishasdocgia = await _unitOfWork.docgiarepo.GetDocGia(docgia.docgia.MaDocGia);
            if (ishasdocgia == null)
            {
                return NotFound();
            }
            if (ishasdocgia.Email == docgia.docgia.Email)
            {
                docgia.docgia.Email = null;
            }
            if (docgia.docgia.Email != null || !string.IsNullOrWhiteSpace(docgia.docgia.Email))
            {
                bool ishasemail = await _unitOfWork.docgiarepo.ExistEmail(docgia.docgia.Email);
                if (ishasemail)
                {
                    return BadRequest(new
                    {
                        error = "email"
                    });
                }
            }
            if (docgia.matkhaucu != ishasdocgia.MatKhau)
            {
                return BadRequest(new
                {
                    error = "matkhau"
                });
            }
            bool result = await _unitOfWork.docgiarepo.UpdateDocGia(docgia.docgia);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
