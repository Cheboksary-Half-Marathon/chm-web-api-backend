using CheboksaryHalfMarathon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CheboksaryHalfMarathon.DAL
{
    public class HalfMarathonDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public HalfMarathonDbContext()
        {  
        }

        public HalfMarathonDbContext(DbContextOptions<HalfMarathonDbContext> options)
            : base(options) 
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().HasKey("UserId");
            modelBuilder.Entity<User>().HasIndex(u => u.UserEmail).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.UserId).HasDefaultValueSql("NEXT VALUE FOR UserIdSequence");
            modelBuilder.HasSequence<int>("UserIdSequence").IncrementsBy(1).HasMin(1).HasMax(100000).StartsAt(1);
            base.OnModelCreating(modelBuilder);
        }
    }
}
