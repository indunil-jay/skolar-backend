using MapsterMapper;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Todos;
using Skolar.Application.Primitives;

namespace Skolar.Application.Todos.Commands;
internal class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, TodoResponse>

{
    private readonly IMapper _mapper;

    public CreateTodoCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }


    public Task<TodoResponse> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = Todo.Create(command.Title, command.Description, command.Priority, command.DueDate);

        return Task.FromResult(_mapper.Map<TodoResponse>(todo));
    }
}
