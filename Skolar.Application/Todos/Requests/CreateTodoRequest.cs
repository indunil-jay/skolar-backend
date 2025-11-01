namespace Skolar.Application.Todos.Requests;

public sealed record CreateTodoRequest(
    string? Title,
    string? Description,
    string? Priority,
    DateTime? DueDate
);
