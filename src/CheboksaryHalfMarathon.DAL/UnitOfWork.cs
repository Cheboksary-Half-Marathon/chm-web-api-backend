using Microsoft.EntityFrameworkCore.Storage;

namespace CheboksaryHalfMarathon.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HalfMarathonDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(HalfMarathonDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
