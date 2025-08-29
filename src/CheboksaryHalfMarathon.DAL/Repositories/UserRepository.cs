using CheboksaryHalfMarathon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CheboksaryHalfMarathon.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public HalfMarathonDbContext dbContext;

        public UserRepository(HalfMarathonDbContext dbContext)
            : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddToContext(User user)
        {
            dbContext.Users.Add(user);
        }

        public DbSet<User> Users()
        {
            return dbContext.Users;
        }
    }
}
