using System.Linq.Expressions;
using JWT.Domain.Interfaces;
using JWT.Domain.Models;

namespace JWT.Data.Interfaces;

public interface IRepository
{
    
}

public interface IRepository<T> : IRepository where T : class, IEntityBase, new()
{
    IQueryable<T> GetQueryable();
    IQueryable<T> GetTrackingQueryable();
    IQueryable<T> GetQueryableIncludeIsDelete();

    Task<T> FindAsync(string id);
    T Find(string id);

    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void RemoveRange(Expression<Func<T, bool>> predicate);

    Task<int> CountAsync();
    Task<int> CountIncludeIsDeleteAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

// public interface IUserInfoRepository : IRepository<UserInfo> {}
// public interface IEnterpriseRepository : IRepository<Enterprise> {}
// public interface IRoleRepository : IRepository<Role> {}
// public interface IRoleUserRepository : IRepository<RoleUser> {}