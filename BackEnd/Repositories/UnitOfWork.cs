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
        public IYeuCauMuonRepository YeuCauMuons { get; private set; }
        public IPhieuMuonRepository PhieuMuons { get; private set; }
        public IDocGiaRepository DocGias { get; private set; }

        public UnitOfWork(
            QlThuvienContext qlThuvienContext)
        {
            _context = qlThuvienContext;
            Saches = new SachRepository(_context);
            NguoiDungs = new NguoiDungRepository(_context);
            YeuCauMuons = new YeuCauMuonRepository(_context);
            PhieuMuons = new PhieuMuonRepository(_context);
            DocGias = new DocGiaRepository(_context);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
