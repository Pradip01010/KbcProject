using KbcProject.Models;
using Microsoft.EntityFrameworkCore;

namespace KbcProject.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
