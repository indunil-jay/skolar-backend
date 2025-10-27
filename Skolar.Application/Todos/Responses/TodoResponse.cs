using Skolar.Domain.Enums;

namespace Skolar.Application.Todos.Responses;

public sealed record TodoResponse (Guid Id, string Title, string? Description, 
    string Priority, bool IsCompleted, 
    DateTime CreatedAt, DateTime? UpdatedAt, DateTime? DueDate, DateTime? CompletedAt);

