using Blocks.Domain.Entities;
using Blocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public static class RepositoryExtentions
{
    public static async Task<TEntity> FindByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        var entity = await repository.GetByIdAsync(id);

        if (entity is null)
            throw new NotFoundException($"{typeof(TEntity).Name} not found");

        return entity;
    }
}
