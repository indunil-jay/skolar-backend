using Skolar.Domain.Primitives;
using Skolar.Domain.Todos.Enums;
using Skolar.Domain.Todos.Events;
using Skolar.Domain.Todos.ValueObjects;

namespace Skolar.Domain.Todos;

public sealed class Todo : AggregateRoot
{
    public TodoTitle Title { get; private set; } = default!;
    public TodoDescription? Description { get; private set; }
    public TodoMetadata Metadata { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    //EF Core
    private Todo()
    {
    }

    private Todo(
        Guid id,
        TodoTitle title,
        TodoDescription? description,
        TodoMetadata metadata,
        DateTime createdAt
    ) : base(id)
    {

        Title = title;
        Description = description;
        Metadata = metadata;
        CreatedAt = createdAt;
    }


    public static Todo Create(
        TodoTitle title,
        TodoDescription? description = null,
        TodoPriority priority = TodoPriority.Normal,
        DateTime? dueDate = null)
    {
        var todo = new Todo(Guid.NewGuid(), title, description, TodoMetadata.Add(priority, dueDate), DateTime.UtcNow);
        todo.PublishDomainEvent(new TodoCreatedDomainEvent(todo));
        return todo;
    }


}
