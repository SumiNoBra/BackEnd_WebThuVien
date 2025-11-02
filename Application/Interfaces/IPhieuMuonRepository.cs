using Domain.Entities;
namespace Application.Interfaces
{
    public interface IPhieuMuonRepository
    {
        public Task<List<Phieumuon>> GetAll();
        public Task<Phieumuon> Create(Phieumuon phieumuon);
        public Task<bool> ExsitsDocGiaID(int madocgia);
        public Task<bool> ExistID(int maphieumuon);
        //public Task<YeuCauMuonDTO?> GetById(int mayeucau);
        public Task<bool> UpdateTrangThai(int maphieumuon, string trangthai);
        //public Task<bool> Delete(int mayeucau);
        //public Task<bool> ExistsDocGiaId(int madocgia);
        public Task<List<Phieumuon>> GetByTrangThai(string trangthai);
        public Task<List<Phieumuon>> KiemTraVaTaoPhatQuaHan();
        public Task<bool> UpdateDaTraSach(int id);
    }
}
