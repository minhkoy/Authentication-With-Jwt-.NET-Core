using System.Linq.Expressions;
using JWT.Data.Interfaces;
using JWT.Domain.Interfaces;
using JWT.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase, new()
{
    private readonly JwtDbContext _jwtDbContext;

    public Repository(JwtDbContext jwtDbContext)
    {
        _jwtDbContext = jwtDbContext;
    }
    
    public IQueryable<TEntity> GetQueryable()
    {
        return _jwtDbContext.Set<TEntity>().AsNoTracking().Where(x => !x.IsActive.HasValue || !x.IsDelete.Value);
    }

    public IQueryable<TEntity> GetTrackingQueryable()
    {
        return _jwtDbContext.Set<TEntity>().Where(x => !x.IsActive.HasValue || !x.IsDelete.Value);
    }

    public IQueryable<TEntity> GetQueryableIncludeIsDelete()
    {
        return _jwtDbContext.Set<TEntity>().AsNoTracking();
    }

    public async Task<TEntity> FindAsync(string id)
    {
        return await this.GetQueryable().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public TEntity Find(string id)
    {
        return this.GetQueryable().FirstOrDefault(x => x.Id.Equals(id));
    }

    public void Add(TEntity entity)
    {
        _jwtDbContext.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _jwtDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _jwtDbContext.Set<TEntity>().AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _jwtDbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _jwtDbContext.Set<TEntity>().Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _jwtDbContext.Set<TEntity>().UpdateRange(entities);
    }

    public void Remove(TEntity entity)
    {
        _jwtDbContext.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _jwtDbContext.Set<TEntity>().RemoveRange(entities);
    }

    public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _jwtDbContext.Set<TEntity>().Where(predicate);
        _jwtDbContext.Set<TEntity>().RemoveRange(entities);
    }

    public async Task<int> CountAsync()
    {
        return await this.GetQueryable().CountAsync();
    }

    public async Task<int> CountIncludeIsDeleteAsync()
    {
        return await _jwtDbContext.Set<TEntity>().CountAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _jwtDbContext.SaveChangesAsync(cancellationToken);
    }
}

// public class UserInfoRepsitory : Repository<UserInfo>, IUserInfoRepository
// {
//     public UserInfoRepsitory(JwtDbContext context) : base(context) {}
// }
//
// public class EnterpriseRepository : Repository<Enterprise>, IEnterpriseRepository
// {
//     public EnterpriseRepository(JwtDbContext jwtDbContext) : base(jwtDbContext)
//     {
//     }
// }

// public class RoleRepository : Repository<Role>, 