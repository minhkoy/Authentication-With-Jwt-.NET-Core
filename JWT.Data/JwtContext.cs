using JWT.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    public class JwtContext : DbContext
    {
        public JwtContext() { }
        public JwtContext(DbContextOptions<JwtContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
    }
}