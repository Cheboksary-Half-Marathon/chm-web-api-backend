using CheboksaryHalfMarathon.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection.Metadata.Ecma335;

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

        public IUserRepository UserRepository
        {
            get
            {
                return new UserRepository(_context);
            }
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
