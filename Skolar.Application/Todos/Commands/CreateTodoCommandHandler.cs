using MapsterMapper;
using Skolar.Application.Todos.Responses;
using Skolar.Domain.Todos;
using Skolar.Application.Primitives;
using Skolar.Domain.Primitives;

namespace Skolar.Application.Todos.Commands;
internal class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, TodoResponse>

{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateTodoCommandHandler(IMapper mapper, ITodoRepository todoRepository ,IUnitOfWork unitOfWork )
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<TodoResponse>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = Todo.Create(command.Title, command.Description, command.Priority, command.DueDate);

        _todoRepository.Add(todo);

        await _unitOfWork.SaveChangesAsync(cancellationToken); 
        
        return _mapper.Map<TodoResponse>(todo);
    }
}
