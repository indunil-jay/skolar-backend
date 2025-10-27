using Mapster;
using Skolar.Api.Controllers.Todos;
using Skolar.Application.Todos.Commands;
using Skolar.Application.Todos.Responses;
using Skolar.Domain;

namespace Skolar.Application.Todos.Mappers;

public sealed class TodoMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTodoRequest, CreateTodoCommand>();

        config.NewConfig<Todo, TodoResponse>();

    }
}
