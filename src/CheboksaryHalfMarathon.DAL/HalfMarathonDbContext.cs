using Microsoft.EntityFrameworkCore;

namespace CheboksaryHalfMarathon.DAL
{
    public class HalfMarathonDbContext : DbContext
    {
        public HalfMarathonDbContext()
        {  
        }

        public HalfMarathonDbContext(DbContextOptions<HalfMarathonDbContext> options)
            : base(options) 
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();

            base.OnModelCreating(modelBuilder);
        }
    }
}
