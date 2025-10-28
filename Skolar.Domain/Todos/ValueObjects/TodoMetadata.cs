using Skolar.Domain.Todos.Enums;
using System.Text.Json.Serialization;

namespace Skolar.Domain.Todos.ValueObjects;

public sealed record TodoMetadata
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TodoPriority Priority { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? DueDate { get; private set; }

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
