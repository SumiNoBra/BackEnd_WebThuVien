using BackEnd.Interfaces;
using BackEnd.EF_Contexts;
using AutoMapper;
namespace BackEnd.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QlThuvienContext _context;
        public ISachRepository Saches { get; private set; }
        public INguoiDungRepository NguoiDungs { get; private set; }

        public UnitOfWork(
            QlThuvienContext qlThuvienContext,
            ISachRepository sachRepository,
            INguoiDungRepository nguoiDungRepository)
        {
            _context = qlThuvienContext;
            Saches = sachRepository;
            NguoiDungs = nguoiDungRepository;
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
