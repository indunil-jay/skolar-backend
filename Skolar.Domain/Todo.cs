using Skolar.Domain.Enums;
using Skolar.Domain.ValueObjects;

namespace Skolar.Domain;

public sealed class Todo
{
    public Guid Id { get; private set; }
    public TodoTitle Title { get; private set; }
    public TodoDescription? Description { get; private set; }
    public TodoMetadata Metadata { get; private set;  }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private Todo(
        Guid id,
        TodoTitle title,
        TodoDescription? description,
        TodoMetadata metadata,
        DateTime createdAt
    )
    {

        Id = id;
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
        return new Todo(
            Guid.NewGuid(),
            title,
            description,
            TodoMetadata.Add(priority, dueDate),
            DateTime.UtcNow
        );
    }

  
}
