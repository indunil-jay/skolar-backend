using Skolar.Domain.Enums;

namespace Skolar.Domain.ValueObjects;

public sealed record TodoMetadata
{
    public TodoPriority Priority { get; }
    public bool IsCompleted { get; }
    public DateTime? DueDate { get; }

    public TodoMetadata(TodoPriority priority, bool isCompleted, DateTime? dueDate)
    {
        Priority = priority;
        IsCompleted = isCompleted;
        DueDate = dueDate;
    }

    public static TodoMetadata Add(TodoPriority priority, DateTime? dueDate = null, bool isCompleted=false)
    {
        return new TodoMetadata(priority, isCompleted, dueDate);
    }
}
