using MapsterMapper;
using MediatR;
using Skolar.Application.Todos.Responses;
using Skolar.Domain;
using Skolar.Domain.Enums;
using Skolar.Domain.ValueObjects;

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
       var todo = Todo.Create(
            new TodoTitle(command.Title),
            new TodoDescription(command.Description),
           (TodoPriority)Enum.Parse(typeof(TodoPriority), command.Priority, true),
        command.DueDate);

        return Task.FromResult(_mapper.Map<TodoResponse>(todo)); 
    }
}
