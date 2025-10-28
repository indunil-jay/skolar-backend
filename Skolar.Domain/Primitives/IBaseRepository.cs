namespace Skolar.Domain.Primitives;

public interface IBaseRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
     void Add(T entity);    
     void Delete(T entity);
}
