using Blocks.Core.Cache;
using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blocks.EntityFramework;

public abstract class CachedRepository<TDbContext, TEntity, TId>(TDbContext dbContext, IMemoryCache memoryCache)
    where TDbContext : DbContext
    where TEntity : class, IEntity<TId>, ICacheable
    where TId : struct
{
    public IEnumerable<TEntity> GetAll()
        => memoryCache.GetOrCreateByType(entry => dbContext.Set<TEntity>().AsNoTracking().ToList())!;

    public TEntity GetById(TId id) => GetAll().Single(x => x.Id.Equals(id));
}
