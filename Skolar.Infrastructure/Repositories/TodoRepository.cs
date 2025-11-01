using Microsoft.EntityFrameworkCore;
using Skolar.Domain.Todos;

namespace Skolar.Infrastructure.Repositories;

internal sealed class TodoRepository : BaseRepository<Todo>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Todo>().ToListAsync(cancellationToken);
    }

}
