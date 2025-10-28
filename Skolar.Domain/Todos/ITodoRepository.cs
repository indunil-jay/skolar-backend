using Skolar.Domain.Primitives;

namespace Skolar.Domain.Todos;

public interface ITodoRepository : IBaseRepository<Todo>
{
    Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
}
