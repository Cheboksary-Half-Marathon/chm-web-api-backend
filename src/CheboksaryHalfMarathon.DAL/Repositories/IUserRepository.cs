using CheboksaryHalfMarathon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CheboksaryHalfMarathon.DAL.Repositories
{
    public interface IUserRepository
    {
        public DbSet<User> Users();
        public void AddToContext(User user);
    }
}
