using JWT.Domain.Interfaces;
using JWT.Domain.Models;

namespace JWT.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    //IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IRepository<UserInfo> UserInfos { get; }
    IRepository<Enterprise> Enterprises { get; }
    IRepository<Role> Roles { get; }
    IRepository<RoleUser> RoleUsers { get; }
}