using MapsterMapper;
using MediatR;
using Skolar.Application.Todos.Responses;
using Skolar.Domain;

namespace Skolar.Application.Todos.Commands;

internal class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoResponse>

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
