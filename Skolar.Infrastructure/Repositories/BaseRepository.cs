using Microsoft.EntityFrameworkCore;
using Skolar.Domain.Primitives;

namespace Skolar.Infrastructure.Repositories;

internal abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public virtual void Add(T entity)
    {
        _dbContext.Add(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(entry=> entry.Id==id, cancellationToken); 
    }
}
