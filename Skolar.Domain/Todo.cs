using Skolar.Domain.Enums;

namespace Skolar.Domain;

public sealed class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TodoPriority Priority { get; private set; } 
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private Todo(
        Guid id,
        string title,
        string? description,
        TodoPriority priority,
        bool isCompleted,
        DateTime createdAt,
        DateTime? dueDate
    )
    {

        Id = id;
        Title = title;
        Description = description;
        Priority = priority;
        IsCompleted = isCompleted;
        CreatedAt = createdAt;
        DueDate = dueDate;
    }

    public static Todo Create(
        string title,
        string? description = null,
        TodoPriority priority = TodoPriority.Normal,
        DateTime? dueDate = null)
    {
        return new Todo(
            Guid.NewGuid(),
            title,
            description,
            priority,
            false, 
            DateTime.UtcNow,
            dueDate
        );
    }

  
}
