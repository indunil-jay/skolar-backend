using Mapster;
using Skolar.Api.Controllers.Todos;
using Skolar.Application.Todos.Commands;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Todos;
using Skolar.Domain.Todos.Enums;
using Skolar.Domain.Todos.ValueObjects;

namespace Skolar.Application.Todos.Mappers;

public sealed class TodoMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Map request → command
        config.NewConfig<CreateTodoRequest, CreateTodoCommand>()
      .Map(dest => dest.Title, src =>  TodoTitle.Create(src.Title))
      .Map(dest => dest.Description, src => src.Description==null ? null :new TodoDescription(src.Description))
      .Map(dest => dest.Priority, src => Enum.Parse<TodoPriority>(src.Priority, true));




        // Map domain entity → response
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

    

}
