using JWT.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    public class JwtDbContext : DbContext
    {
        public JwtDbContext() { }
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
    }
}