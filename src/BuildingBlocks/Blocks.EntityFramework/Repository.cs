using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> FindByIdAsync(int id);
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);//this method is not async becuase it didnt touch the database and only change tracked object and then save change async
    void Remove(TEntity entity);
    Task<bool> DeleteByIdAsync(int id);
}

public class Repository<TContext, TEntity> : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : class, IEntity
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _entity;

    public TContext Context => _context;
    public virtual DbSet<TEntity> Entity => _entity;
    public string TableName => _context.Model.FindEntityType(typeof(TEntity))?.GetTableName()!;

    public Repository(TContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> Query() => _entity;

    public virtual async Task<TEntity?> FindByIdAsync(int id)
        => await _entity.FindAsync(id);

    public virtual async Task<TEntity?> GetByIdAsync(int id)
        => await Query().SingleOrDefaultAsync(x => x.Id.Equals(id));

    public virtual async Task<TEntity> AddAsync(TEntity entity)
        => (await _entity.AddAsync(entity)).Entity;

    public virtual TEntity Update(TEntity entity)
        => _entity.Update(entity).Entity;

    public virtual void Remove(TEntity entity)
        => _entity.Remove(entity);

    public virtual async Task<bool> DeleteByIdAsync(int id)
    {
        var rowAffected = await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM {TableName} WHERE Id = {id}");
        return rowAffected > 0;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);
}
