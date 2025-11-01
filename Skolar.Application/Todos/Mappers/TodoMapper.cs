using Mapster;
using Skolar.Application.Todos.Commands;
using Skolar.Application.Todos.Requests;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Todos;
using Skolar.Domain.Todos.Enums;

namespace Skolar.Application.Todos.Mappers
{
    public sealed class TodoMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // ---- Request → Command ----
            config.NewConfig<CreateTodoRequest, CreateTodoCommand>()
                 .Map(dest => dest.Title, src => src.Title ?? string.Empty)
                 .Map(dest => dest.Description, src => src.Description)
                 .Map(dest => dest.DueDate, src => src.DueDate)
                 .Map(dest => dest.Priority, src => ParsePriorityOrDefault(src.Priority));

            // ---- Domain → Response ----
            config.NewConfig<Todo, TodoResponse>()
                .Map(dest => dest.Title, src => src.Title.Value)
                .Map(dest => dest.Description, src => src.Description != null ? src.Description.Value : null)
                .Map(dest => dest.Priority, src => src.Metadata.Priority.ToString())
                .Map(dest => dest.IsCompleted, src => src.Metadata.IsCompleted)
                .Map(dest => dest.DueDate, src => src.Metadata.DueDate)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.UpdatedAt, src => src.UpdatedAt)
                .Map(dest => dest.CompletedAt, src => src.CompletedAt);
        }

        private static TodoPriority ParsePriorityOrDefault(string? priority)
        {
            if (string.IsNullOrWhiteSpace(priority))
                return TodoPriority.None;

            return Enum.TryParse<TodoPriority>(priority, true, out var parsed)
                ? parsed
                : TodoPriority.None;
        }
    }
}
