using JWT.Data.Interfaces;
using JWT.Data.Repository;
using JWT.Domain.Interfaces;
using JWT.Domain.Models;

namespace JWT.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly JwtDbContext _context;

    public UnitOfWork(JwtDbContext context)
    {
        _context = context;
        UserInfos = new Repository<UserInfo>(_context);
        Enterprises = new Repository<Enterprise>(_context);
        Roles = new Repository<Role>(_context);
        RoleUsers = new Repository<RoleUser>(_context);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    // public IRepository<T> GetRepository<T>() where T : class, IEntityBase, new()
    // {
    //     var repositoryType = typeof(Repository<T>);
    //
    //     if (repositories.TryGetValue(repositoryType, out var repository))
    //     {
    //         return (Repository<T>)repository;
    //     }
    //
    //     repository = new Repository<T>(pbsDbContext);
    //     repositories.Add(repositoryType, repository);
    //
    //     return (Repository<T>)repository;
    //
    // }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync();
    }

    public IRepository<UserInfo> UserInfos { get; private set; }
    public IRepository<Enterprise> Enterprises { get; private set; }
    public IRepository<Role> Roles { get; private set; }
    public IRepository<RoleUser> RoleUsers { get; private set; }
}