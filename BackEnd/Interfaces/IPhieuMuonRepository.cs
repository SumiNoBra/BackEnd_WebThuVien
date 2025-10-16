using BackEnd.DTOs;
using BackEnd.EF_Contexts;
using BackEnd.Repositories;

namespace BackEnd.Interfaces
{
    public interface IPhieuMuonRepository
    {
        public Task<List<PhieuMuonDTO>> GetAll();
        public Task<Phieumuon> Create(Phieumuon phieumuon);
        public Task<bool> ExsitsDocGiaID(int madocgia);
        public Task<bool> ExistID(int maphieumuon);
        //public Task<YeuCauMuonDTO?> GetById(int mayeucau);
        public Task<bool> UpdateTrangThai(int maphieumuon, string trangthai);
        //public Task<bool> Delete(int mayeucau);
        //public Task<bool> ExistsDocGiaId(int madocgia);
        public Task<List<PhieuMuonDTO>> GetByTrangThai(string trangthai);
        public Task<List<PhieuMuonDTO>> KiemTraVaTaoPhatQuaHan();
        public Task<bool> UpdateDaTraSach(int id);
    }
}
