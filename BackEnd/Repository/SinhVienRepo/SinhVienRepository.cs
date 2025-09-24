using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DTo;

namespace MainAPI.Repository
{
    public class SinhVienReponsitory : ISinhVienRepository
    {
        private readonly QL_SinhVienContext _context;
        private readonly IMapper _mapper;

        public SinhVienReponsitory(QL_SinhVienContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> CreateSV(SinhVien _sv)
        {
            if (_sv == null || string.IsNullOrEmpty(_sv.MaSV))
            {
                return "isNull";
            }
            var isHas = await _context.SinhViens.AnyAsync((e) => e.MaSV == _sv.MaSV);
            if (isHas)
            {
                return "isHas";
            }
            try
            {
                _context.SinhViens.Add(_sv);
                await _context.SaveChangesAsync();
                return "true";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return "isError";
            }
        }

        public async Task<string> DeleteSV(string id)
        {
            try
            {
                var sv = await _context.SinhViens.FirstOrDefaultAsync((e) => e.MaSV == id);
                if (sv == null)
                {
                    return "isNull";
                }
                else
                {
                    _context.QuanHes.RemoveRange(_context.QuanHes.Where(qh => qh.MaSV == id));
                    _context.Diems.RemoveRange(_context.Diems.Where(dd => dd.MaSV == id));
                    _context.SinhViens.Remove(sv);
                    await _context.SaveChangesAsync();
                    return "true";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return "isError";
            }
        }

        public async Task<List<SinhVien>> GetAllSV()
        {
            try
            {
                return await _context.SinhViens.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return new List<SinhVien>();
            }
        }

        public async Task<string> UpdateSV(string id, SinhVienDTo _sv)
        {
            try
            {
                SinhVien sv = await _context.SinhViens.FirstOrDefaultAsync((e) => e.MaSV == id);
                if (sv == null)
                {
                    return "isNull";
                }
                else
                {
                    _mapper.Map(_sv, sv);
                    await _context.SaveChangesAsync();
                    return "true";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                return "isError";
            }
        }

    }
}
