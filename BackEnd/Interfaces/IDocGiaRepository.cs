using BackEnd.DTOs;
using BackEnd.EF_Contexts;

namespace BackEnd.Interfaces
{
    public interface IDocGiaRepository
    {
        public Task<List<DocGiaDTO>> GetAll();
        public Task Create(Docgia docgia);
        public Task<bool> ExistNguoiDungID(int id);
        public Task<bool> Update(Docgia docgia);
    }
}
