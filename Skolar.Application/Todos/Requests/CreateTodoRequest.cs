namespace Skolar.Api.Controllers.Todos;

public sealed record CreateTodoRequest(string Title, string? Description, string Priority, DateTime DueDate);

